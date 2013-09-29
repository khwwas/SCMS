using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        DALCustomer objDALCustomer = new DALCustomer();
        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            ViewData["ddl_CustomerType"] = new SelectList(new DALCustomerType().GetAllRecords(), "CustType_Id", "CustType_Title", "ddl_CustomerType");
            return View("Customer");
        }

        public ActionResult SaveCustomerRecord(String Code, String location, String CustomerType, String Title, String Address, String Email, String Phone, String Fax)
        {
            SETUP_Customer row_Customer = new SETUP_Customer();
            String ls_Action = "Edit", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[7], ls_Data = new String[7];
            Int32 li_ReturnValue = 0;

            try
            {
                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Customer") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_Customer");
                        ls_Action = "Add";
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    row_Customer.Cust_Id = Code;
                    row_Customer.Cust_Code = Code;
                    row_Customer.Loc_Id = location;
                    row_Customer.CustType_Id = CustomerType;
                    row_Customer.Cust_Title = Title;
                    row_Customer.Cust_Address1 = Address;
                    row_Customer.City_Id = "00001";
                    row_Customer.Cust_Email = Email;
                    row_Customer.Cust_Phone = Phone;
                    row_Customer.Cust_Fax = Fax;
                    row_Customer.Cust_Active = 1;
                    li_ReturnValue = objDALCustomer.SaveRecord(row_Customer);
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
                        ls_Lable[3] = "Customer Type";
                        ls_Lable[4] = "Email";
                        ls_Lable[5] = "Phone";
                        ls_Lable[6] = "Fax";

                        ls_Data[0] = Code;
                        ls_Data[1] = Title;
                        ls_Data[2] = Address;
                        ls_Data[3] = CustomerType;
                        ls_Data[4] = Email;
                        ls_Data[5] = Phone;
                        ls_Data[6] = Fax;

                        objAuditLog.SaveRecord(7, ls_UserId, ls_Action, ls_Lable, ls_Data);
                    }
                }
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteRecord(String CusID)
        {
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[7], ls_Data = new String[7];
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Customer SupplierRow = objDALCustomer.GetAllCustomer().Where(c => c.Cust_Id.Equals(CusID)).SingleOrDefault();

                li_ReturnValue = objDALCustomer.DeleteRecordById(CusID);
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

                    ls_Data[0] = SupplierRow.Cust_Code;
                    ls_Data[1] = SupplierRow.Cust_Title;
                    ls_Data[2] = SupplierRow.Cust_Address1;
                    ls_Data[3] = SupplierRow.CustType_Id;
                    ls_Data[4] = SupplierRow.Cust_Email;
                    ls_Data[5] = SupplierRow.Cust_Phone;
                    ls_Data[6] = SupplierRow.Cust_Fax;

                    objAuditLog.SaveRecord(7, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
