using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class SupplierController : Controller
    {
        //
        // GET: /Supplier/
        DALSupplier objDalSupplier = new DALSupplier();
        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            ViewData["ddl_SupplierType"] = new SelectList(new DALSupplierType().GetAllRecords(), "SuppType_Id", "SuppType_Title", "ddl_SupplierType");
            return View("Supplier");
        }

        public ActionResult SaveSupplierRecord(String Code, String location, String SupplierType, String Title, String Address, String Email, String Phone, String Fax)
        {
            SETUP_Supplier row_Supplier = new SETUP_Supplier();
            String ls_Action = "Edit", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[7], ls_Data = new String[7];
            Int32 li_ReturnValue = 0;

            try
            {
                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Supplier") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_Supplier");
                        ls_Action = "Add";
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    row_Supplier.Supp_Id = Code;
                    row_Supplier.Supp_Code = Code;
                    row_Supplier.Loc_Id = location;
                    row_Supplier.SuppType_Id = SupplierType;
                    row_Supplier.Supp_Title = Title;
                    row_Supplier.Supp_Address1 = Address;
                    row_Supplier.Supp_Email = Email;
                    row_Supplier.Supp_Phone = Phone;
                    row_Supplier.Supp_Fax = Fax;
                    row_Supplier.Supp_Active = 1;
                    row_Supplier.City_Id = "00001";
                    li_ReturnValue = objDalSupplier.SaveRecord(row_Supplier);
                    ViewData["SaveResult"] = li_ReturnValue;

                    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                    // Save Audit Log
                    if (li_ReturnValue > 0 && IsAuditTrail == "1")
                    {
                        DALAuditLog objAuditLog = new DALAuditLog();

                        ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                        ls_Lable[0] = "Code";
                        ls_Lable[1] = "Title";
                        ls_Lable[2] = "Address";
                        ls_Lable[3] = "Supplier Type";
                        ls_Lable[4] = "Email";
                        ls_Lable[5] = "Phone";
                        ls_Lable[6] = "Fax";

                        ls_Data[0] = Code;
                        ls_Data[1] = Title;
                        ls_Data[2] = Address;
                        ls_Data[3] = SupplierType;
                        ls_Data[4] = Email;
                        ls_Data[5] = Phone;
                        ls_Data[6] = Fax;

                        objAuditLog.SaveRecord(5, ls_UserId, ls_Action, ls_Lable, ls_Data);
                    }
                }
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteRecord(String SuppID)
        {
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[7], ls_Data = new String[7];
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Supplier SupplierRow = objDalSupplier.GetAllSupplier().Where(c => c.Supp_Id.Equals(SuppID)).SingleOrDefault();

                li_ReturnValue = objDalSupplier.DeleteRecordById(SuppID);
                ViewData["SaveResult"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Title";
                    ls_Lable[2] = "Address";
                    ls_Lable[3] = "Customer Type";
                    ls_Lable[4] = "Email";
                    ls_Lable[5] = "Phone";
                    ls_Lable[6] = "Fax";

                    ls_Data[0] = SupplierRow.Supp_Code;
                    ls_Data[1] = SupplierRow.Supp_Title;
                    ls_Data[2] = SupplierRow.Supp_Address1;
                    ls_Data[3] = SupplierRow.SuppType_Id;
                    ls_Data[4] = SupplierRow.Supp_Email;
                    ls_Data[5] = SupplierRow.Supp_Phone;
                    ls_Data[6] = SupplierRow.Supp_Fax;

                    objAuditLog.SaveRecord(5, ls_UserId, ls_Action, ls_Lable, ls_Data);
                }
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

    }
}
