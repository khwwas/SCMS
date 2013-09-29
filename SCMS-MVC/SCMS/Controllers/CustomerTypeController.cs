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
            SETUP_CustomerType lrow_CustomerType = new SETUP_CustomerType();
            String ls_Action = "Edit", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[2], ls_Data = new String[2];
            Int32 li_ReturnValue = 0;

            try
            {
                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_CustomerType") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_CustomerType");
                        ls_Action = "Add";
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

                    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                    // Save Audit Log
                    if (li_ReturnValue > 0 && IsAuditTrail == "1")
                    {
                        DALAuditLog objAuditLog = new DALAuditLog();

                        ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                        ls_Lable[0] = "Code";
                        ls_Lable[1] = "Title";

                        ls_Data[0] = ps_Code;
                        ls_Data[1] = ps_Title;

                        objAuditLog.SaveRecord(6, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[2], ls_Data = new String[2];
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_CustomerType CustomerTypeRow = objDalCustomerType.GetAllCustomerType().Where(c => c.CustType_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalCustomerType.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Title";

                    ls_Data[0] = CustomerTypeRow.CustType_Code;
                    ls_Data[1] = CustomerTypeRow.CustType_Title;

                    objAuditLog.SaveRecord(6, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
