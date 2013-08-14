using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
namespace SCMS.Controllers
{
    public class CustomerTypeController : Controller
    {
        //
        // GET: /Supplier Type/
        DALCustomerType objDalCustomerType = new DALCustomerType();

        public ActionResult Index()
        {
            return View("CustomerType");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_CustomerType lrow_CustomerType = new SETUP_CustomerType();
                String Action = "Add";
                if (!string.IsNullOrEmpty(ps_Code))
                {
                    Action = "Edit";
                }

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_CustomerType") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_CustomerType");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_CustomerType.CustType_Id = ps_Code;
                    lrow_CustomerType.CustType_Code = ps_Code;
                    lrow_CustomerType.CustType_Title = ps_Title;
                    lrow_CustomerType.CustType_Active = 1;

                    li_ReturnValue = objDalCustomerType.SaveRecord(lrow_CustomerType);
                    ViewData["SaveResult"] = li_ReturnValue;

                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0)
                    {
                        string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                        if (IsAuditTrail == "1")
                        {
                            SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                            DALAuditTrail objAuditTrail = new DALAuditTrail();
                            systemAuditTrail.Scr_Id = 6;
                            systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                            systemAuditTrail.Loc_Id = lrow_CustomerType.Loc_Id;
                            systemAuditTrail.AdtTrl_Action = Action;
                            systemAuditTrail.AdtTrl_EntryId = ps_Code;
                            systemAuditTrail.AdtTrl_DataDump = "CustType_Id = " + lrow_CustomerType.CustType_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "CustType_Code = " + lrow_CustomerType.CustType_Code + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + lrow_CustomerType.Cmp_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + lrow_CustomerType.Loc_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "CustType_Title = " + lrow_CustomerType.CustType_Title + ";";
                            systemAuditTrail.AdtTrl_DataDump += "CustType_Active = " + lrow_CustomerType.CustType_Active + ";";
                            systemAuditTrail.AdtTrl_DataDump += "CustType_SortOrder = " + lrow_CustomerType.CustType_SortOrder + ";";
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

        public ActionResult DeleteRecord(String _pId)
        {

            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_CustomerType CustomerTypeRow = objDalCustomerType.GetAllCustomerType().Where(c => c.CustType_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalCustomerType.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 6;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = CustomerTypeRow.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = _pId;
                        systemAuditTrail.AdtTrl_DataDump = "CustType_Id = " + CustomerTypeRow.CustType_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "CustType_Code = " + CustomerTypeRow.CustType_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + CustomerTypeRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + CustomerTypeRow.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "CustType_Title = " + CustomerTypeRow.CustType_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "CustType_Active = " + CustomerTypeRow.CustType_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "CustType_SortOrder = " + CustomerTypeRow.CustType_SortOrder + ";";
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
