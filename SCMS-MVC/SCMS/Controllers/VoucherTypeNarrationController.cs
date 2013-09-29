using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class VoucherTypeNarrationController : Controller
    {
        //
        // GET: /VoucherTypeNarration/

        public ActionResult Index()
        {
            ViewData["ddl_VoucherType"] = new SelectList(new DALVoucherType().GetAllData().Where(c => c.VchrType_Active == 1).OrderBy(c => c.VchrType_Title), "VchrType_Id", "VchrType_Title", "ddl_VoucherType");
            return View("VoucherTypeNarration");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title, String ps_CmpId)
        {
            DALVoucherTypeNarration objDal = new DALVoucherTypeNarration();
            SETUP_VoucherTypeNarration lrow_Data = new SETUP_VoucherTypeNarration();
            String ls_Action = "Edit", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[3], ls_Data = new String[3];
            Int32 li_ReturnValue = 0;

            try
            {
                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_VoucherTypeNarration") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_VoucherTypeNarration");
                        ls_Action = "Add";
                    }
                }

                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_Data.VchrTypeNarr_Id = ps_Code;
                    lrow_Data.VchrTypeNarr_Code = ps_Code;
                    lrow_Data.VchrTypeNarr_Title = ps_Title;
                    lrow_Data.VchrType_Id = ps_CmpId;
                    lrow_Data.VchrTypeNarr_Active = 1;

                    li_ReturnValue = objDal.SaveRecord(lrow_Data);
                    ViewData["SaveResult"] = li_ReturnValue;

                    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                    // Save Audit Log
                    if (li_ReturnValue > 0 && IsAuditTrail == "1")
                    {
                        DALAuditLog objAuditLog = new DALAuditLog();

                        ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                        ls_Lable[0] = "Code";
                        ls_Lable[1] = "Title";
                        ls_Lable[2] = "Voucher Type";

                        ls_Data[0] = ps_Code;
                        ls_Data[1] = ps_Title;
                        ls_Data[2] = ps_CmpId;

                        objAuditLog.SaveRecord(9, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
            DALVoucherTypeNarration objDal = new DALVoucherTypeNarration();
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[3], ls_Data = new String[3];
            Int32 li_ReturnValue = 0;

            try
            {
                sp_GetVoucherTypeNarrationListResult VoucherTypeNarrationRow = objDal.GetAllData().Where(c => c.VchrTypeNarr_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDal.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Title";
                    ls_Lable[2] = "Voucher Type";

                    ls_Data[0] = VoucherTypeNarrationRow.VchrTypeNarr_Code;
                    ls_Data[1] = VoucherTypeNarrationRow.VchrTypeNarr_Title;
                    ls_Data[2] = VoucherTypeNarrationRow.VchrType_Id;

                    objAuditLog.SaveRecord(9, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
