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

        // public ActionResult SaveVoucherType(String Code, String Title, String Prefix, String LocId)
        public ActionResult SaveVoucherType(String Code, String Title, String Prefix, int CodeInitilization)
        {
            Int32 VoucherTypeId = 0;

            try
            {
                SETUP_VoucherType setupVoucherTypeRow = new SETUP_VoucherType();

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_VoucherType") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_VoucherType");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    setupVoucherTypeRow.VchrType_Id = Code;
                    setupVoucherTypeRow.VchrType_Code = Code;
                    setupVoucherTypeRow.VchrType_Title = Title;
                    setupVoucherTypeRow.VchrType_Prefix = Prefix;
                    //setupVoucherTypeRow.Loc_Id = LocId;
                    setupVoucherTypeRow.VchrType_CodeInitialization = CodeInitilization;
                    setupVoucherTypeRow.VchrType_SortOrder = 1;
                    setupVoucherTypeRow.VchrType_Active = 1;

                    VoucherTypeId = objDALVoucherType.SaveVoucherType(setupVoucherTypeRow);
                    ViewData["SaveResult"] = VoucherTypeId;
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
            try
            {
                int result = objDALVoucherType.DeleteVoucherTypeById(VoucherTypeId);
                ViewData["result"] = result;
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }
    }
}
