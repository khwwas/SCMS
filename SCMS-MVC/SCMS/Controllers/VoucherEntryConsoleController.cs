using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class VoucherEntryConsoleController : Controller
    {
        //
        // GET: /Voucher/
        public ActionResult Index()
        {
            //ViewData["ddl_Location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            //ViewData["ddl_VoucherType"] = new SelectList(new DALVoucherType().PopulateData(), "VchrType_Id", "VchrType_Title", "ddl_VoucherType");

            return View("VoucherEntryConsole");
        }

        public ActionResult Search(String ps_Location, String ps_VoucherType, String ps_DateFrom, String ps_DateTo)
        {
            DALVoucherEntry objDal = new DALVoucherEntry();
            int li_AllLocation = -1, li_AllVoucherType = -1, li_AllDate = -1;

            try
            {
                if (ps_Location == null || ps_Location == "" || ps_Location == "0")
                {
                    li_AllLocation = 1;
                }
                else
                {
                    li_AllLocation = 0;
                }

                if (ps_VoucherType == null || ps_VoucherType == "" || ps_VoucherType == "0")
                {
                    li_AllVoucherType = 1;
                }
                else
                {
                    li_AllVoucherType = 0;
                }

                if (ps_DateFrom == null || ps_DateFrom == "" || ps_DateTo == null || ps_DateTo == "")
                {
                    li_AllDate = 1;
                }
                else
                {
                    li_AllDate = 0;
                }

                objDal.GetVoucherEntryConsoleData(li_AllLocation, ps_Location, li_AllVoucherType, ps_VoucherType, li_AllDate, ps_DateFrom, ps_DateTo, false);

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteRecordById(String ps_Id)
        {
            DALVoucherEntry objDal = new DALVoucherEntry();

            try
            {
                GL_VchrMaster VoucherMasterRow = objDal.GetAllMasterRecords().Where(c => c.VchMas_Id.Equals(ps_Id)).SingleOrDefault();
                List<GL_VchrDetail> VoucherDetailList = objDal.GetAllDetailRecords().Where(c => c.VchMas_Id.Equals(ps_Id)).ToList();

                int result = objDal.DeleteRecordById(ps_Id);
                ViewData["result"] = result;

                // Audit Trail Entry Section
                if (result > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 16;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = VoucherMasterRow.Loc_Id;
                        systemAuditTrail.VchrType_Id = VoucherMasterRow.VchrType_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = ps_Id;
                        systemAuditTrail.AdtTrl_DataDump = "VchMas_Id = " + VoucherMasterRow.VchMas_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchMas_Code = " + VoucherMasterRow.VchMas_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchMas_Date = " + VoucherMasterRow.VchMas_Date + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + VoucherMasterRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + VoucherMasterRow.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchrType_Id = " + VoucherMasterRow.VchrType_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchMas_Remarks = " + VoucherMasterRow.VchMas_Remarks + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchMas_Status = " + VoucherMasterRow.VchMas_Status + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchMas_EnteredBy = " + VoucherMasterRow.VchMas_EnteredBy + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchMas_EnteredDate = " + VoucherMasterRow.VchMas_EnteredDate + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchMas_ApprovedBy = " + VoucherMasterRow.VchMas_ApprovedBy + ";";
                        systemAuditTrail.AdtTrl_DataDump += "VchMas_ApprovedDate = " + VoucherMasterRow.VchMas_ApprovedDate + ";";
                        systemAuditTrail.AdtTrl_DataDump += "SyncStatus = " + VoucherMasterRow.SyncStatus + ";";

                        foreach (GL_VchrDetail voucherDetail in VoucherDetailList)
                        {
                            systemAuditTrail.AdtTrl_DataDump += "║ VchDet_Id = " + voucherDetail.VchDet_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "VchMas_Id = " + voucherDetail.VchMas_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "ChrtAcc_Id = " + voucherDetail.ChrtAcc_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "VchMas_DrAmount = " + voucherDetail.VchMas_DrAmount + ";";
                            systemAuditTrail.AdtTrl_DataDump += "VchMas_CrAmount = " + voucherDetail.VchMas_CrAmount + ";";
                            systemAuditTrail.AdtTrl_DataDump += "VchDet_Remarks = " + voucherDetail.VchDet_Remarks + ";";
                        }

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

        public ActionResult GetVoucherEntryConsoleData(String ps_Location, String ps_VoucherType, String pcnt_DateFrom, String pcnt_DateTo)
        {
            GL_VchrMaster DataRow = new GL_VchrMaster();
            String companyId = "0";

            try
            {
                //if (!String.IsNullOrEmpty(Code))
                //{
                //    DataRow.Cmp_Id = Code;
                //    DataRow.Cmp_Code = Code;
                //    DataRow.Cmp_Title = Name;
                //    DataRow.Cmp_Address1 = Address1;
                //    DataRow.Cmp_Address2 = Address2;
                //    DataRow.Cmp_Email = Email;
                //    DataRow.Cmp_Phone = Phone;
                //    DataRow.Cmp_Fax = Fax;
                //    DataRow.Cmp_Active = 1;
                //    companyId = objDalCompany.SaveCompany(DataRow).ToString();
                //    ViewData["companyId"] = companyId;
                //}
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

    }
}
