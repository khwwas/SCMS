using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class BudgetTypeController : Controller
    {

        DALBudgetType objDALBudgetType = new DALBudgetType();

        public ActionResult Index()
        {
            //ViewData["ddl_Locations"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Locations");
            return View("BudgetType");
        }

        public ActionResult SaveBudgetType(String Code, String Title, String Prefix)
        {
            SYSTEM_BudgetType SystemBudgetTypeRow = new SYSTEM_BudgetType();
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[3], ls_Data = new String[3];
            Int32 li_ReturnValue = 0;

            try
            {
                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SYSTEM_BudgetType") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SYSTEM_BudgetType");
                        ls_Action = "Add";
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    SystemBudgetTypeRow.VchrType_Id = Code;
                    SystemBudgetTypeRow.VchrType_Code = Code;
                    SystemBudgetTypeRow.VchrType_Title = Title;
                    SystemBudgetTypeRow.VchrType_Prefix = Prefix;
                    SystemBudgetTypeRow.VchrType_SortOrder = 1;
                    SystemBudgetTypeRow.VchrType_Active = 1;
                    li_ReturnValue = objDALBudgetType.SaveBudgetType(SystemBudgetTypeRow);

                    ViewData["SaveResult"] = li_ReturnValue;

                    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0 && IsAuditTrail == "1")
                    {
                        DALAuditLog objAuditLog = new DALAuditLog();

                        ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                        ls_Lable[0] = "Code";
                        ls_Lable[1] = "Title";
                        ls_Lable[2] = "Prefix";

                        ls_Data[0] = Code;
                        ls_Data[1] = Title;
                        ls_Data[2] = Prefix;
  
                        objAuditLog.SaveRecord(8, ls_UserId, ls_Action, ls_Lable, ls_Data);
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

        public ActionResult DeleteBudgetType(String BudgetTypeId)
        {
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[4], ls_Data = new String[4];
            Int32 li_ReturnValue = 0;

            try
            {
                sp_PopulateBudgetTypeListResult BudgetTypeRow = objDALBudgetType.PopulateData().Where(c => c.VchrType_Id.Equals(BudgetTypeId)).SingleOrDefault();

                li_ReturnValue = objDALBudgetType.DeleteBudgetTypeById(BudgetTypeId);
                ViewData["SaveResult"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Title";
                    ls_Lable[2] = "Prefix";
                    ls_Lable[3] = "Code Initialization";

                    ls_Data[0] = BudgetTypeRow.VchrType_Code;
                    ls_Data[1] = BudgetTypeRow.VchrType_Title;
                    ls_Data[2] = BudgetTypeRow.VchrType_Prefix;
                    ls_Data[3] = BudgetTypeRow.VchrType_CodeInitialization.ToString();

                    objAuditLog.SaveRecord(8, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
