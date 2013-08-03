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
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Supplier row_Supplier = new SETUP_Supplier();

                String Action = "Add";
                if (!string.IsNullOrEmpty(Code))
                {
                    Action = "Edit";
                }


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
                }

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 5;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = row_Supplier.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = Action;
                        systemAuditTrail.AdtTrl_EntryId = Code;
                        systemAuditTrail.AdtTrl_DataDump = "Supp_Id = " + row_Supplier.Supp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Code = " + row_Supplier.Supp_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + row_Supplier.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + row_Supplier.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "SuppType_Id = " + row_Supplier.SuppType_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "City_Id = " + row_Supplier.City_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Title = " + row_Supplier.Supp_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Address1 = " + row_Supplier.Supp_Address1 + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Address2 = " + row_Supplier.Supp_Address2 + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Email = " + row_Supplier.Supp_Email + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Phone = " + row_Supplier.Supp_Phone + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Fax = " + row_Supplier.Supp_Fax + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Active = " + row_Supplier.Supp_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_SortOrder = " + row_Supplier.Supp_SortOrder + ";";
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

        public ActionResult DeleteRecord(String SuppID)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Supplier SupplierRow = objDalSupplier.GetAllSupplier().Where(c => c.Supp_Id.Equals(SuppID)).SingleOrDefault();

                li_ReturnValue = objDalSupplier.DeleteRecordById(SuppID);
                ViewData["SaveResult"] = li_ReturnValue;

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 5;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = SupplierRow.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = SuppID;
                        systemAuditTrail.AdtTrl_DataDump = "Supp_Id = " + SupplierRow.Supp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Code = " + SupplierRow.Supp_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + SupplierRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + SupplierRow.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "SuppType_Id = " + SupplierRow.SuppType_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "City_Id = " + SupplierRow.City_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Title = " + SupplierRow.Supp_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Address1 = " + SupplierRow.Supp_Address1 + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Address2 = " + SupplierRow.Supp_Address2 + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Email = " + SupplierRow.Supp_Email + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Phone = " + SupplierRow.Supp_Phone + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Fax = " + SupplierRow.Supp_Fax + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_Active = " + SupplierRow.Supp_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Supp_SortOrder = " + SupplierRow.Supp_SortOrder + ";";
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
