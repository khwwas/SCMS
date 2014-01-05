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
    public class BudgetControllerBack : Controller
    {
        //
        // GET: /Budget/
        public ActionResult Index(string p_BudgetId)
        {
            //DALBudgetEntry objDalBudgetEntry = new DALBudgetEntry();

            var lst_BudgetTypes = new DALBudgetType().PopulateData();
            var lst_Years = new DALCalendar().GetAllRecords();
            var lst_Locations = new DALLocation().PopulateData();
            var lst_ChartOfAccounts = new DALChartOfAccount().GetChartOfAccountForDropDown();

            ViewData["ddl_BudgetType"] = new SelectList(lst_BudgetTypes, "BgdtType_Id", "BgdtType_Title", "");
            ViewData["ddl_Year"] = new SelectList(lst_Years, "Cldr_Id", "Cldr_Title", "");
            ViewData["ddl_Location"] = new SelectList(lst_Locations, "Loc_Id", "Loc_Title", "");
            ViewData["ddl_Account"] = new SelectList(lst_ChartOfAccounts, "ChrtAcc_Id", "ChrtAcc_Title", "");

            //string ChartOfAccountCodes = "";
            //foreach (SETUP_ChartOfAccount COA in ChartOfAccounts)
            //{
            //    if (ChartOfAccountCodes.Length > 0)
            //    {
            //        ChartOfAccountCodes += "," + COA.ChrtAcc_Id + ":" + COA.ChrtAcc_Title;
            //    }
            //    {
            //        ChartOfAccountCodes += COA.ChrtAcc_Id + ":" + COA.ChrtAcc_Title;
            //    }
            //}
            //ViewData["ChartOfAccountCodesWithTitles"] = ChartOfAccountCodes;
            //SETUP_ChartOfAccount SelectChartOfAccount = new SETUP_ChartOfAccount();
            //SelectChartOfAccount.ChrtAcc_Id = "0";
            //SelectChartOfAccount.ChrtAcc_Code = "";
            //ChartOfAccounts.Insert(0, SelectChartOfAccount);
            //ViewData["ChartOfAccounts"] = ChartOfAccounts;
            //ViewData["ddl_Account"] = new SelectList(ChartOfAccounts, "ChrtAcc_Id", "ChrtAcc_Title", "");
            ////var NarrationList = new DALBudgetTypeNarration().GetAllData().ToList();
            ////string[] nList = new string[NarrationList.Count];
            ////if (NarrationList != null && NarrationList.Count > 0)
            ////{
            ////    for (int index = 0; index < NarrationList.Count; index++)
            ////    {
            ////        nList[index] = NarrationList[index].VchrTypeNarr_Title;
            ////    }
            ////    System.Web.Script.Serialization.JavaScriptSerializer se = new System.Web.Script.Serialization.JavaScriptSerializer();
            ////    ViewData["Narrations"] = se.Serialize(nList);
            ////}
            //ViewData["ddl_Location"] = new SelectList(Locations, "Loc_Id", "Loc_Title", Session["LocationIdForBudgetEntry"]);
            if (!String.IsNullOrEmpty(p_BudgetId))
            {
                EditBudget(p_BudgetId);
            }
            else
            {
                ViewData["BudgetCode"] = "[Auto]";
                ViewData["CurrentDate"] = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Session.Remove("BudgetTypeForBudgetEntry");
            //Session.Remove("LocationIdForBudgetEntry");
            return View("Budget");
        }

        //public ActionResult BudgetEntry(String BudgetId)
        //{
        //    DALBudgetEntry objDalBudgetEntry = new DALBudgetEntry();

        //    var BudgetTypes = new DALBudgetType().PopulateData();
        //    //var Locations = new DALLocation().PopulateData();
        //    //var Budget = new DALBudgetEntry().GetAllMasterRecords();

        //    ViewData["ddl_BudgetType"] = new SelectList(BudgetTypes, "BgdtType_Id", "BgdtType_Title", "");
        //    ViewData["ddl_Year"] = new SelectList(BudgetTypes, "BgdtType_Id", "BgdtType_Title", "");
        //    //var ChartOfAccounts = new DALChartOfAccount().GetChartOfAccountForDropDown();
        //    //string ChartOfAccountCodes = "";
        //    //foreach (SETUP_ChartOfAccount COA in ChartOfAccounts)
        //    //{
        //    //    if (ChartOfAccountCodes.Length > 0)
        //    //    {
        //    //        ChartOfAccountCodes += "," + COA.ChrtAcc_Id + ":" + COA.ChrtAcc_Title;
        //    //    }
        //    //    {
        //    //        ChartOfAccountCodes += COA.ChrtAcc_Id + ":" + COA.ChrtAcc_Title;
        //    //    }
        //    //}
        //    //ViewData["ChartOfAccountCodesWithTitles"] = ChartOfAccountCodes;
        //    //SETUP_ChartOfAccount SelectChartOfAccount = new SETUP_ChartOfAccount();
        //    //SelectChartOfAccount.ChrtAcc_Id = "0";
        //    //SelectChartOfAccount.ChrtAcc_Code = "";
        //    //ChartOfAccounts.Insert(0, SelectChartOfAccount);
        //    //ViewData["ChartOfAccounts"] = ChartOfAccounts;
        //    //ViewData["ddl_Account"] = new SelectList(ChartOfAccounts, "ChrtAcc_Id", "ChrtAcc_Title", "");
        //    ////var NarrationList = new DALBudgetTypeNarration().GetAllData().ToList();
        //    ////string[] nList = new string[NarrationList.Count];
        //    ////if (NarrationList != null && NarrationList.Count > 0)
        //    ////{
        //    ////    for (int index = 0; index < NarrationList.Count; index++)
        //    ////    {
        //    ////        nList[index] = NarrationList[index].VchrTypeNarr_Title;
        //    ////    }
        //    ////    System.Web.Script.Serialization.JavaScriptSerializer se = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    ////    ViewData["Narrations"] = se.Serialize(nList);
        //    ////}
        //    //ViewData["ddl_Location"] = new SelectList(Locations, "Loc_Id", "Loc_Title", Session["LocationIdForBudgetEntry"]);
        //    //if (!String.IsNullOrEmpty(BudgetId))
        //    //{
        //    //    SetBudgetEntryToEdit(BudgetId);
        //    //}
        //    //else
        //    //{
        //    //    ViewData["BudgetCode"] = "[Auto]";
        //    //    ViewData["CurrentDate"] = DateTime.Now.ToString("MM/dd/yyyy");
        //    //}
        //    //Session.Remove("BudgetTypeForBudgetEntry");
        //    //Session.Remove("LocationIdForBudgetEntry");
        //    return View("Budget");
        //}

        public String NewBudgetDetailEntryRow(int RowNo, String AccountCode, String Narration, String Debit, String Credit)
        {
            if (AccountCode == null || AccountCode == "undefined")
            {
                AccountCode = "0";
            }
            if (Narration == null || Narration == "undefined")
            {
                Narration = "";
            }
            if (Debit == null || Debit == "undefined" || Debit == "0.000000")
            {
                Debit = "";
            }
            else
            {
                Debit = Math.Round(Convert.ToDecimal(Debit), 2).ToString();
            }
            if (Credit == null || Credit == "undefined" || Credit == "0.000000")
            {
                Credit = "";
            }
            else
            {
                Credit = Math.Round(Convert.ToDecimal(Credit), 2).ToString();
            }
            var ChartOfAccounts = new DALChartOfAccount().GetChartOfAccountForDropDown();
            String Response = "";
            // Response += "<div id='DetailRow" + RowNo.ToString() + "' style='float: left; width: auto;'>";
            Response += "<div class='CustomCell' style='width: 250px; height: 30px;'>";
            Response += "<select id='ddl_Account" + RowNo.ToString() + "' name='ddl_Account" + RowNo.ToString() + "' style='width:250px;'><option value='0'></option>";
            foreach (SETUP_ChartOfAccount row in ChartOfAccounts)
            {
                if (row.ChrtAcc_Id == AccountCode)
                {
                    Response += "<option value='" + row.ChrtAcc_Id + "' selected='selected'>" + row.ChrtAcc_Title + "</option>";
                }
                else
                {
                    Response += "<option value='" + row.ChrtAcc_Id + "'>" + row.ChrtAcc_Title + "</option>";
                }
            }
            Response += "</select>";
            Response += "</div>";
            Response += "<div class='CustomCell' style='width: 565px; height: 30px;'>";
            Response += "<input type='text' class='CustomText' style='width: 545px;' id='txt_Details" + RowNo.ToString() + "' name='txt_Details'";
            Response += "maxlength='200' value='" + Narration + "' />";
            Response += "</div>";
            Response += "<div class='CustomCell' style='width: 118px; height: 30px;'>";
            Response += "<input type='text' class='CustomText' style='width: 100px;' id='txt_Debit" + RowNo.ToString() + "' name='txt_Debit'";
            if (Credit != "")
            {
                Response += "maxlength='50' onblur='SetTotals(this.id)' disabled='disabled' />";
            }
            else
            {
                Response += "maxlength='50' value='" + Debit + "' onblur='SetTotals(this.id)' />";
            }
            Response += "</div>";
            Response += "<div class='CustomCell' style='width: 118px; height: 30px;'>";
            Response += "<input type='text' class='CustomText' style='width: 100px;' id='txt_Credit" + RowNo.ToString() + "' name='txt_Credit'";
            if (Debit != "")
            {
                Response += "maxlength='50'  onblur='SetTotals(this.id)' disabled='disabled' />";
            }
            else
            {
                Response += "maxlength='50' value='" + Credit + "' onblur='SetTotals(this.id)' />";
            }
            Response += "</div>";
            // Response += "</div>";
            return Response;
        }

        //public String GetLastRecordByBudgetTypeId(string LocationId, string BudgetTypeId)
        //{
        //    var Budget = new DALBudgetEntry().GetLastRecordByVchrType(LocationId, BudgetTypeId);
        //    String LastBudget = "";
        //    if (Budget != null && Budget.Count > 0)
        //    {
        //        LastBudget += "<div class='CustomCell' style='width: 800px; height: 30px; font-family: Tahoma;'>";
        //        LastBudget += "<b>Budget # : </b>";
        //        LastBudget += Budget.Last().BgdtMas_Code;
        //        LastBudget += "<b>, Date : </b>";
        //        LastBudget += Budget.Last().BgdtMas_Date != null ? Convert.ToDateTime(Budget.Last().BgdtMas_Date).ToShortDateString() : "";
        //        LastBudget += "<b>, Status : </b>";
        //        LastBudget += Budget.Last().BgdtMas_Status;
        //        LastBudget += "</div>";
        //        LastBudget += "<div class='Clear' style='border-bottom: 1px solid #ccc; margin-bottom: 5px;'>";
        //        LastBudget += "</div>";
        //    }
        //    return LastBudget;
        //}

        //public ActionResult SaveBudget(String BudgetMasterCode, DateTime BudgetDate, string Status, String BudgetType, String LocationId, String Remarks, String[] BudgetDetailRows)
        //[HttpPost]
        //[AcceptVerbs(HttpVerbs.Post)]
        public string SaveBudget(IEnumerable<string> BudgetRow)
        {

            //Session["BudgetTypeForBudgetEntry"] = BudgetType;
            //Session["LocationIdForBudgetEntry"] = LocationId;
            DALBudgetEntry objDalBudgetEntry = new DALBudgetEntry();
            DALCalendar objDalCalendar = new DALCalendar();
            GL_BgdtMaster GLBgdtMaster = new GL_BgdtMaster();
            //int flag = 0;

            //String ls_Action = "Edit", IsAuditTrail = "", ls_UserId = "";
            //String[] ls_Lable = new String[7], ls_Data = new String[7];
            Int32 li_ReturnValue = 0;

            try
            {

                //    GL_VchrDetail GL_Detail = new GL_VchrDetail();
                //    String Prefix = new DALBudgetType().GetAllData().Where(c => c.BgdtType_Id.Equals(BudgetType)).SingleOrDefault().VchrType_Prefix;
                //    ls_VchrTypId = Convert.ToString(Convert.ToInt32(BudgetType));

                String[] MasterRow = BudgetRow.Last().Split('║');
                String BudgetMasterId = MasterRow[0];
                String BudgetMasterCode = MasterRow[1].Replace("[Auto]", null);
                DateTime BudgetDate = MasterRow[2] != null ? Convert.ToDateTime(MasterRow[2]) : DateTime.Now;
                String Status = MasterRow[3];
                String BudgetType = MasterRow[4];
                String Year = MasterRow[5];
                String LocationId = MasterRow[6];
                String Remarks = MasterRow[7];
                string ls_YearPrefix = "";


                //if (String.IsNullOrEmpty(BudgetMasterId))
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
                        //BudgetMasterCode = DALCommon.GetMaxBudgetCode("GL_VchrMaster", BudgetType, Prefix, LocationId, ls_YearPrefix);
                        //ls_Action = "Add";
                    }
                }

                //    List<GL_VchrDetail> BudgetDetailList = new List<GL_VchrDetail>();

                if (!String.IsNullOrEmpty(BudgetMasterCode))
                {
                    //ViewData["BudgetId"] = BudgetMasterId;
                    //ViewData["BudgetCode"] = BudgetMasterCode;

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
                    li_ReturnValue = objDalBudgetEntry.SaveBudgetMaster(GLBgdtMaster);
                    //        if (li_ReturnValue > 0)
                    //        {
                    //            if (flag == 1)
                    //            {
                    //                objDalBudgetEntry.DeleteDetailRecordByMasterId(BudgetMasterId);
                    //            }
                    //            for (int index = 0; index < BudgetDetailRows.ToList().Count - 1; index++)
                    //            {
                    //                string Row = BudgetDetailRows.ToList()[index];
                    //                String[] Columns = Row.Split('║');
                    //                String BudgetDetailCode = "";

                    //                if (String.IsNullOrEmpty(BudgetDetailCode))
                    //                {
                    //                    if (DALCommon.AutoCodeGeneration("GL_VchrDetail") == 1)
                    //                    {
                    //                        BudgetDetailCode = DALCommon.GetMaximumCode("GL_VchrDetail");
                    //                        //BudgetDetailCode = BudgetMasterCode;
                    //                    }
                    //                }

                    //                if (!String.IsNullOrEmpty(BudgetDetailCode) && Columns[0] != null && Columns[0] != "" && Columns[0] != "0" && ((Columns[1] != null && Columns[1] != "") || (Columns[2] != null && Columns[2] != "")))
                    //                {
                    //                    GL_Detail = new GL_VchrDetail();
                    //                    GL_Detail.VchDet_Id = BudgetDetailCode;
                    //                    GL_Detail.BgdtMas_Id = BudgetMasterId;
                    //                    GL_Detail.ChrtAcc_Id = Columns[0].ToString(); //Columns[0] has AccountId from Account Title drop down;
                    //                    GL_Detail.VchMas_DrAmount = (Columns[1] != null && Columns[1] != "") ? Convert.ToDecimal(Columns[1].Replace(",", "")) : 0; // Columns[1] has Debit Amount
                    //                    GL_Detail.VchMas_CrAmount = (Columns[2] != null && Columns[2] != "") ? Convert.ToDecimal(Columns[2].Replace(",", "")) : 0; // Columns[2] has Debit Amount
                    //                    GL_Detail.VchDet_Remarks = (Columns[3] != null && Columns[3] != "") ? Columns[3].ToString() : ""; // Columns[3] has Remarks
                    //                    objDalBudgetEntry.SaveBudgetDetail(GL_Detail);
                    //                    BudgetDetailList.Add(GL_Detail);
                    //                }
                    //            }
                    //        }
                }
                //ViewData["SaveResult"] = li_ReturnValue;

                //    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                //    // Save Audit Log
                //    if (li_ReturnValue > 0 && IsAuditTrail == "1")
                //    {
                //        DALAuditLog objAuditLog = new DALAuditLog();

                //        ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                //        ls_Lable[0] = "Code";
                //        ls_Lable[1] = "Date";
                //        ls_Lable[2] = "Location";
                //        ls_Lable[3] = "Budget Type";
                //        ls_Lable[4] = "Remarks";
                //        ls_Lable[5] = "Status";

                //        ls_Data[0] = BudgetMasterCode;
                //        ls_Data[1] = BudgetDate.ToString("dd/MM/yyyy");
                //        ls_Data[2] = LocationId;
                //        ls_Data[3] = BudgetType;
                //        ls_Data[4] = Remarks;
                //        ls_Data[5] = Status;

                //        foreach (GL_VchrDetail BudgetDetail in BudgetDetailList)
                //        {
                //            Increment++;
                //            ls_Lable[Increment] = "Account Code";
                //            ls_Data[Increment] = BudgetDetail.ChrtAcc_Id;

                //            Increment++;
                //            ls_Lable[Increment] = "Debit";
                //            ls_Data[Increment] = Convert.ToString(BudgetDetail.VchMas_DrAmount);

                //            Increment++;
                //            ls_Lable[Increment] = "Credit";
                //            ls_Data[Increment] = Convert.ToString(BudgetDetail.VchMas_CrAmount);

                //            Increment++;
                //            ls_Lable[Increment] = "Narration";
                //            ls_Data[Increment] = BudgetDetail.VchDet_Remarks;
                //        }

                //        objAuditLog.SaveRecord(7, ls_UserId, ls_Action, ls_Lable, ls_Data);
                //    }
                //    //// Audit Trail Entry Section
                //    //if (li_ReturnValue > 0)
                //    //{
                //    //    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                //    //    if (IsAuditTrail == "1")
                //    //    {
                //    //        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                //    //        DALAuditLog objAuditTrail = new DALAuditLog();
                //    //        systemAuditTrail.Scr_Id = flag == 0 ? 17 : 16;
                //    //        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                //    //        systemAuditTrail.Loc_Id = GL_Master.Loc_Id;
                //    //        systemAuditTrail.BgdtType_Id = GL_Master.BgdtType_Id;
                //    //        systemAuditTrail.AdtTrl_Action = flag == 0 ? "Add" : "Edit";
                //    //        systemAuditTrail.AdtTrl_EntryId = BudgetMasterId;
                //    //        systemAuditTrail.AdtTrl_DataDump = "BgdtMas_Id = " + GL_Master.BgdtMas_Id + ";";
                //    //        systemAuditTrail.AdtTrl_DataDump += "BgdtMas_Code = " + GL_Master.BgdtMas_Code + ";";
                //    //        systemAuditTrail.AdtTrl_DataDump += "BgdtMas_Date = " + GL_Master.BgdtMas_Date + ";";
                //    //        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + GL_Master.Cmp_Id + ";";
                //    //        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + GL_Master.Loc_Id + ";";
                //    //        systemAuditTrail.AdtTrl_DataDump += "BgdtType_Id = " + GL_Master.BgdtType_Id + ";";
                //    //        systemAuditTrail.AdtTrl_DataDump += "BgdtMas_Remarks = " + GL_Master.BgdtMas_Remarks + ";";
                //    //        systemAuditTrail.AdtTrl_DataDump += "BgdtMas_Status = " + GL_Master.BgdtMas_Status + ";";
                //    //        systemAuditTrail.AdtTrl_DataDump += "VchMas_EnteredBy = " + GL_Master.VchMas_EnteredBy + ";";
                //    //        systemAuditTrail.AdtTrl_DataDump += "VchMas_EnteredDate = " + GL_Master.VchMas_EnteredDate + ";";
                //    //        systemAuditTrail.AdtTrl_DataDump += "VchMas_ApprovedBy = " + GL_Master.VchMas_ApprovedBy + ";";
                //    //        systemAuditTrail.AdtTrl_DataDump += "VchMas_ApprovedDate = " + GL_Master.VchMas_ApprovedDate + ";";
                //    //        systemAuditTrail.AdtTrl_DataDump += "SyncStatus = " + GL_Master.SyncStatus + ";";

                //    //        foreach (GL_VchrDetail BudgetDetail in BudgetDetailList)
                //    //        {
                //    //            systemAuditTrail.AdtTrl_DataDump += "║ VchDet_Id = " + BudgetDetail.VchDet_Id + ";";
                //    //            systemAuditTrail.AdtTrl_DataDump += "BgdtMas_Id = " + BudgetDetail.BgdtMas_Id + ";";
                //    //            systemAuditTrail.AdtTrl_DataDump += "ChrtAcc_Id = " + BudgetDetail.ChrtAcc_Id + ";";
                //    //            systemAuditTrail.AdtTrl_DataDump += "VchMas_DrAmount = " + BudgetDetail.VchMas_DrAmount + ";";
                //    //            systemAuditTrail.AdtTrl_DataDump += "VchMas_CrAmount = " + BudgetDetail.VchMas_CrAmount + ";";
                //    //            systemAuditTrail.AdtTrl_DataDump += "VchDet_Remarks = " + BudgetDetail.VchDet_Remarks + ";";
                //    //        }

                //    //        systemAuditTrail.AdtTrl_Date = DateTime.Now;
                //    //        objAuditTrail.SaveRecord(systemAuditTrail);
                //    //    }
                //    //}
                //    //// Audit Trail Section End

                //    //return PartialView("GridData");
                //    //string result = ViewData["BudgetId"].ToString(); //+ "|" + ViewData["BudgetCode"].ToString() + "|" + ViewData["SaveResult"].ToString();
                string[] rList = new string[3];
                rList[0] = BudgetMasterId;// ViewData["BudgetId"].ToString();
                rList[1] = BudgetMasterCode;// ViewData["BudgetCode"].ToString();
                rList[2] = li_ReturnValue.ToString();// ViewData["SaveResult"].ToString();
                System.Web.Script.Serialization.JavaScriptSerializer se = new System.Web.Script.Serialization.JavaScriptSerializer();
                string result = se.Serialize(rList);
                return result;
            }
            catch
            {
                ViewData["SaveResult"] = 0;
                //return PartialView("GridData");
                return "0";
            }

            //return li_ReturnValue.ToString();

        }

        public void EditBudget(String BudgetId)
        {
            var BudgetEntryRow = new DALBudgetEntry().GetAllMasterRecords().Where(c => c.BgdtMas_Id.Equals(BudgetId)).SingleOrDefault();
            if (BudgetEntryRow != null)
            {
                ViewData["BudgetCode"] = BudgetEntryRow.BgdtMas_Code;
                ViewData["BudgetId"] = BudgetEntryRow.BgdtMas_Id;
                ViewData["CurrentDate"] = Convert.ToDateTime(BudgetEntryRow.BgdtMas_Date).ToString("MM/dd/yyyy");
                ViewData["Status"] = BudgetEntryRow.BgdtMas_Status;
                ViewData["BudgetType"] = BudgetEntryRow.BgdtType_Id;
                ViewData["LocationId"] = BudgetEntryRow.Loc_Id;
                ViewData["Remarks"] = BudgetEntryRow.BgdtMas_Remarks;
                //var BudgetDetailRows = new DALBudgetEntry().GetAllDetailRecords().Where(c => c.BgdtMas_Id.Equals(BudgetId)).ToList().OrderBy(c => c.VchDet_Id).ToList();
                //ViewData["DetailRecords"] = BudgetDetailRows;
                ViewData["Edit"] = "OK";
            }
        }

    }
}
