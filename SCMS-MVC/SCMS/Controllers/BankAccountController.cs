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
        //
        // GET: /BankAccount/
        DALBankAccount objDalBankAccount = new DALBankAccount();
        public ActionResult Index()
        {
            ViewData["ddl_Company"] = new SelectList(new DALCompany().PopulateData(), "Cmp_Id", "Cmp_Title", "ddl_Company");
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            ViewData["ddl_Bank"] = new SelectList(new DALBank().PopulateData(), "Bank_Id", "Bank_Title", "ddl_Bank");
            return View("BankAccount");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Comapany, String Location, String Bank, String Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_BankAccount lrow_BankAccount = new SETUP_BankAccount();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_BankAccount") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_BankAccount");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_BankAccount.BankAcc_Id = ps_Code;
                    lrow_BankAccount.BankAcc_Id = ps_Code;
                    lrow_BankAccount.BankAcc_Title = Title;
                    lrow_BankAccount.Cmp_Id = Comapany;
                    lrow_BankAccount.Loc_Id = Location;
                    lrow_BankAccount.Bank_Id = Bank;
                    lrow_BankAccount.BankAcc_Active = 1;

                    li_ReturnValue = objDalBankAccount.SaveRecord(lrow_BankAccount);
                    ViewData["SaveResult"] = li_ReturnValue;
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
            Int32 li_ReturnValue = 0;

            try
            {
                li_ReturnValue = objDalBankAccount.DeleteRecordById(_pId);
                ViewData["DeleteResult"] = li_ReturnValue;

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

    }
}
