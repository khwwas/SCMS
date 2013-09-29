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
            SETUP_Location lrow_Location = new SETUP_Location();
            String ls_Action = "Edit", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[3], ls_Data = new String[3];
            Int32 li_ReturnValue = 0;

            try
            {
                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Location") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_Location");
                        ls_Action = "Add";
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

                    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];

                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0 && IsAuditTrail == "1")
                    {
                        DALAuditLog objAuditLog = new DALAuditLog();

                        ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                        ls_Lable[0] = "Code";
                        ls_Lable[1] = "Title";
                        ls_Lable[2] = "Company";

                        ls_Data[0] = ps_Code;
                        ls_Data[1] = ps_Title;
                        ls_Data[2] = ps_CmpId;

                        objAuditLog.SaveRecord(2, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[3], ls_Data = new String[3];
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Location LocationRow = objDalLocation.GetAllLocation().Where(c => c.Loc_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalLocation.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Title";
                    ls_Lable[2] = "Company";

                    ls_Data[0] = LocationRow.Loc_Code;
                    ls_Data[1] = LocationRow.Loc_Title;
                    ls_Data[2] = LocationRow.Cmp_Id;

                    objAuditLog.SaveRecord(2, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
