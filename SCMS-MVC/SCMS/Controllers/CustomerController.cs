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
                    row_Customer.Cust_Email = Email;
                    row_Customer.Cust_Phone = Phone;
                    row_Customer.Cust_Fax = Fax;
                    row_Customer.Cust_Active = 1;
                    li_ReturnValue = objDALCustomer.SaveRecord(row_Customer);
                    ViewData["SaveResult"] = li_ReturnValue;
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
                li_ReturnValue = objDALCustomer.DeleteRecordById(CusID);
                ViewData["DeleteResult"] = li_ReturnValue;

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

    }
}
