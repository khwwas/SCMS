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
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Customer row_Customer = new SETUP_Customer();

                String Action = "Add";
                if (!string.IsNullOrEmpty(Code))
                {
                    Action = "Edit";
                }

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Customer") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_Customer");
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

                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0)
                    {
                        string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                        if (IsAuditTrail == "1")
                        {
                            SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                            DALAuditTrail objAuditTrail = new DALAuditTrail();
                            systemAuditTrail.Scr_Id = 7;
                            systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                            systemAuditTrail.Loc_Id = row_Customer.Loc_Id;
                            systemAuditTrail.AdtTrl_Action = Action;
                            systemAuditTrail.AdtTrl_EntryId = Code;
                            systemAuditTrail.AdtTrl_DataDump = "Cust_Id = " + row_Customer.Cust_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cust_Code = " + row_Customer.Cust_Code + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + row_Customer.Cmp_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + row_Customer.Loc_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "CustType_Id = " + row_Customer.CustType_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "City_Id = " + row_Customer.City_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cust_Title = " + row_Customer.Cust_Title + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cust_Address1 = " + row_Customer.Cust_Address1 + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cust_Address2 = " + row_Customer.Cust_Address2 + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cust_Email = " + row_Customer.Cust_Email + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cust_Phone = " + row_Customer.Cust_Phone + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cust_Fax = " + row_Customer.Cust_Fax + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cust_Active = " + row_Customer.Cust_Active + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cust_SortOrder = " + row_Customer.Cust_SortOrder + ";";
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

        public ActionResult DeleteRecord(String CusID)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Customer SupplierRow = objDALCustomer.GetAllCustomer().Where(c => c.Cust_Id.Equals(CusID)).SingleOrDefault();

                li_ReturnValue = objDALCustomer.DeleteRecordById(CusID);
                ViewData["SaveResult"] = li_ReturnValue;

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 7;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = SupplierRow.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = CusID;
                        systemAuditTrail.AdtTrl_DataDump = "Cust_Id = " + SupplierRow.Cust_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cust_Code = " + SupplierRow.Cust_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + SupplierRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + SupplierRow.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "CustType_Id = " + SupplierRow.CustType_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "City_Id = " + SupplierRow.City_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cust_Title = " + SupplierRow.Cust_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cust_Address1 = " + SupplierRow.Cust_Address1 + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cust_Address2 = " + SupplierRow.Cust_Address2 + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cust_Email = " + SupplierRow.Cust_Email + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cust_Phone = " + SupplierRow.Cust_Phone + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cust_Fax = " + SupplierRow.Cust_Fax + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cust_Active = " + SupplierRow.Cust_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cust_SortOrder = " + SupplierRow.Cust_SortOrder + ";";
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
