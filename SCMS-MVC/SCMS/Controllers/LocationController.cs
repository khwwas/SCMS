using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class LocationController : Controller
    {
        //
        // GET: /Location/
        DALLocation objDalLocation = new DALLocation();

        public ActionResult Index()
        {
            ViewData["ddl_Company"] = new SelectList(new DALCompany().PopulateData(), "Cmp_Id", "Cmp_Title", "ddl_Company");
            return View("Location");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title, String ps_CmpId)
        {
            bool isEdit = true;

            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Location lrow_Location = new SETUP_Location();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    isEdit = false;
                    if (DALCommon.AutoCodeGeneration("SETUP_Location") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_Location");
                    }
                }

                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_Location.Loc_Id = ps_Code;
                    lrow_Location.Loc_Code = ps_Code;
                    lrow_Location.Loc_Title = ps_Title;
                    lrow_Location.Cmp_Id = ps_CmpId;
                    lrow_Location.Loc_Active = 1;

                    li_ReturnValue = objDalLocation.SaveRecord(lrow_Location);
                    ViewData["SaveResult"] = li_ReturnValue;

                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0)
                    {
                        string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                        if (IsAuditTrail == "1")
                        {
                            SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                            DALAuditTrail objAuditTrail = new DALAuditTrail();
                            systemAuditTrail.Scr_Id = 2;
                            systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                            systemAuditTrail.AdtTrl_Action = isEdit == true ? "Edit" : "Add";
                            systemAuditTrail.AdtTrl_EntryId = ps_Code;
                            systemAuditTrail.AdtTrl_DataDump = "Loc_Id = " + lrow_Location.Loc_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Loc_Code = " + lrow_Location.Loc_Code + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + lrow_Location.Cmp_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Loc_Title = " + lrow_Location.Loc_Title + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Loc_Active = " + lrow_Location.Loc_Active + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Loc_SortOrder = " + lrow_Location.Loc_SortOrder + ";";
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
                SETUP_Location LocationRow = objDalLocation.GetAllLocation().Where(c => c.Loc_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalLocation.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;


                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 2;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = _pId;
                        systemAuditTrail.AdtTrl_DataDump = "Loc_Id = " + LocationRow.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Code = " + LocationRow.Loc_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + LocationRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Title = " + LocationRow.Loc_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Active = " + LocationRow.Loc_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_SortOrder = " + LocationRow.Loc_SortOrder + ";";
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
