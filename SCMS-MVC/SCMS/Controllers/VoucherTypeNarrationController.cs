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
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_VoucherTypeNarration lrow_Data = new SETUP_VoucherTypeNarration();

                String Action = "Add";
                if (!string.IsNullOrEmpty(ps_Code))
                {
                    Action = "Edit";
                }

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_VoucherTypeNarration") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_VoucherTypeNarration");
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
                }

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 9;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        //systemAuditTrail.Loc_Id = lrow_Data.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = Action;
                        systemAuditTrail.AdtTrl_EntryId = ps_Code;
                        systemAuditTrail.AdtTrl_DataDump = "VchrTypeNarr_Id = " + lrow_Data.VchrTypeNarr_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrTypeNarr_Code = " + lrow_Data.VchrTypeNarr_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrType_Id = " + lrow_Data.VchrType_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrTypeNarr_Title = " + lrow_Data.VchrTypeNarr_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrTypeNarr_Active = " + lrow_Data.VchrTypeNarr_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrTypeNarr_SortOrder = " + lrow_Data.VchrTypeNarr_SortOrder + ";";
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
            DALVoucherTypeNarration objDal = new DALVoucherTypeNarration();
            Int32 li_ReturnValue = 0;

            try
            {
                sp_GetVoucherTypeNarrationListResult VoucherTypeNarrationRow = objDal.GetAllData().Where(c => c.VchrTypeNarr_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDal.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 9;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = VoucherTypeNarrationRow.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = _pId;
                        systemAuditTrail.AdtTrl_DataDump = "VchrTypeNarr_Id = " + VoucherTypeNarrationRow.VchrTypeNarr_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrTypeNarr_Code = " + VoucherTypeNarrationRow.VchrTypeNarr_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrType_Id = " + VoucherTypeNarrationRow.VchrType_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrTypeNarr_Title = " + VoucherTypeNarrationRow.VchrTypeNarr_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrTypeNarr_Active = " + VoucherTypeNarrationRow.VchrTypeNarr_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrTypeNarr_SortOrder = " + VoucherTypeNarrationRow.VchrTypeNarr_SortOrder + ";";
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
