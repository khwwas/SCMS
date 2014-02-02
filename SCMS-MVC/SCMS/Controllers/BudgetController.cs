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
            string ls_Nature = "'Expense', 'Assets'";
            var lst_BudgetTypes = new DALBudgetType().PopulateData();
            var lst_Years = new DALCalendar().GetAllRecords();
            var lst_Locations = new DALLocation().PopulateData();
            var ChartOfAccounts = new DALChartOfAccount().GetChartOfAccountForDropDown(0, ls_Nature);
            string ChartOfAccountCodes = "";

            foreach (SETUP_ChartOfAccount COA in ChartOfAccounts)
            {
                if (ChartOfAccountCodes.Length > 0)
                {
                    ChartOfAccountCodes += "|" + COA.ChrtAcc_Id + ":" + COA.ChrtAcc_Title;
                }
                else
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
            ViewData["ddl_Account"] = ChartOfAccounts;

            return View("Budget", modelBudget);
        }

        public string SaveBudget(SCMS.Models.BudgetMaster BudgetModel)
        {
            DALBudgetEntry objDalBudgetEntry = new DALBudgetEntry();
            DALCalendar objDalCalendar = new DALCalendar();
            GL_BgdtMaster GLBgdtMaster = new GL_BgdtMaster();
            string ls_YearPrefix = "";
            Int32 li_ReturnValue = 0;
            String ls_BudgetMasterId = "", ls_BudgetMasterCode = "", ls_Status = "", ls_BudgetType = "", ls_Year = "", ls_LocationId = "", ls_Remarks = "",
                   ls_BudgetDetailId = "";
            DateTime ldt_BudgetDate;

            try
            {
                ls_BudgetMasterId = BudgetModel.MasterId;
                ls_BudgetMasterCode = BudgetModel.Code.Replace("[Auto]", null);
                ldt_BudgetDate = !String.IsNullOrEmpty(BudgetModel.Date) ? Convert.ToDateTime(BudgetModel.Date) : DateTime.Now;
                ls_Status = BudgetModel.Status;
                ls_BudgetType = BudgetModel.BudgetType;
                ls_Year = BudgetModel.CalendarYear;
                ls_LocationId = BudgetModel.Location;
                ls_Remarks = BudgetModel.Remarks;

                //ls_YearPrefix = objDalCalendar.GetCalendarPrefix_ByCurrentDate(ldt_BudgetDate);
                ls_YearPrefix = objDalCalendar.GetCalendarPrefix_ByCldrId(ls_Year);

                if (String.IsNullOrEmpty(ls_BudgetMasterId))
                {
                    if (DALCommon.AutoCodeGeneration("GL_BgdtMaster") == 1)
                    {

                        if (ls_YearPrefix == null && ls_YearPrefix == "")
                        {
                            return "";
                        }
                        ls_BudgetMasterId = DALCommon.GetMaxBudgetMasId(ls_YearPrefix);
                        ls_BudgetMasterCode = ls_BudgetMasterId;
                    }
                }
                else
                {
                    objDalBudgetEntry.DeleteBudgetDetail_ByBgdtMasId(ls_BudgetMasterId);
                }

                if (!String.IsNullOrEmpty(ls_BudgetMasterCode))
                {
                    GLBgdtMaster.BgdtMas_Id = ls_BudgetMasterId;
                    GLBgdtMaster.BgdtMas_Code = ls_BudgetMasterCode;
                    GLBgdtMaster.BgdtMas_Date = ldt_BudgetDate;
                    GLBgdtMaster.BgdtMas_Status = ls_Status;
                    GLBgdtMaster.BgdtType_Id = ls_BudgetType;
                    GLBgdtMaster.Cldr_Id = ls_Year;
                    GLBgdtMaster.Loc_Id = ls_LocationId;
                    GLBgdtMaster.BgdtMas_Remarks = ls_Remarks;
                    GLBgdtMaster.BgdtMas_EnteredDate = DateTime.Now;
                    GLBgdtMaster.BgdtMas_EnteredBy = ((SECURITY_User)Session["user"]).User_Title;

                    li_ReturnValue = objDalBudgetEntry.SaveBudgetMaster(GLBgdtMaster);

                    if (li_ReturnValue > 0)
                    {
                        foreach (SCMS.Models.BudgetDetail detail in BudgetModel.ListBudgetDetail)
                        {
                            ls_BudgetDetailId = DALCommon.GetMaxBudgetDetId(ls_YearPrefix);

                            GL_BgdtDetail dbDetail = new GL_BgdtDetail();
                            dbDetail.BgdtDet_Id = ls_BudgetDetailId;
                            dbDetail.BgdtMas_Id = ls_BudgetMasterId;
                            dbDetail.ChrtAcc_Id = detail.Account;
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
                            dbDetail.BgdtDet_TotalAmount = detail.TotalAmount;
                            dbDetail.BgdtDet_Remarks = detail.Remarks;

                            objDalBudgetEntry.SaveBudgetDetail(dbDetail);
                        }
                    }
                }


                return ls_BudgetMasterId + ":" + ls_BudgetMasterCode;
            }
            catch
            {
                return "0";
            }
        }

        public ActionResult CopyBudget(FormCollection frm)
        {
            DALBudgetEntry dal_BudgetEntry = new DALBudgetEntry();
            DALCalendar dal_Calendar = new DALCalendar();
            GL_BgdtMaster lst_ExistingBudgetMaster = new GL_BgdtMaster();
            GL_BgdtMaster lst_NewBudgetMaster = new GL_BgdtMaster();

            string ls_YearPrefix = "", ls_NewMasterId = "", ls_CurrentMasterId = "", ls_NewDetailId = "", ls_ApplicableOn = "", ls_InflateDeflate = "";
            Int64 li_ReturnValue = 0, li_ApplicablePercent = 0;
            decimal ldec_BudgetTotalAmount = 0, ldec_BudgetMonthlyAmount = 0;

            try
            {
                ls_CurrentMasterId = frm["MasterId"];
                ls_ApplicableOn = frm["rdo_BudgetActual"];
                li_ApplicablePercent = Convert.ToInt32(frm["percentage"]);
                ls_InflateDeflate = frm["rdo_InflateDeflate"];

                // Getting year prefix
                ls_YearPrefix = dal_Calendar.GetCalendarPrefix_ByCurrentDate(DateTime.Now);
                if (DALCommon.AutoCodeGeneration("GL_BgdtMaster") == 1)
                {
                    ls_NewMasterId = DALCommon.GetMaxBudgetMasId(ls_YearPrefix);
                }

                // Retrieving existing master and detail records
                lst_ExistingBudgetMaster = dal_BudgetEntry.GetAllMasterRecords().Where(c => c.BgdtMas_Id.Equals(ls_CurrentMasterId)).SingleOrDefault();
                List<GL_BgdtDetail> lst_ExistingBudgetDetail = dal_BudgetEntry.GetAllDetailRecords().Where(c => c.BgdtMas_Id.Equals(ls_CurrentMasterId)).ToList();

                // Setting and saving of new budget master record
                lst_NewBudgetMaster.BgdtMas_Id = ls_NewMasterId;
                lst_NewBudgetMaster.BgdtMas_Code = ls_NewMasterId;
                lst_NewBudgetMaster.BgdtMas_Date = DateTime.Now;
                lst_NewBudgetMaster.BgdtMas_Status = "Pending";
                lst_NewBudgetMaster.BgdtType_Id = lst_ExistingBudgetMaster.BgdtType_Id;
                lst_NewBudgetMaster.Cldr_Id = lst_ExistingBudgetMaster.Cldr_Id;
                lst_NewBudgetMaster.Loc_Id = lst_ExistingBudgetMaster.Loc_Id;
                lst_NewBudgetMaster.BgdtMas_Remarks = "Copy of " + lst_ExistingBudgetMaster.BgdtMas_Code + " - " + lst_ExistingBudgetMaster.BgdtMas_Remarks;
                lst_NewBudgetMaster.BgdtMas_EnteredDate = DateTime.Now;
                lst_NewBudgetMaster.BgdtMas_EnteredBy = ((SECURITY_User)Session["user"]).User_Title;

                li_ReturnValue = dal_BudgetEntry.SaveBudgetMaster(lst_NewBudgetMaster);

                if (li_ReturnValue > 0)
                {
                    foreach (GL_BgdtDetail detail in lst_ExistingBudgetDetail)
                    {
                        GL_BgdtDetail lst_NewBudgetDetail = new GL_BgdtDetail();

                        if (DALCommon.AutoCodeGeneration("GL_BgdtDetail") == 1)
                        {
                            ls_NewDetailId = DALCommon.GetMaxBudgetDetId(ls_YearPrefix);
                        }

                        lst_NewBudgetDetail.BgdtDet_Id = ls_NewDetailId;
                        lst_NewBudgetDetail.BgdtMas_Id = ls_NewMasterId;
                        lst_NewBudgetDetail.ChrtAcc_Id = detail.ChrtAcc_Id;

                        ldec_BudgetTotalAmount = 0;
                        if (ls_ApplicableOn == "Budget")
                        {
                            ldec_BudgetTotalAmount = Convert.ToDecimal(detail.BgdtDet_TotalAmount.ToString());
                        }
                        else
                        {
                            //li_BudgetTotalAmount = 7000;
                        }

                        if (ls_InflateDeflate == "Inflate")
                        {
                            ldec_BudgetTotalAmount = ldec_BudgetTotalAmount + ((ldec_BudgetTotalAmount * li_ApplicablePercent) / 100);
                        }
                        else
                        {
                            ldec_BudgetTotalAmount = ldec_BudgetTotalAmount - ((ldec_BudgetTotalAmount * li_ApplicablePercent) / 100);
                        }

                        ldec_BudgetMonthlyAmount = 0;
                        ldec_BudgetMonthlyAmount = ldec_BudgetTotalAmount / 12;

                        lst_NewBudgetDetail.BgdtDet_TotalAmount = ldec_BudgetTotalAmount;
                        lst_NewBudgetDetail.BgdtDet_Month1 = ldec_BudgetMonthlyAmount;
                        lst_NewBudgetDetail.BgdtDet_Month2 = ldec_BudgetMonthlyAmount;
                        lst_NewBudgetDetail.BgdtDet_Month3 = ldec_BudgetMonthlyAmount;
                        lst_NewBudgetDetail.BgdtDet_Month4 = ldec_BudgetMonthlyAmount;
                        lst_NewBudgetDetail.BgdtDet_Month5 = ldec_BudgetMonthlyAmount;
                        lst_NewBudgetDetail.BgdtDet_Month6 = ldec_BudgetMonthlyAmount;
                        lst_NewBudgetDetail.BgdtDet_Month7 = ldec_BudgetMonthlyAmount;
                        lst_NewBudgetDetail.BgdtDet_Month8 = ldec_BudgetMonthlyAmount;
                        lst_NewBudgetDetail.BgdtDet_Month9 = ldec_BudgetMonthlyAmount;
                        lst_NewBudgetDetail.BgdtDet_Month10 = ldec_BudgetMonthlyAmount;
                        lst_NewBudgetDetail.BgdtDet_Month11 = ldec_BudgetMonthlyAmount;
                        lst_NewBudgetDetail.BgdtDet_Month12 = ldec_BudgetTotalAmount - (ldec_BudgetMonthlyAmount * 11);
                        lst_NewBudgetDetail.BgdtDet_Remarks = detail.BgdtDet_Remarks;

                        dal_BudgetEntry.SaveBudgetDetail(lst_NewBudgetDetail);
                    }
                }

                return RedirectToAction("Index", "BudgetConsole", new { });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ActionResult Temp_CopyBudget(FormCollection frm)
        {
            DALBudgetEntry dal_BudgetEntry = new DALBudgetEntry();
            DALCalendar dal_Calendar = new DALCalendar();
            GL_BgdtMaster ds_ExistingBudgetMaster;
            Int64 li_BudgetTotalAmount = 0;
            string ls_YearPrefix = "", ls_NewMasterId = "", ls_CurrentMasterId = "", ls_ApplicableOn = "", ls_InflateDeflate = "";
            Int64 li_ApplicablePercent = 0;

            try
            {
                ls_CurrentMasterId = frm["MasterId"];
                ls_ApplicableOn = frm["rdo_BudgetActual"];
                li_ApplicablePercent = Convert.ToInt32(frm["percentage"]);
                ls_InflateDeflate = frm["rdo_InflateDeflate"];

                if (DALCommon.AutoCodeGeneration("GL_BgdtMaster") == 1)
                {
                    ls_YearPrefix = dal_Calendar.GetCalendarPrefix_ByCurrentDate(DateTime.Now);
                    ls_NewMasterId = DALCommon.GetMaxBudgetMasId(ls_YearPrefix);
                }


                ds_ExistingBudgetMaster = dal_BudgetEntry.GetAllMasterRecords().Where(c => c.BgdtMas_Id.Equals(ls_CurrentMasterId)).SingleOrDefault();
                List<GL_BgdtDetail> SelectedBudgetDetail = new DALBudgetEntry().GetAllDetailRecords().Where(c => c.BgdtMas_Id.Equals(ls_CurrentMasterId)).ToList();
                System.Data.DataSet dsBudget = new DALReports().BudgetSummary(ds_ExistingBudgetMaster.Loc_Id, 0, ds_ExistingBudgetMaster.Cldr_Id);

                if (ls_ApplicableOn == "Budget")
                {
                    li_BudgetTotalAmount = 5000;//dsBudget.Tables[0].Rows[0]["ActualBudget"] == null ? 0 : Convert.ToInt32(dsBudget.Tables[0].Rows[0]["ActualBudget"]);//Convert.ToInt32(SelectedBudgetDetail.Sum(c => c.BgdtDet_TotalAmount));
                }
                else
                {

                    li_BudgetTotalAmount = 7000;//dsBudget.Tables[0].Rows[0]["ActualExpense"] == null ? 0 : Convert.ToInt32(dsBudget.Tables[0].Rows[0]["ActualExpense"]);
                }

                if (ls_InflateDeflate == "Inflate")
                {
                    li_BudgetTotalAmount = li_BudgetTotalAmount + ((li_BudgetTotalAmount * li_ApplicablePercent) / 100);
                }
                else
                {
                    li_BudgetTotalAmount = li_BudgetTotalAmount - ((li_BudgetTotalAmount * li_ApplicablePercent) / 100);
                }

                int BudgetPerMonth = Convert.ToInt32(li_BudgetTotalAmount / 12);

                GL_BgdtMaster NewBudgetMaster = new GL_BgdtMaster();


                NewBudgetMaster.BgdtMas_Id = ls_NewMasterId;
                NewBudgetMaster.BgdtMas_Code = ls_NewMasterId;
                NewBudgetMaster.BgdtMas_Date = DateTime.Now;
                NewBudgetMaster.BgdtMas_Status = "Pending";
                NewBudgetMaster.BgdtType_Id = ds_ExistingBudgetMaster.BgdtType_Id;
                NewBudgetMaster.Cldr_Id = ds_ExistingBudgetMaster.Cldr_Id;
                NewBudgetMaster.Loc_Id = ds_ExistingBudgetMaster.Loc_Id;
                NewBudgetMaster.BgdtMas_Remarks = "Copy of " + ds_ExistingBudgetMaster.BgdtMas_Code + " - " + ds_ExistingBudgetMaster.BgdtMas_Remarks;
                NewBudgetMaster.BgdtMas_EnteredDate = DateTime.Now;
                NewBudgetMaster.BgdtMas_EnteredBy = ((SECURITY_User)Session["user"]).User_Title;
                Int32 li_ReturnValue = dal_BudgetEntry.SaveBudgetMaster(NewBudgetMaster);

                if (li_ReturnValue > 0)
                {
                    foreach (GL_BgdtDetail detail in SelectedBudgetDetail)
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
                        dbDetail.BgdtMas_Id = ls_NewMasterId;
                        dbDetail.BgdtDet_Month1 = BudgetPerMonth;
                        dbDetail.BgdtDet_Month2 = BudgetPerMonth;
                        dbDetail.BgdtDet_Month3 = BudgetPerMonth;
                        dbDetail.BgdtDet_Month4 = BudgetPerMonth;
                        dbDetail.BgdtDet_Month5 = BudgetPerMonth;
                        dbDetail.BgdtDet_Month6 = BudgetPerMonth;
                        dbDetail.BgdtDet_Month7 = BudgetPerMonth;
                        dbDetail.BgdtDet_Month8 = BudgetPerMonth;
                        dbDetail.BgdtDet_Month9 = BudgetPerMonth;
                        dbDetail.BgdtDet_Month10 = BudgetPerMonth;
                        dbDetail.BgdtDet_Month11 = BudgetPerMonth;
                        dbDetail.BgdtDet_Month12 = BudgetPerMonth;
                        dbDetail.BgdtDet_TotalAmount = li_BudgetTotalAmount;
                        dbDetail.BgdtDet_Remarks = detail.BgdtDet_Remarks;
                        dbDetail.ChrtAcc_Id = detail.ChrtAcc_Id;
                        dal_BudgetEntry.SaveBudgetDetail(dbDetail);
                    }
                }

                return RedirectToAction("Index", "BudgetConsole", new { });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
