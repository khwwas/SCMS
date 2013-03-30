using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class BankController : Controller
    {
        //
        // GET: /Bank/
        DALBank objDalBank = new DALBank();
        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            return View("Bank");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Location, String Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Bank lrow_Bank = new SETUP_Bank();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Bank") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_Bank");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_Bank.Bank_Id = ps_Code;
                    lrow_Bank.Bank_Code = ps_Code;
                    lrow_Bank.Bank_Title = Title;
                    lrow_Bank.Loc_Id = Location;
                    lrow_Bank.Bank_Active = 1;

                    li_ReturnValue = objDalBank.SaveRecord(lrow_Bank);
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
                li_ReturnValue = objDalBank.DeleteRecordById(_pId);
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
