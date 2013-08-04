using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Text;

namespace SCMS.Controllers
{
    public class ChartOfAccountController : Controller
    {
        //
        // GET: /Company/
        DALChartOfAccount objDalChartOfAccount = new DALChartOfAccount();

        public ActionResult Index()
        {
            ViewData["ddl_Nature"] = new SelectList(new DALNature().PopulateData(), "Natr_Id", "Natr_Title");
            ViewData["ddl_AccNature"] = new SelectList(new DALAccountNature().PopulateData(), "AccNatr_Id", "AccNatr_Title");
            return View("ChartOfAccount");
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult SaveRecord(string ps_Id, string ps_Code, string ps_Title, Int32 pi_Level, Int32 pi_BudgetLevel, Int32 pi_Active,
        //Int32 pi_Type, string ps_Nature, string ps_AccountNature, string ps_CodeBeforeEdit)
        public string SaveRecord(IEnumerable<string> dataString)
        {
            String[] Row = dataString.Last().Split('║');
            string ps_Id = Row[0];
            string ps_Code = Row[1];
            string ps_Title = Row[2];
            int pi_Level = Row[3] != null ? Convert.ToInt32(Row[3]) : 0;
            int pi_BudgetLevel = Row[4] != null ? Convert.ToInt32(Row[4]) : 0;
            int pi_Active = Row[5] != null ? Convert.ToInt32(Row[5]) : 0;
            int pi_Type = Row[6] != null ? Convert.ToInt32(Row[6]) : 0;
            string ps_Nature = Row[7];
            string ps_AccountNature = Row[8];
            string ps_CodeBeforeEdit = Row[9];

            Int32 li_ReturnValue = 0;
            bool isEdit = false;
            if (!string.IsNullOrEmpty(ps_Id))
            {
                isEdit = true;
            }
            try
            {
                SETUP_ChartOfAccount lrow_ChartOfAccount = new SETUP_ChartOfAccount();

                if (String.IsNullOrEmpty(ps_Id))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_ChartOfAccount") == 1)
                    {
                        ps_Id = DALCommon.GetMaximumCode("SETUP_ChartOfAccount");
                    }
                }

                if (!String.IsNullOrEmpty(ps_Id))
                {
                    lrow_ChartOfAccount.ChrtAcc_Id = ps_Id;
                    lrow_ChartOfAccount.ChrtAcc_Code = ps_Code.Replace("-", "").Replace("_", "");

                    string[] AccCodeArr = ps_Code.Replace("_", "").Split('-');
                    string AccountCodeForDisplay = "";
                    foreach (string code in AccCodeArr)
                    {
                        AccountCodeForDisplay += (!string.IsNullOrEmpty(AccountCodeForDisplay) ? (!string.IsNullOrEmpty(code) ? "-" + code : code) : code);
                    }
                    lrow_ChartOfAccount.ChrtAcc_CodeDisplay = AccountCodeForDisplay.Trim();

                    lrow_ChartOfAccount.ChrtAcc_Title = ps_Title;
                    lrow_ChartOfAccount.ChrtAcc_Level = pi_Level;
                    lrow_ChartOfAccount.ChrtAcc_BudgetLevel = pi_BudgetLevel;
                    lrow_ChartOfAccount.ChrtAcc_Type = pi_Type;
                    lrow_ChartOfAccount.Natr_Id = ps_Nature;
                    //lrow_ChartOfAccount.AccNatr_Id = ps_AccountNature;
                    lrow_ChartOfAccount.ChrtAcc_Active = 1;
                    var ChartOfAccountCode = objDalChartOfAccount.GetAllRecords().Where(c => c.ChrtAcc_Code.Equals(lrow_ChartOfAccount.ChrtAcc_Code)).ToList();
                    if (isEdit == false && ChartOfAccountCode != null && ChartOfAccountCode.Count > 0)
                    {
                        ViewData["SaveResult"] = -1;
                    }
                    else if (isEdit == true && ps_Code.Replace("-", "").Replace("_", "") != ps_CodeBeforeEdit.Replace("-", "").Replace("_", "") && ChartOfAccountCode != null && ChartOfAccountCode.Count > 0)
                    {
                        ViewData["SaveResult"] = -1;
                    }
                    else
                    {
                        li_ReturnValue = objDalChartOfAccount.SaveRecord(lrow_ChartOfAccount);
                        ViewData["SaveResult"] = li_ReturnValue;

                        // Audit Trail Entry Section
                        if (li_ReturnValue > 0)
                        {
                            string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                            if (IsAuditTrail == "1")
                            {
                                SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                                DALAuditTrail objAuditTrail = new DALAuditTrail();
                                systemAuditTrail.Scr_Id = 10;
                                systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                                systemAuditTrail.Loc_Id = lrow_ChartOfAccount.Loc_Id;
                                systemAuditTrail.AdtTrl_Action = isEdit == true ? "Edit" : "Add";
                                systemAuditTrail.AdtTrl_EntryId = lrow_ChartOfAccount.ChrtAcc_Id;
                                systemAuditTrail.AdtTrl_DataDump = "ChrtAcc_Id = " + lrow_ChartOfAccount.ChrtAcc_Id + ";";
                                systemAuditTrail.AdtTrl_DataDump += "ChrtAcc_Code = " + lrow_ChartOfAccount.ChrtAcc_CodeDisplay + ";";
                                systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + lrow_ChartOfAccount.Cmp_Id + ";";
                                systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + lrow_ChartOfAccount.Loc_Id + ";";
                                systemAuditTrail.AdtTrl_DataDump += "Natr_Id = " + lrow_ChartOfAccount.Natr_Id + ";";
                                systemAuditTrail.AdtTrl_DataDump += "AccNatr_Id = " + lrow_ChartOfAccount.AccNatr_Id + ";";
                                systemAuditTrail.AdtTrl_DataDump += "ChrtAcc_Title = " + lrow_ChartOfAccount.ChrtAcc_Title + ";";
                                systemAuditTrail.AdtTrl_DataDump += "ChrtAcc_Type = " + lrow_ChartOfAccount.ChrtAcc_Type + ";";
                                systemAuditTrail.AdtTrl_DataDump += "ChrtAcc_Level = " + lrow_ChartOfAccount.ChrtAcc_Level + ";";
                                systemAuditTrail.AdtTrl_DataDump += "ChrtAcc_BudgetLevel = " + lrow_ChartOfAccount.ChrtAcc_BudgetLevel + ";";
                                systemAuditTrail.AdtTrl_Date = DateTime.Now;
                                objAuditTrail.SaveRecord(systemAuditTrail);
                            }
                        }
                        // Audit Trail Section End

                    }
                }
                string[] rList = new string[1];
                rList[0] = "";

                rList[0] += "<style type='text/css'>";
                rList[0] += "select";
                rList[0] += "{";
                rList[0] += "background: none;";
                rList[0] += "width: auto;";
                rList[0] += "padding: 0;";
                rList[0] += "margin: 0;";
                rList[0] += "border-radius: 0px;";
                rList[0] += "}";
                rList[0] += "input[type='text']";
                rList[0] += "{";
                rList[0] += "margin-bottom: 0;";
                rList[0] += "}";
                rList[0] += "</style>";
                rList[0] += "<table id='ChartOfAccountGrid' class='data display datatable'>";
                rList[0] += "<thead>";
                rList[0] += "<tr>";
                rList[0] += "<th style='width: 5%; display: none;'>";
                rList[0] += "Sr No.";
                rList[0] += "</th>";
                rList[0] += "<th style='width: 6%;'>";
                rList[0] += "Action";
                rList[0] += "</th>";
                rList[0] += "<th style='width: 20%;'>";
                rList[0] += "Code";
                rList[0] += "</th>";
                rList[0] += "<th style='width: 23%;'>";
                rList[0] += "Title";
                rList[0] += "</th>";
                rList[0] += "<th style='width: 10%;'>";
                rList[0] += "Level";
                rList[0] += "</th>";
                rList[0] += "<th style='width: 12%;'>";
                rList[0] += "Budget Level";
                rList[0] += "</th>";
                rList[0] += "<th style='width: 12%;'>";
                rList[0] += "Type";
                rList[0] += "</th>";
                rList[0] += "<th style='width: 12%;'>";
                rList[0] += "Nature";
                rList[0] += "</th>";
                rList[0] += "</tr>";
                rList[0] += "</thead>";
                rList[0] += "<tbody>";
                var lList_Data = new SCMSDataLayer.DALChartOfAccount().GetAllRecords();
                var IList_AccountNature = new SCMSDataLayer.DALAccountNature().GetAllRecords();
                var IList_Nature = new SCMSDataLayer.DALNature().GetAllRecords();
                if (lList_Data != null && lList_Data.Count > 0)
                {
                    int count = 0;
                    foreach (SCMSDataLayer.DB.SETUP_ChartOfAccount lRow_Data in lList_Data)
                    {
                        string tempValue = "";
                        string Title = lRow_Data.ChrtAcc_Title.Replace("'", "&#39");
                        for (int index = 0; index < lRow_Data.ChrtAcc_Code.Length; index++)
                        {
                            if (index == 2 || index == 5 || index == 9 || index == 14 || index == 19 || index == 24)
                            {
                                tempValue += "-" + lRow_Data.ChrtAcc_Code[index];
                            }
                            else
                            {
                                tempValue += lRow_Data.ChrtAcc_Code[index];
                            }
                        }
                        lRow_Data.ChrtAcc_Code = tempValue;
                        count++;
                        rList[0] += "<tr class='odd' style='line-height: 15px;'>";
                        rList[0] += "<td style='display: none;'>";
                        rList[0] += count;
                        rList[0] += "</td>";
                        rList[0] += "<td>";
                        rList[0] += " <div onclick=\"javascript:return EditRecord('" + lRow_Data.ChrtAcc_Id + "', '" + lRow_Data.ChrtAcc_Code + "')\" ";
                        rList[0] += " style='width: 22px; padding-right: 5px; float: left; cursor: pointer;'>";
                        rList[0] += "<img alt='Edit' src='../../img/edit.png' style='width: 22px; vertical-align: middle' />";
                        rList[0] += "</div>";
                        rList[0] += "<div onclick=\"javascript:return DeleteRecord('" + lRow_Data.ChrtAcc_Id + "')\" style='width: 22px;";
                        rList[0] += "float: left; cursor: pointer;'>";
                        rList[0] += "<img alt='Delete' src='../../img/delete.png' style='width: 22px; vertical-align: middle' />";
                        rList[0] += "</div>";
                        rList[0] += "</td>";
                        rList[0] += "<td id='txt_Code" + lRow_Data.ChrtAcc_Id + "' style='vertical-align: middle;'>";
                        if (lRow_Data.ChrtAcc_Level == 2)
                        {
                            lRow_Data.ChrtAcc_Code = "&nbsp; " + lRow_Data.ChrtAcc_Code;
                        }

                        else if (lRow_Data.ChrtAcc_Level == 3)
                        {
                            lRow_Data.ChrtAcc_Code = "&nbsp; &nbsp; " + lRow_Data.ChrtAcc_Code;
                        }

                        else if (lRow_Data.ChrtAcc_Level == 4)
                        {
                            lRow_Data.ChrtAcc_Code = "&nbsp; &nbsp; &nbsp; " + lRow_Data.ChrtAcc_Code;
                        }

                        else if (lRow_Data.ChrtAcc_Level == 5)
                        {
                            lRow_Data.ChrtAcc_Code = "&nbsp; &nbsp; &nbsp; &nbsp; " + lRow_Data.ChrtAcc_Code;
                        }

                        else if (lRow_Data.ChrtAcc_Level == 6)
                        {
                            lRow_Data.ChrtAcc_Code = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" + lRow_Data.ChrtAcc_Code;
                        }

                        rList[0] += lRow_Data.ChrtAcc_Code;
                        rList[0] += "</td>";
                        rList[0] += "<td id='txt_Title" + lRow_Data.ChrtAcc_Id + "' style='vertical-align: middle;'>";
                        if (lRow_Data.ChrtAcc_Level == 2)
                        {
                            lRow_Data.ChrtAcc_Title = "&nbsp; " + lRow_Data.ChrtAcc_Title;
                        }

                        else if (lRow_Data.ChrtAcc_Level == 3)
                        {
                            lRow_Data.ChrtAcc_Title = "&nbsp; &nbsp; " + lRow_Data.ChrtAcc_Title;
                        }

                        else if (lRow_Data.ChrtAcc_Level == 4)
                        {
                            lRow_Data.ChrtAcc_Title = "&nbsp; &nbsp; &nbsp; " + lRow_Data.ChrtAcc_Title;
                        }

                        else if (lRow_Data.ChrtAcc_Level == 5)
                        {
                            lRow_Data.ChrtAcc_Title = "&nbsp; &nbsp; &nbsp; &nbsp; " + lRow_Data.ChrtAcc_Title;
                        }

                        else if (lRow_Data.ChrtAcc_Level == 6)
                        {
                            lRow_Data.ChrtAcc_Title = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" + lRow_Data.ChrtAcc_Title;
                        }

                        rList[0] += Title;
                        rList[0] += "</td>";
                        rList[0] += "<td id='txt_AccountLevel" + lRow_Data.ChrtAcc_Id + "' style='vertical-align: middle;'>";
                        rList[0] += lRow_Data.ChrtAcc_Level;
                        rList[0] += "<input type='hidden' id='ChrtAcc_Level" + lRow_Data.ChrtAcc_Id + "' value='" + lRow_Data.ChrtAcc_Level + "' />";
                        rList[0] += "</td>";
                        rList[0] += "<td id='txt_AccountBudgetLevel'" + lRow_Data.ChrtAcc_Id + "' style='vertical-align: middle;'>";
                        rList[0] += lRow_Data.ChrtAcc_BudgetLevel;
                        rList[0] += "<input type='hidden' id='ChrtAcc_BudgetLevel" + lRow_Data.ChrtAcc_Id + "' value='" + lRow_Data.ChrtAcc_BudgetLevel + "' />";
                        rList[0] += "</td>";
                        rList[0] += "<td id='txt_AccountType" + lRow_Data.ChrtAcc_Id + "' style='vertical-align: middle;'>";
                        if (lRow_Data.ChrtAcc_Type == 1)
                        {
                            rList[0] += "Group";
                        }
                        else
                        {
                            rList[0] += "Detail";
                        }
                        rList[0] += "<input type='hidden' id='ChrtAcc_Type" + lRow_Data.ChrtAcc_Id + "' value='" + lRow_Data.ChrtAcc_Type + "' />";
                        rList[0] += "</td>";
                        rList[0] += "<td id='txt_NatureId" + lRow_Data.ChrtAcc_Id + "' style='vertical-align: middle;'>";
                        String Nature = IList_Nature.Where(c => c.Natr_Id.Equals(lRow_Data.Natr_Id)).SingleOrDefault().Natr_Title;
                        if (Nature == "None")
                        {
                            Nature = "";
                        }
                        rList[0] += Nature;

                        rList[0] += "<input type='hidden' id='Natr_Id" + lRow_Data.ChrtAcc_Id + "' value='" + lRow_Data.Natr_Id + "' />";
                        rList[0] += "</td>";
                        rList[0] += "</tr>";
                    }
                }

                rList[0] += "</tbody>";
                rList[0] += "</table>";
                rList[0] += "<input type='hidden' id='SaveResult' value='" + ViewData["SaveResult"] + "' />";

                System.Web.Script.Serialization.JavaScriptSerializer se = new System.Web.Script.Serialization.JavaScriptSerializer();
                string result = se.Serialize(rList);
                return result;
            }
            catch
            {
                return "";
            }
        }

        [HttpPost]
        public ActionResult ImportData(HttpPostedFileBase txt_File)
        {
            try
            {
                String Path = Server.MapPath("~/UploadedDocuments/");
                if (txt_File != null)
                {
                    if (System.IO.File.Exists(Path + txt_File.FileName))
                    {
                        System.IO.File.Delete(Path + txt_File.FileName);
                    }
                    txt_File.SaveAs(Path + txt_File.FileName);
                    return RedirectToAction("SaveChartOfAccountsFromFile", new { FileName = txt_File.FileName });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult SaveChartOfAccountsFromFile(String FileName)
        {

            try
            {
                Int32 li_ReturnValue = 0;
                String Path = Server.MapPath("~/UploadedDocuments/");

                string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + FileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";
                using (OleDbConnection conn = new OleDbConnection(constr))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Open();
                    String Query = "Select * from [Sheet1$]";
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(Query, conn);
                    da.Fill(dt);
                    conn.Close();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            SETUP_ChartOfAccount lrow_ChartOfAccount = new SETUP_ChartOfAccount();
                            if (row["ChrtAcc_Code"] != null && !String.IsNullOrEmpty(row["ChrtAcc_Code"].ToString()) && row["ChrtAcc_Title"] != null && !String.IsNullOrEmpty(row["ChrtAcc_Title"].ToString()))
                            {
                                lrow_ChartOfAccount.ChrtAcc_Id = row["ChrtAcc_Code"].ToString();
                                lrow_ChartOfAccount.ChrtAcc_Code = row["ChrtAcc_Code"].ToString();
                                if (row["Cmp_Id"] != null && row["Cmp_Id"].ToString() != "")
                                {
                                    lrow_ChartOfAccount.Cmp_Id = row["Cmp_Id"].ToString();
                                }
                                if (row["Loc_Id"] != null && row["Loc_Id"].ToString() != "")
                                {
                                    lrow_ChartOfAccount.Loc_Id = row["Loc_Id"].ToString();
                                }
                                if (row["Natr_Id"] != null && row["Natr_Id"].ToString() != "")
                                {
                                    lrow_ChartOfAccount.Natr_Id = row["Natr_Id"].ToString();
                                }
                                if (row["AccNatr_Id"] != null && row["AccNatr_Id"].ToString() != "")
                                {
                                    lrow_ChartOfAccount.AccNatr_Id = row["AccNatr_Id"].ToString();
                                }
                                lrow_ChartOfAccount.ChrtAcc_Title = row["ChrtAcc_Title"].ToString();
                                if (row["ChrtAcc_Type"] != null && row["ChrtAcc_Type"].ToString() != "")
                                {
                                    lrow_ChartOfAccount.ChrtAcc_Type = Convert.ToInt32(row["ChrtAcc_Type"]);
                                }
                                if (row["ChrtAcc_Level"] != null && row["ChrtAcc_Level"].ToString() != "")
                                {
                                    lrow_ChartOfAccount.ChrtAcc_Level = Convert.ToInt32(row["ChrtAcc_Level"]);
                                }
                                if (row["ChrtAcc_BudgetLevel"] != null && row["ChrtAcc_BudgetLevel"].ToString() != "")
                                {
                                    lrow_ChartOfAccount.ChrtAcc_BudgetLevel = Convert.ToInt32(row["ChrtAcc_BudgetLevel"]);
                                }
                                lrow_ChartOfAccount.ChrtAcc_Active = 1;
                                li_ReturnValue = objDalChartOfAccount.SaveRecord(lrow_ChartOfAccount);
                            }
                        }
                    }
                }
                System.IO.File.Delete(Path + FileName);
                ViewData["SaveResult"] = li_ReturnValue;
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult DeleteRecord(String _pId)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_ChartOfAccount ChartOfAccountRow = objDalChartOfAccount.GetAllRecords().Where(c => c.ChrtAcc_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalChartOfAccount.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 10;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = ChartOfAccountRow.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = ChartOfAccountRow.ChrtAcc_Id;
                        systemAuditTrail.AdtTrl_DataDump = "ChrtAcc_Id = " + ChartOfAccountRow.ChrtAcc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "ChrtAcc_Code = " + ChartOfAccountRow.ChrtAcc_CodeDisplay + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + ChartOfAccountRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + ChartOfAccountRow.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Natr_Id = " + ChartOfAccountRow.Natr_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "AccNatr_Id = " + ChartOfAccountRow.AccNatr_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "ChrtAcc_Title = " + ChartOfAccountRow.ChrtAcc_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "ChrtAcc_Type = " + ChartOfAccountRow.ChrtAcc_Type + ";";
                        systemAuditTrail.AdtTrl_DataDump += "ChrtAcc_Level = " + ChartOfAccountRow.ChrtAcc_Level + ";";
                        systemAuditTrail.AdtTrl_DataDump += "ChrtAcc_BudgetLevel = " + ChartOfAccountRow.ChrtAcc_BudgetLevel + ";";
                        systemAuditTrail.AdtTrl_Date = DateTime.Now;
                        objAuditTrail.SaveRecord(systemAuditTrail);
                    }
                }
                // Audit Trail Section End

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }
    }
}
