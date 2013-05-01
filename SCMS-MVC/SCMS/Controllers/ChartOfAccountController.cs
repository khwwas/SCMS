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

        public ActionResult SaveRecord(string ps_Id, string ps_Code, string ps_Title, Int32 pi_Level, Int32 pi_BudgetLevel, Int32 pi_Active,
                                       Int32 pi_Type, string ps_Nature, string ps_AccountNature, string ps_CodeBeforeEdit)
        {
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
                    }
                }

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
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
                li_ReturnValue = objDalChartOfAccount.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }
    }
}
