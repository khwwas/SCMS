using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/
        DALCompany objDalCompany = new DALCompany();
        public ActionResult Index()
        {
            return View("Company");
        }

        public ActionResult SaveCompany(String Code, String Name, String Address1, String Address2, String Email, String Phone, String Fax)
        {
            SETUP_Company setupCompanyRow = new SETUP_Company();
            String ls_Action = "Edit", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[7], ls_Data = new String[7];
            Int32 li_ReturnValue = 0;

            try
            {
                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Company") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_Company");
                        ls_Action = "Add";
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    setupCompanyRow.Cmp_Id = Code;
                    setupCompanyRow.Cmp_Code = Code;
                    setupCompanyRow.Cmp_Title = Name;
                    setupCompanyRow.Cmp_Address1 = Address1;
                    setupCompanyRow.Cmp_Address2 = Address2;
                    setupCompanyRow.Cmp_Email = Email;
                    setupCompanyRow.Cmp_Phone = Phone;
                    setupCompanyRow.Cmp_Fax = Fax;
                    setupCompanyRow.Cmp_Active = 1;
                    li_ReturnValue = objDalCompany.SaveCompany(setupCompanyRow);
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
                        ls_Lable[3] = "       ";
                        ls_Lable[4] = "Email";
                        ls_Lable[5] = "Phone";
                        ls_Lable[6] = "Fax";

                        ls_Data[0] = Code;
                        ls_Data[1] = Name;
                        ls_Data[2] = Address1;
                        ls_Data[3] = Address2;
                        ls_Data[4] = Email;
                        ls_Data[5] = Phone;
                        ls_Data[6] = Fax;

                        objAuditLog.SaveRecord(1, ls_UserId, ls_Action, ls_Lable, ls_Data);
                    }
                }
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteCompany(String companyId)
        {
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[7], ls_Data = new String[7];
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Company CompanyRow = objDalCompany.GetCmpByCode(companyId).SingleOrDefault();

                li_ReturnValue = objDalCompany.DeleteCompanyByCompanyId(companyId);
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
                    ls_Lable[3] = "       ";
                    ls_Lable[4] = "Email";
                    ls_Lable[5] = "Phone";
                    ls_Lable[6] = "Fax";

                    ls_Data[0] = CompanyRow.Cmp_Code;
                    ls_Data[1] = CompanyRow.Cmp_Title;
                    ls_Data[2] = CompanyRow.Cmp_Address1;
                    ls_Data[3] = CompanyRow.Cmp_Address2;
                    ls_Data[4] = CompanyRow.Cmp_Email;
                    ls_Data[5] = CompanyRow.Cmp_Phone;
                    ls_Data[6] = CompanyRow.Cmp_Fax;

                    objAuditLog.SaveRecord(1, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
