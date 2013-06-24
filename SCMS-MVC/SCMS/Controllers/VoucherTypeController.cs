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
                bool isEdit = true;
                SETUP_VoucherType setupVoucherTypeRow = new SETUP_VoucherType();

                if (String.IsNullOrEmpty(Code))
                {
                    isEdit = false;
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

                    //if (isEdit == false && VoucherTypeId > 0)
                    //{
                    //    Security_UserVoucherType userVoucherTypeRow = new Security_UserVoucherType();
                    //    userVoucherTypeRow.UserGrp_Id = DALCommon.UserGroupId;
                    //    if (DALCommon.UserLoginId != null)
                    //    {
                    //        userVoucherTypeRow.User_Id = DALCommon.UserLoginId;
                    //    }
                    //    userVoucherTypeRow.VchrType_Id = Code;
                    //    DALUserMenuRights objUserMenuRights = new DALUserMenuRights();
                    //    objUserMenuRights.SetUserVoucherTypes(userVoucherTypeRow);
                    //}

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
            Int32 li_ReturnValue = 0;
            try
            {
                li_ReturnValue = objDALVoucherType.DeleteVoucherTypeById(VoucherTypeId);
                ViewData["SaveResult"] = li_ReturnValue;
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }
    }
}
