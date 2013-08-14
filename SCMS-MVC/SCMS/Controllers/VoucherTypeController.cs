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

                    // Audit Trail Entry Section
                    if (VoucherTypeId > 0)
                    {
                        string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                        if (IsAuditTrail == "1")
                        {
                            SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                            DALAuditTrail objAuditTrail = new DALAuditTrail();
                            systemAuditTrail.Scr_Id = 8;
                            systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                            systemAuditTrail.Loc_Id = setupVoucherTypeRow.Loc_Id;
                            systemAuditTrail.AdtTrl_Action = isEdit == true ? "Edit" : "Add";
                            systemAuditTrail.AdtTrl_EntryId = Code;
                            systemAuditTrail.AdtTrl_DataDump = "VchrType_Id = " + setupVoucherTypeRow.VchrType_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "VchrType_Code = " + setupVoucherTypeRow.VchrType_Code + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + setupVoucherTypeRow.Cmp_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + setupVoucherTypeRow.Loc_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "VchrType_Title = " + setupVoucherTypeRow.VchrType_Title + ";";
                            systemAuditTrail.AdtTrl_DataDump += "VchrType_Prefix = " + setupVoucherTypeRow.VchrType_Prefix + ";";
                            systemAuditTrail.AdtTrl_DataDump += "VchrType_Active = " + setupVoucherTypeRow.VchrType_Active + ";";
                            systemAuditTrail.AdtTrl_DataDump += "VchrType_SortOrder = " + setupVoucherTypeRow.VchrType_SortOrder + ";";
                            systemAuditTrail.AdtTrl_DataDump += "VchrType_CodeInitialization = " + setupVoucherTypeRow.VchrType_CodeInitialization + ";";
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


        public ActionResult DeleteVoucherType(String VoucherTypeId)
        {
            Int32 li_ReturnValue = 0;
            try
            {
                sp_PopulateVoucherTypeListResult VoucherTypeRow = objDALVoucherType.PopulateData().Where(c => c.VchrType_Id.Equals(VoucherTypeId)).SingleOrDefault();

                li_ReturnValue = objDALVoucherType.DeleteVoucherTypeById(VoucherTypeId);
                ViewData["SaveResult"] = li_ReturnValue;

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 8;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = VoucherTypeRow.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = VoucherTypeId;
                        systemAuditTrail.AdtTrl_DataDump = "VchrType_Id = " + VoucherTypeRow.VchrType_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrType_Code = " + VoucherTypeRow.VchrType_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + VoucherTypeRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + VoucherTypeRow.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrType_Title = " + VoucherTypeRow.VchrType_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrType_Prefix = " + VoucherTypeRow.VchrType_Prefix + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrType_Active = " + VoucherTypeRow.VchrType_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrType_SortOrder = " + VoucherTypeRow.VchrType_SortOrder + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrType_CodeInitialization = " + VoucherTypeRow.VchrType_CodeInitialization + ";";
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
