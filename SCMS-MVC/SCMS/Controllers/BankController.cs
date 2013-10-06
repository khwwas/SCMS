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

        public ActionResult SaveRecord(String ps_Code, String Title)
        {
            SETUP_Bank lrow_Bank = new SETUP_Bank();
            String ls_Action = "Edit", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[2], ls_Data = new String[2];
            Int32 li_ReturnValue = 0;

            try
            {

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Bank") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_Bank");
                        ls_Action = "Add";
                    }
                }

                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_Bank.Bank_Id = ps_Code;
                    lrow_Bank.Bank_Code = ps_Code;
                    lrow_Bank.Bank_Title = Title;
                    //lrow_Bank.Loc_Id = Location;
                    lrow_Bank.Bank_Active = 1;

                    li_ReturnValue = objDalBank.SaveRecord(lrow_Bank);
                    ViewData["SaveResult"] = li_ReturnValue;

                    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                    // Save Audit Log
                    if (li_ReturnValue > 0 && IsAuditTrail == "1")
                    {
                        DALAuditLog objAuditLog = new DALAuditLog();

                        ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                        ls_Lable[0] = "Code";
                        ls_Lable[1] = "Title";
                        //ls_Lable[2] = "Location";

                        ls_Data[0] = ps_Code;
                        ls_Data[1] = Title;
                        //ls_Data[2] = Location;

                        objAuditLog.SaveRecord(11, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[2], ls_Data = new String[2];
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Bank BankRow = objDalBank.GetAllRecords().Where(c => c.Bank_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalBank.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Title";
                    //ls_Lable[2] = "Location";

                    ls_Data[0] = BankRow.Bank_Code;
                    ls_Data[1] = BankRow.Bank_Title;
                    //ls_Data[2] = BankRow.Loc_Id;

                    objAuditLog.SaveRecord(11, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
