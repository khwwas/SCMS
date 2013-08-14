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

                String Action = "Add";
                if (!string.IsNullOrEmpty(ps_Code))
                {
                    Action = "Edit";
                }

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
                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0)
                    {
                        string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                        if (IsAuditTrail == "1")
                        {
                            SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                            DALAuditTrail objAuditTrail = new DALAuditTrail();
                            systemAuditTrail.Scr_Id = 11;
                            systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                            systemAuditTrail.Loc_Id = lrow_Bank.Loc_Id;
                            systemAuditTrail.AdtTrl_Action = Action;
                            systemAuditTrail.AdtTrl_EntryId = ps_Code;
                            systemAuditTrail.AdtTrl_DataDump = "Bank_Id = " + lrow_Bank.Bank_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Bank_Code = " + lrow_Bank.Bank_Code + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + lrow_Bank.Cmp_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + lrow_Bank.Loc_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Bank_Title = " + lrow_Bank.Bank_Title + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Bank_Active = " + lrow_Bank.Bank_Active + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Bank_SortOrder = " + lrow_Bank.Bank_SortOrder + ";";
                            systemAuditTrail.AdtTrl_Date = DateTime.Now;
                            objAuditTrail.SaveRecord(systemAuditTrail);
                        }
                    }
                    // Audit Trail Section End

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
                SETUP_Bank BankRow = objDalBank.GetAllRecords().Where(c => c.Bank_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalBank.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 11;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = BankRow.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = _pId;
                        systemAuditTrail.AdtTrl_DataDump = "Bank_Id = " + BankRow.Bank_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Bank_Code = " + BankRow.Bank_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + BankRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + BankRow.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Bank_Title = " + BankRow.Bank_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Bank_Active = " + BankRow.Bank_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Bank_SortOrder = " + BankRow.Bank_SortOrder + ";";
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
