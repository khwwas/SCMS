using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

using System.IO;
using System.Web.Script.Serialization;

namespace SCMS.Controllers
{
    public class SupplierTypeController : Controller
    {
        //
        // GET: /Supplier Type/
        DALSupplierType objDalSupplierType = new DALSupplierType();

        public ActionResult Index()
        {
            return View("SupplierType");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title)
        {
            SETUP_SupplierType lrow_SupplierType = new SETUP_SupplierType();
            String ls_Action = "Edit", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[2], ls_Data = new String[2];
            Int32 li_ReturnValue = 0;

            try
            {
                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_SupplierType") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_SupplierType");
                        ls_Action = "Add";
                    }
                }

                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_SupplierType.SuppType_Id = ps_Code;
                    lrow_SupplierType.SuppType_Code = ps_Code;
                    lrow_SupplierType.SuppType_Title = ps_Title;
                    lrow_SupplierType.SuppType_Active = 1;

                    li_ReturnValue = objDalSupplierType.SaveRecord(lrow_SupplierType);
                    ViewData["SaveResult"] = li_ReturnValue;

                    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                    // Save Audit Log
                    if (li_ReturnValue > 0 && IsAuditTrail == "1")
                    {
                        DALAuditLog objAuditLog = new DALAuditLog();

                        ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                        ls_Lable[0] = "Code";
                        ls_Lable[1] = "Title";

                        ls_Data[0] = ps_Code;
                        ls_Data[1] = ps_Title;

                        objAuditLog.SaveRecord(4, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
                SETUP_SupplierType SupplierTypeRow = objDalSupplierType.GetAllSupplierType().Where(c => c.SuppType_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalSupplierType.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Title";
                    
                    ls_Data[0] = SupplierTypeRow.SuppType_Code;
                    ls_Data[1] = SupplierTypeRow.SuppType_Title;
                   
                    objAuditLog.SaveRecord(4, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
