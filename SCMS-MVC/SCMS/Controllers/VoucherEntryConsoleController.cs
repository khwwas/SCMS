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
        public ActionResult Index(string locationId)
        {
            ViewData["ddl_Location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", locationId);
            //ddl_Location.
            //ViewData["ddl_VoucherType"] = new SelectList(new DALVoucherType().PopulateData(), "VchrType_Id", "VchrType_Title", "ddl_VoucherType");
            ViewData["LocationId"] = locationId;
            return View("VoucherEntryConsole");
        }

        //public ActionResult Search(string ps_AllLoc, string ps_Location)
        //{
        //    DALVoucherEntry objDal = new DALVoucherEntry();
        //    int li_AllLocation = -1;//, li_AllVoucherType = -1, li_AllDate = -1;

        //    try
        //    {
        //        if (ps_Location == null || ps_Location == "" || ps_Location == "0")
        //        {
        //            li_AllLocation = 1;
        //        }
        //        else
        //        {
        //            li_AllLocation = 0;
        //        }

        //        //if (ps_VoucherType == null || ps_VoucherType == "" || ps_VoucherType == "0")
        //        //{
        //        //    li_AllVoucherType = 1;
        //        //}
        //        //else
        //        //{
        //        //    li_AllVoucherType = 0;
        //        //}

        //        //if (ps_DateFrom == null || ps_DateFrom == "" || ps_DateTo == null || ps_DateTo == "")
        //        //{
        //        //    li_AllDate = 1;
        //        //}
        //        //else
        //        //{
        //        //    li_AllDate = 0;
        //        //}

        //        //objDal.GetVoucherEntryConsoleData(li_AllLocation, ps_Location, li_AllVoucherType, ps_VoucherType, li_AllDate, ps_DateFrom, ps_DateTo, false);
        //        objDal.GetVoucherEntryConsoleData(li_AllLocation, ps_Location, 1, "", 1, "", "", true);

        //        return PartialView("GridData");
        //    }
        //    catch
        //    {
        //        return PartialView("GridData");
        //    }
        //}

        public ActionResult DeleteRecordById(String ps_Id)
        {
            DALVoucherEntry objDal = new DALVoucherEntry();
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[7], ls_Data = new String[7];
            Int32 li_ReturnValue = 0;

            try
            {
                GL_VchrMaster VoucherMasterRow = objDal.GetAllMasterRecords().Where(c => c.VchMas_Id.Equals(ps_Id)).SingleOrDefault();
                List<GL_VchrDetail> VoucherDetailList = objDal.GetAllDetailRecords().Where(c => c.VchMas_Id.Equals(ps_Id)).ToList();

                li_ReturnValue = objDal.DeleteRecordById(ps_Id);
                ViewData["result"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Date";
                    ls_Lable[2] = "Location";
                    ls_Lable[3] = "Voucher Type";
                    ls_Lable[4] = "Remarks";
                    ls_Lable[5] = "Status";
                    ls_Lable[6] = "Entered By";

                    ls_Data[0] = VoucherMasterRow.VchMas_Code;
                    ls_Data[1] = Convert.ToString(VoucherMasterRow.VchMas_Date);
                    ls_Data[2] = VoucherMasterRow.Loc_Id;
                    ls_Data[3] = VoucherMasterRow.VchrType_Id;
                    ls_Data[4] = VoucherMasterRow.VchMas_Remarks;
                    ls_Data[5] = VoucherMasterRow.VchMas_Status;
                    ls_Data[6] = VoucherMasterRow.VchMas_EnteredBy;

                    objAuditLog.SaveRecord(16, ls_UserId, ls_Action, ls_Lable, ls_Data);
                }
                // Audit Trail Section End

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        //public ActionResult GetVoucherEntryConsoleData(String ps_Location, String ps_VoucherType, String pcnt_DateFrom, String pcnt_DateTo)
        //{
        //    GL_VchrMaster DataRow = new GL_VchrMaster();
        //    String companyId = "0";

        //    try
        //    {
        //        //if (!String.IsNullOrEmpty(Code))
        //        //{
        //        //    DataRow.Cmp_Id = Code;
        //        //    DataRow.Cmp_Code = Code;
        //        //    DataRow.Cmp_Title = Name;
        //        //    DataRow.Cmp_Address1 = Address1;
        //        //    DataRow.Cmp_Address2 = Address2;
        //        //    DataRow.Cmp_Email = Email;
        //        //    DataRow.Cmp_Phone = Phone;
        //        //    DataRow.Cmp_Fax = Fax;
        //        //    DataRow.Cmp_Active = 1;
        //        //    companyId = objDalCompany.SaveCompany(DataRow).ToString();
        //        //    ViewData["companyId"] = companyId;
        //        //}
        //        return PartialView("GridData");
        //    }
        //    catch
        //    {
        //        return PartialView("GridData");
        //    }
        //}

    }
}
