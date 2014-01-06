using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class BankAccountController : Controller
    {
        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "");
            ViewData["ddl_Bank"] = new SelectList(new DALBank().PopulateData(), "Bank_Id", "Bank_Title", "");

            var ChartOfAccounts = new DALChartOfAccount().GetChartOfAccountForDropDown(1,"''");
            SETUP_ChartOfAccount SelectChartOfAccount = new SETUP_ChartOfAccount();
            SelectChartOfAccount.ChrtAcc_Id = "0";
            SelectChartOfAccount.ChrtAcc_Title = "";
            ChartOfAccounts.Insert(0, SelectChartOfAccount);
            ViewData["ddl_AccountCode"] = new SelectList(ChartOfAccounts, "ChrtAcc_Id", "ChrtAcc_Title", "");

            return View("BankAccount");
        }

        public ActionResult SaveRecord(String ps_Code, String Location, String Bank, String Title, String AccountCode)
        {
            DALBankAccount objDalBankAccount = new DALBankAccount();
            SETUP_BankAccount lrow_BankAccount = new SETUP_BankAccount();
            String ls_Action = "Edit", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[5], ls_Data = new String[5];
            Int32 li_ReturnValue = 0;

            try
            {
                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_BankAccount") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_BankAccount");
                        ls_Action = "Add";
                    }
                }

                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_BankAccount.BankAcc_Id = ps_Code;
                    lrow_BankAccount.BankAcc_Id = ps_Code;
                    lrow_BankAccount.BankAcc_Title = Title;
                    lrow_BankAccount.Loc_Id = Location;
                    lrow_BankAccount.Bank_Id = Bank;
                    lrow_BankAccount.ChrtAcc_Id = AccountCode;
                    lrow_BankAccount.BankAcc_Active = 1;

                    li_ReturnValue = objDalBankAccount.SaveRecord(lrow_BankAccount);
                    ViewData["SaveResult"] = li_ReturnValue;

                    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];

                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0 && IsAuditTrail == "1")
                    {
                        DALAuditLog objAuditLog = new DALAuditLog();

                        ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                        ls_Lable[0] = "Code";
                        ls_Lable[1] = "Location";
                        ls_Lable[2] = "Bank";
                        ls_Lable[3] = "Title";
                        ls_Lable[4] = "Account Code";

                        ls_Data[0] = ps_Code;
                        ls_Data[1] = Location;
                        ls_Data[2] = Bank;
                        ls_Data[3] = Title;
                        ls_Data[4] = AccountCode;

                        objAuditLog.SaveRecord(12, ls_UserId, ls_Action, ls_Lable, ls_Data);
                    }
                }
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteRecord(String _pId)
        {
            DALBankAccount objDalBankAccount = new DALBankAccount();
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[5], ls_Data = new String[5];
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_BankAccount BankRow = objDalBankAccount.GetAllRecords().Where(c => c.BankAcc_Id.Equals(_pId)).SingleOrDefault();
                li_ReturnValue = objDalBankAccount.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Location";
                    ls_Lable[2] = "Bank";
                    ls_Lable[3] = "Title";
                    ls_Lable[4] = "Account Code";

                    ls_Data[0] = BankRow.BankAcc_Code;
                    ls_Data[1] = BankRow.Loc_Id;
                    ls_Data[2] = BankRow.Bank_Id;
                    ls_Data[3] = BankRow.BankAcc_Title;
                    ls_Data[4] = BankRow.ChrtAcc_Id;

                    objAuditLog.SaveRecord(12, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
