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
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            ViewData["ddl_Bank"] = new SelectList(new DALBank().PopulateData(), "Bank_Id", "Bank_Title", "ddl_Bank");
            return View("BankAccount");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Location, String Bank, String Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_BankAccount lrow_BankAccount = new SETUP_BankAccount();

                String Action = "Add";
                if (!string.IsNullOrEmpty(ps_Code))
                {
                    Action = "Edit";
                }

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
                    lrow_BankAccount.Loc_Id = Location;
                    lrow_BankAccount.Bank_Id = Bank;
                    lrow_BankAccount.BankAcc_Active = 1;

                    li_ReturnValue = objDalBankAccount.SaveRecord(lrow_BankAccount);
                    ViewData["SaveResult"] = li_ReturnValue;

                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0)
                    {
                        string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                        if (IsAuditTrail == "1")
                        {
                            SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                            DALAuditTrail objAuditTrail = new DALAuditTrail();
                            systemAuditTrail.Scr_Id = 12;
                            systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                            systemAuditTrail.Loc_Id = lrow_BankAccount.Loc_Id;
                            systemAuditTrail.AdtTrl_Action = Action;
                            systemAuditTrail.AdtTrl_EntryId = ps_Code;
                            systemAuditTrail.AdtTrl_DataDump = "BankAcc_Id = " + lrow_BankAccount.BankAcc_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "BankAcc_Code = " + lrow_BankAccount.BankAcc_Code + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + lrow_BankAccount.Cmp_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + lrow_BankAccount.Loc_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Bank_Id = " + lrow_BankAccount.Bank_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "BankAcc_Title = " + lrow_BankAccount.BankAcc_Title + ";";
                            systemAuditTrail.AdtTrl_DataDump += "BankAcc_Active = " + lrow_BankAccount.BankAcc_Active + ";";
                            systemAuditTrail.AdtTrl_DataDump += "BankAcc_SortOrder = " + lrow_BankAccount.BankAcc_SortOrder + ";";
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
                SETUP_BankAccount BankRow = objDalBankAccount.GetAllRecords().Where(c => c.BankAcc_Id.Equals(_pId)).SingleOrDefault();
                li_ReturnValue = objDalBankAccount.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 12;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = BankRow.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = _pId;
                        systemAuditTrail.AdtTrl_DataDump = "BankAcc_Id = " + BankRow.BankAcc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "BankAcc_Code = " + BankRow.BankAcc_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + BankRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + BankRow.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Bank_Id = " + BankRow.Bank_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "BankAcc_Title = " + BankRow.BankAcc_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "BankAcc_Active = " + BankRow.BankAcc_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "BankAcc_SortOrder = " + BankRow.BankAcc_SortOrder + ";";
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
