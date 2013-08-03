using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
namespace SCMS.Controllers
{
    public class CityController : Controller
    {
        //
        // GET: /Company/
        DALCity objDalCity = new DALCity();

        public ActionResult Index()
        {
            return View("City");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title)
        {
            Int32 li_ReturnValue = 0;

            String Action = "Add";
            if (!string.IsNullOrEmpty(ps_Code))
            {
                Action = "Edit";
            }

            try
            {
                SETUP_City lrow_City = new SETUP_City();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_City") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_City");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_City.City_Id = ps_Code;
                    lrow_City.City_Code = ps_Code;
                    lrow_City.City_Title = ps_Title;
                    lrow_City.Cnty_Id = "00001";
                    lrow_City.City_Active = 1;

                    li_ReturnValue = objDalCity.SaveRecord(lrow_City);
                    ViewData["SaveResult"] = li_ReturnValue;
                }

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 3;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.AdtTrl_Action = Action;
                        systemAuditTrail.AdtTrl_EntryId = ps_Code;
                        systemAuditTrail.AdtTrl_DataDump = "City_Id = " + lrow_City.City_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "City_Code = " + lrow_City.City_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cnty_Id = " + lrow_City.Cnty_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "City_Title = " + lrow_City.City_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "City_Active = " + lrow_City.City_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "City_SortOrder = " + lrow_City.City_SortOrder + ";";
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

        public ActionResult DeleteRecord(String _pId)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_City CityRow = objDalCity.GetAllRecords().Where(c => c.City_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalCity.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;


                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 3;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = _pId;
                        systemAuditTrail.AdtTrl_DataDump = "City_Id = " + CityRow.City_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "City_Code = " + CityRow.City_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cnty_Id = " + CityRow.Cnty_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "City_Title = " + CityRow.City_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "City_Active = " + CityRow.City_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "City_SortOrder = " + CityRow.City_SortOrder + ";";
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
