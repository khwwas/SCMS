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
        // GET: /City/
        DALCity objDalCity = new DALCity();

        public ActionResult Index()
        {
            return View("City");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title)
        {
            SETUP_City lrow_City = new SETUP_City();
            String ls_Action = "Edit", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[3], ls_Data = new String[3];
            Int32 li_ReturnValue = 0;

            try
            {
                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_City") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_City");
                        ls_Action = "Add";
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

                    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                    // Save Audit Log
                    if (li_ReturnValue > 0 && IsAuditTrail == "1")
                    {
                        DALAuditLog objAuditLog = new DALAuditLog();

                        ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                        ls_Lable[0] = "Code";
                        ls_Lable[1] = "Title";
                        ls_Lable[2] = "Country";

                        ls_Data[0] = ps_Code;
                        ls_Data[1] = ps_Title;
                        ls_Data[2] = "00001";
                       
                        objAuditLog.SaveRecord(3, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
            String[] ls_Lable = new String[3], ls_Data = new String[3];
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_City CityRow = objDalCity.GetAllRecords().Where(c => c.City_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalCity.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Title";
                    ls_Lable[2] = "Country";

                    ls_Data[0] = CityRow.City_Code;
                    ls_Data[1] = CityRow.City_Title;
                    ls_Data[2] = CityRow.Cnty_Id;

                    objAuditLog.SaveRecord(3, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
