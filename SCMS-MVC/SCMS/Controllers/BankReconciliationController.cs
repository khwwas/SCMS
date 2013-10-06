using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class BankReconciliationController : Controller
    {
        //
        // GET: /BankReconciliation/
        public ActionResult Index()
        {
            return View("BankReconciliation");
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

     }
}
