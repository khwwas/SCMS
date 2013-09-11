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
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Company setupCompanyRow = new SETUP_Company();
                String Action = "Add";
                if (!string.IsNullOrEmpty(Code))
                {
                    Action = "Edit";
                }

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
                    li_ReturnValue = objDalCompany.SaveCompany(setupCompanyRow);
                    ViewData["SaveResult"] = li_ReturnValue;

                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0)
                    {
                        string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];
                        if (IsAuditTrail == "1")
                        {
                            SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                            DALAuditTrail objAuditTrail = new DALAuditTrail();

                            systemAuditTrail.Scr_Id = 1;
                            systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                            systemAuditTrail.AdtTrl_Action = Action;
                            systemAuditTrail.AdtTrl_EntryId = Code;
                            ////systemAuditTrail.AdtTrl_DataDump =  ":" + Code + ";";
                            //systemAuditTrail.AdtTrl_DataDump += "<html><b>Code   :</b> " + Code + " &nbsp; <br/>";
                            //systemAuditTrail.AdtTrl_DataDump += "<b>Title  :</b> " + Name + " &nbsp; <br/>";
                            //systemAuditTrail.AdtTrl_DataDump += "<b>Address:</b> " + Address1 + " &nbsp; <br/>";
                            //systemAuditTrail.AdtTrl_DataDump += "<b>Address:</b> " + Address2 + " &nbsp; <br/>";
                            //systemAuditTrail.AdtTrl_DataDump += "<b>Email  :</b> " + Email + " &nbsp; <br/>";
                            //systemAuditTrail.AdtTrl_DataDump += "<b>Phone  :</b> " + Phone + " &nbsp; <br/>";
                            //systemAuditTrail.AdtTrl_DataDump += "<b>Fax    :</b> " + Fax + " </html>";
                            ////systemAuditTrail.AdtTrl_DataDump += "Cmp_Active = " + 1 + ";";
 
                            systemAuditTrail.AdtTrl_DataDump += "<html><div style=clear:both><b>Code: </b> " + Code + " &nbsp;</div> ";
                            systemAuditTrail.AdtTrl_DataDump += "<div style=clear:both><b>Titl: </b> " + Name + " &nbsp;</div>";
                            systemAuditTrail.AdtTrl_DataDump += "<div style=clear:both><b>Address: </b> " + Address1 + " &nbsp;</div>";
                            systemAuditTrail.AdtTrl_DataDump += "<div style=clear:both><b>Address: </b> " + Address2 + " &nbsp;</div>";
                            systemAuditTrail.AdtTrl_DataDump += "<div style=clear:both><b>Email: </b> " + Email + " &nbsp;</div>";
                            systemAuditTrail.AdtTrl_DataDump += "<div style=clear:both><b>Phone: </b> " + Phone + " &nbsp;</div>";
                            systemAuditTrail.AdtTrl_DataDump += "<div style=clear:both><b>Fax: </b> " + Fax + " </html>";
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

        public ActionResult DeleteCompany(String companyId)
        {
            Int32 li_ReturnValue = 0;
            try
            {
                SETUP_Company CompanyRow = objDalCompany.GetCmpByCode(companyId).SingleOrDefault();

                li_ReturnValue = objDalCompany.DeleteCompanyByCompanyId(companyId);
                ViewData["SaveResult"] = li_ReturnValue;

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 1;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = companyId;
                        systemAuditTrail.AdtTrl_DataDump = "Cmp_Id = " + CompanyRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Code = " + CompanyRow.Cmp_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Title = " + CompanyRow.Cmp_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Address1 = " + CompanyRow.Cmp_Address1 + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Address2 = " + CompanyRow.Cmp_Address2 + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Email = " + CompanyRow.Cmp_Email + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Phone = " + CompanyRow.Cmp_Phone + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Fax = " + CompanyRow.Cmp_Fax + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Active = " + CompanyRow.Cmp_Active + ";";
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
