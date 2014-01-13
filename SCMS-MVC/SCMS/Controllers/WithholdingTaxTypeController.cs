using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class WithholdingTaxTypeController : Controller
    {
        //
        // GET: /WithholdingTaxType/
        DALWithholdingTaxType objDalWithholdingTaxType = new DALWithholdingTaxType();
        public ActionResult Index()
        {
            ViewData["ddl_Company"] = new SelectList(new DALCompany().PopulateData(), "Cmp_Id", "Cmp_Title", "ddl_Company");
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            return View("WithholdingTaxType");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Title)
        {
            SETUP_WithholdingTaxType lrow_WithholdingTaxType = new SETUP_WithholdingTaxType();
            String ls_Action = "Edit", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[2], ls_Data = new String[2];
            Int32 li_ReturnValue = 0;

            try
            {
                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_WithholdingTaxType") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_WithholdingTaxType");
                        ls_Action = "Add";
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_WithholdingTaxType.CldrType_Id = ps_Code;
                    lrow_WithholdingTaxType.CldrType_Code = ps_Code;
                    lrow_WithholdingTaxType.CldrType_Title = Title;
                    lrow_WithholdingTaxType.CldrType_Active = 1;

                    li_ReturnValue = objDalWithholdingTaxType.SaveRecord(lrow_WithholdingTaxType);
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
                        ls_Data[1] = Title;
                        
                        objAuditLog.SaveRecord(13, ls_UserId, ls_Action, ls_Lable, ls_Data);
                    }
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
                SETUP_WithholdingTaxType WithholdingTaxTypeRow = objDalWithholdingTaxType.GetAllRecords().Where(c => c.CldrType_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalWithholdingTaxType.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Title";

                    ls_Data[0] = WithholdingTaxTypeRow.CldrType_Code;
                    ls_Data[1] = WithholdingTaxTypeRow.CldrType_Title;
                   
                    objAuditLog.SaveRecord(13, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
