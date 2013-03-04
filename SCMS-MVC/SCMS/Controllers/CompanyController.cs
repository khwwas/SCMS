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
            String companyId = "0";

            try
            {
                SETUP_Company setupCompanyRow = new SETUP_Company();

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Company") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_Company");
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
                    companyId = objDalCompany.SaveCompany(setupCompanyRow).ToString();
                    ViewData["companyId"] = companyId;
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
            try
            {
                int result = objDalCompany.DeleteCompanyByCompanyId(companyId);
                ViewData["result"] = result;
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

    }
}
