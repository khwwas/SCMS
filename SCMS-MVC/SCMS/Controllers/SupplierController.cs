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
            ViewData["ddl_Company"] = new SelectList(new DALCompany().PopulateData(), "Cmp_Id", "Cmp_Title", "ddl_Company");
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            ViewData["ddl_SupplierType"] = new SelectList(new DALSupplierType().GetAllRecords(), "SuppType_Id", "SuppType_Title", "ddl_SupplierType");
            ViewData["ddl_City"] = new SelectList(new DALCity().GetAllRecords(), "City_Id", "City_Title", "ddl_City");
            return View("Supplier");
        }

        public ActionResult SaveSupplierRecord(String Code, String Company, String location, String SupplierType, String City, String Title, String Address1, String Address2, String Email, String Phone, String Fax)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Supplier row_Supplier = new SETUP_Supplier();

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Supplier") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_Supplier");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    row_Supplier.Supp_Id = Code;
                    row_Supplier.Supp_Code = Code;
                    row_Supplier.Cmp_Id = Company;
                    row_Supplier.Loc_Id = location;
                    row_Supplier.SuppType_Id = SupplierType;
                    row_Supplier.City_Id = City;
                    row_Supplier.Supp_Title = Title;
                    row_Supplier.Supp_Address1 = Address1;
                    row_Supplier.Supp_Address2 = Address2;
                    row_Supplier.Supp_Email = Email;
                    row_Supplier.Supp_Phone = Phone;
                    row_Supplier.Supp_Fax = Fax;
                    row_Supplier.Supp_Active = 1;
                    li_ReturnValue = objDalSupplier.SaveRecord(row_Supplier);
                    ViewData["SaveResult"] = li_ReturnValue;
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
            Int32 li_ReturnValue = 0;

            try
            {
                li_ReturnValue = objDalSupplier.DeleteRecordById(SuppID);
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
