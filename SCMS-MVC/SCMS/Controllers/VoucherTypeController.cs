using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class VoucherTypeController : Controller
    {

        DALVoucherType objDALVoucherType = new DALVoucherType();

        public ActionResult Index()
        {
            //ViewData["ddl_Locations"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Locations");
            return View("VoucherType");
        }

        public ActionResult SaveVoucherType(String Code, String Title, String Prefix, int CodeInitilization)
        {
            SETUP_VoucherType setupVoucherTypeRow = new SETUP_VoucherType();
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[4], ls_Data = new String[4];
            Int32 li_ReturnValue = 0;

            try
            {
                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_VoucherType") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_VoucherType");
                        ls_Action = "Add";
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    setupVoucherTypeRow.VchrType_Id = Code;
                    setupVoucherTypeRow.VchrType_Code = Code;
                    setupVoucherTypeRow.VchrType_Title = Title;
                    setupVoucherTypeRow.VchrType_Prefix = Prefix;
                    setupVoucherTypeRow.VchrType_CodeInitialization = CodeInitilization;
                    setupVoucherTypeRow.VchrType_SortOrder = 1;
                    setupVoucherTypeRow.VchrType_Active = 1;
                    li_ReturnValue = objDALVoucherType.SaveVoucherType(setupVoucherTypeRow);

                    ViewData["SaveResult"] = li_ReturnValue;

                    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0 && IsAuditTrail == "1")
                    {
                        DALAuditLog objAuditLog = new DALAuditLog();

                        ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                        ls_Lable[0] = "Code";
                        ls_Lable[1] = "Title";
                        ls_Lable[2] = "Prefix";
                        ls_Lable[3] = "Code Initialization";

                        ls_Data[0] = Code;
                        ls_Data[1] = Title;
                        ls_Data[2] = Prefix;
                        ls_Data[3] = CodeInitilization.ToString();

                        objAuditLog.SaveRecord(8, ls_UserId, ls_Action, ls_Lable, ls_Data);
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

        public ActionResult DeleteVoucherType(String VoucherTypeId)
        {
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[4], ls_Data = new String[4];
            Int32 li_ReturnValue = 0;

            try
            {
                sp_PopulateVoucherTypeListResult VoucherTypeRow = objDALVoucherType.PopulateData().Where(c => c.VchrType_Id.Equals(VoucherTypeId)).SingleOrDefault();

                li_ReturnValue = objDALVoucherType.DeleteVoucherTypeById(VoucherTypeId);
                ViewData["SaveResult"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Title";
                    ls_Lable[2] = "Prefix";
                    ls_Lable[3] = "Code Initialization";

                    ls_Data[0] = VoucherTypeRow.VchrType_Code;
                    ls_Data[1] = VoucherTypeRow.VchrType_Title;
                    ls_Data[2] = VoucherTypeRow.VchrType_Prefix;
                    ls_Data[3] = VoucherTypeRow.VchrType_CodeInitialization.ToString();

                    objAuditLog.SaveRecord(8, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
