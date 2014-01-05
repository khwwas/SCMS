using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
using CrystalDecisions.Shared.Json;
using System.Web.Script.Serialization;

namespace SCMS.Controllers
{
    public class BudgetController : Controller
    {
        public ActionResult Index(string p_BudgetId)
        {
            var lst_BudgetTypes = new DALBudgetType().PopulateData();
            var lst_Years = new DALCalendar().GetAllRecords();
            var lst_Locations = new DALLocation().PopulateData();
            //var lst_ChartOfAccounts = new DALChartOfAccount().GetChartOfAccountForDropDown();
            var ChartOfAccounts = new DALChartOfAccount().GetChartOfAccountForDropDown();
            string ChartOfAccountCodes = "";
            foreach (SETUP_ChartOfAccount COA in ChartOfAccounts)
            {
                if (ChartOfAccountCodes.Length > 0)
                {
                    ChartOfAccountCodes += "," + COA.ChrtAcc_Id + ":" + COA.ChrtAcc_Title;
                }
                {
                    ChartOfAccountCodes += COA.ChrtAcc_Id + ":" + COA.ChrtAcc_Title;
                }
            }
            ViewData["ChartOfAccountCodesWithTitles"] = ChartOfAccountCodes;

            SCMS.Models.BudgetMaster modelBudget = new SCMS.Models.BudgetMaster();
            modelBudget.ListBudgetDetail = new List<SCMS.Models.BudgetDetail>();

            if (!string.IsNullOrEmpty(p_BudgetId))
            {
                GL_BgdtMaster budget = new SCMSDataLayer.DALBudgetEntry().GetAllMasterRecords().Where(c => c.BgdtMas_Id.Equals(p_BudgetId)).SingleOrDefault();
                modelBudget.MasterId = budget.BgdtMas_Id;
                modelBudget.Code = budget.BgdtMas_Code;
                modelBudget.Date = Convert.ToDateTime(budget.BgdtMas_Date).ToString("MM/dd/yyyy");
                modelBudget.Status = budget.BgdtMas_Status;
                modelBudget.Remarks = budget.BgdtMas_Remarks;
                modelBudget.BudgetType = budget.BgdtType_Id;
                modelBudget.Location = budget.Loc_Id;
                modelBudget.CalendarYear = budget.Cldr_Id;
                List<GL_BgdtDetail> budgetDetailList = new DALBudgetEntry().GetAllDetailRecords().Where(c => c.BgdtMas_Id.Equals(p_BudgetId)).ToList();

                foreach (GL_BgdtDetail dbBudgetDetail in budgetDetailList)
                {
                    SCMS.Models.BudgetDetail budgetDetailRow = new SCMS.Models.BudgetDetail();
                    budgetDetailRow.MasterId = dbBudgetDetail.BgdtMas_Id;
                    budgetDetailRow.DetailId = dbBudgetDetail.BgdtDet_Id;
                    budgetDetailRow.Month1 = dbBudgetDetail.BgdtDet_Month1;
                    budgetDetailRow.Month2 = dbBudgetDetail.BgdtDet_Month2;
                    budgetDetailRow.Month3 = dbBudgetDetail.BgdtDet_Month3;
                    budgetDetailRow.Month4 = dbBudgetDetail.BgdtDet_Month4;
                    budgetDetailRow.Month5 = dbBudgetDetail.BgdtDet_Month5;
                    budgetDetailRow.Month6 = dbBudgetDetail.BgdtDet_Month6;
                    budgetDetailRow.Month7 = dbBudgetDetail.BgdtDet_Month7;
                    budgetDetailRow.Month8 = dbBudgetDetail.BgdtDet_Month8;
                    budgetDetailRow.Month9 = dbBudgetDetail.BgdtDet_Month9;
                    budgetDetailRow.Month10 = dbBudgetDetail.BgdtDet_Month10;
                    budgetDetailRow.Month11 = dbBudgetDetail.BgdtDet_Month11;
                    budgetDetailRow.Month12 = dbBudgetDetail.BgdtDet_Month12;
                    budgetDetailRow.TotalAmount = dbBudgetDetail.BgdtDet_TotalAmount;
                    budgetDetailRow.Account = dbBudgetDetail.ChrtAcc_Id;

                    modelBudget.ListBudgetDetail.Add(budgetDetailRow);
                }
            }
            else
            {
                modelBudget.Date = DateTime.Now.ToString("MM/dd/yyyy");
                modelBudget.Code = "[Auto]";
                SCMS.Models.BudgetDetail detail = new SCMS.Models.BudgetDetail();
                modelBudget.ListBudgetDetail.Add(detail);
            }

            ViewData["BudgetType"] = new SelectList(lst_BudgetTypes, "BgdtType_Id", "BgdtType_Title", modelBudget.BudgetType);
            ViewData["CalendarYear"] = new SelectList(lst_Years, "Cldr_Id", "Cldr_Title", modelBudget.CalendarYear);
            ViewData["Location"] = new SelectList(lst_Locations, "Loc_Id", "Loc_Title", modelBudget.Location);
            ViewData["ddl_Account"] = ChartOfAccounts;//new SelectList(lst_ChartOfAccounts, "ChrtAcc_Id", "ChrtAcc_Title", "");

            return View("Budget", modelBudget);
        }

        public string SaveBudget(SCMS.Models.BudgetMaster BudgetModel)
        {
            DALBudgetEntry objDalBudgetEntry = new DALBudgetEntry();
            DALCalendar objDalCalendar = new DALCalendar();
            GL_BgdtMaster GLBgdtMaster = new GL_BgdtMaster();
            int flag = 0;

            String IsAuditTrail = "", ls_UserId = "";

            try
            {

                String BudgetMasterId = BudgetModel.MasterId;
                String BudgetMasterCode = BudgetModel.Code.Replace("[Auto]", null);
                DateTime BudgetDate = !String.IsNullOrEmpty(BudgetModel.Date) ? Convert.ToDateTime(BudgetModel.Date) : DateTime.Now;
                String Status = BudgetModel.Status;
                String BudgetType = BudgetModel.BudgetType;
                String Year = BudgetModel.CalendarYear;
                String LocationId = BudgetModel.Location;
                String Remarks = BudgetModel.Remarks;
                string ls_YearPrefix = "";


                if (String.IsNullOrEmpty(BudgetMasterCode))
                {
                    if (DALCommon.AutoCodeGeneration("GL_BgdtMaster") == 1)
                    {
                        ls_YearPrefix = objDalCalendar.GetCalendarPrefix_ByCurrentDate(BudgetDate);
                        if (ls_YearPrefix == null && ls_YearPrefix == "")
                        {
                            return "";
                        }
                        BudgetMasterId = DALCommon.GetMaxBudgetId(ls_YearPrefix);
                        BudgetMasterCode = BudgetMasterId;
                    }
                }
                else
                {
                    flag = 1;
                }


                if (!String.IsNullOrEmpty(BudgetMasterCode))
                {
                    GLBgdtMaster.BgdtMas_Id = BudgetMasterId;
                    GLBgdtMaster.BgdtMas_Code = BudgetMasterCode;
                    GLBgdtMaster.BgdtMas_Date = BudgetDate;
                    GLBgdtMaster.BgdtMas_Status = Status;
                    GLBgdtMaster.BgdtType_Id = BudgetType;
                    GLBgdtMaster.Cldr_Id = Year;
                    GLBgdtMaster.Loc_Id = LocationId;
                    GLBgdtMaster.BgdtMas_Remarks = Remarks;
                    GLBgdtMaster.BgdtMas_EnteredDate = DateTime.Now;
                    GLBgdtMaster.BgdtMas_EnteredBy = ((SECURITY_User)Session["user"]).User_Title;
                    Int32 li_ReturnValue = objDalBudgetEntry.SaveBudgetMaster(GLBgdtMaster);

                    if (li_ReturnValue > 0)
                    {
                        if (flag == 1)
                        {
                            objDalBudgetEntry.DeleteDetailRecordByMasterId(BudgetMasterId);
                        }

                        foreach (SCMS.Models.BudgetDetail detail in BudgetModel.ListBudgetDetail)
                        {
                            String BudgetDetailCode = "";

                            if (String.IsNullOrEmpty(BudgetDetailCode))
                            {
                                if (DALCommon.AutoCodeGeneration("GL_BgdtDetail") == 1)
                                {
                                    BudgetDetailCode = DALCommon.GetMaximumCode("GL_BgdtDetail");
                                }
                            }

                            GL_BgdtDetail dbDetail = new GL_BgdtDetail();
                            dbDetail.BgdtDet_Id = BudgetDetailCode;
                            dbDetail.BgdtMas_Id = BudgetMasterCode;
                            dbDetail.BgdtDet_Month1 = detail.Month1;
                            dbDetail.BgdtDet_Month2 = detail.Month2;
                            dbDetail.BgdtDet_Month3 = detail.Month3;
                            dbDetail.BgdtDet_Month4 = detail.Month4;
                            dbDetail.BgdtDet_Month5 = detail.Month5;
                            dbDetail.BgdtDet_Month6 = detail.Month6;
                            dbDetail.BgdtDet_Month7 = detail.Month7;
                            dbDetail.BgdtDet_Month8 = detail.Month8;
                            dbDetail.BgdtDet_Month9 = detail.Month9;
                            dbDetail.BgdtDet_Month10 = detail.Month10;
                            dbDetail.BgdtDet_Month11 = detail.Month11;
                            dbDetail.BgdtDet_Month12 = detail.Month12;
                            //decimal TotalAmount = 0;
                            //TotalAmount += Convert.ToDecimal(detail.Month1);
                            //TotalAmount += Convert.ToDecimal(detail.Month2);
                            //TotalAmount += Convert.ToDecimal(detail.Month3);
                            //TotalAmount += Convert.ToDecimal(detail.Month4);
                            //TotalAmount += Convert.ToDecimal(detail.Month5);
                            //TotalAmount += Convert.ToDecimal(detail.Month6);
                            //TotalAmount += Convert.ToDecimal(detail.Month7);
                            //TotalAmount += Convert.ToDecimal(detail.Month8);
                            //TotalAmount += Convert.ToDecimal(detail.Month9);
                            //TotalAmount += Convert.ToDecimal(detail.Month10);
                            //TotalAmount += Convert.ToDecimal(detail.Month11);
                            //TotalAmount += Convert.ToDecimal(detail.Month12);
                            dbDetail.BgdtDet_TotalAmount = detail.TotalAmount;
                            dbDetail.BgdtDet_Remarks = detail.Remarks;
                            dbDetail.ChrtAcc_Id = detail.Account;
                            objDalBudgetEntry.SaveBudgetDetail(dbDetail);
                        }
                    }
                }


                return BudgetMasterId + ":" + BudgetMasterCode;
            }
            catch
            {
                return "0";
            }

        }


    }
}
