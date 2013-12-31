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
            return View("BudgetType");
        }

        public ActionResult Save(String Code, String Title, String Prefix)
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
                    SystemBudgetTypeRow.BgdtType_Id = Code;
                    SystemBudgetTypeRow.BgdtType_Code = Code;
                    SystemBudgetTypeRow.BgdtType_Title = Title;
                    SystemBudgetTypeRow.BgdtType_Prefix = Prefix;
                    SystemBudgetTypeRow.BgdtType_SortOrder = 1;
                    SystemBudgetTypeRow.BgdtType_Active = 1;
                    li_ReturnValue = objDALBudgetType.Save(SystemBudgetTypeRow);

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
  
                        objAuditLog.SaveRecord(18, ls_UserId, ls_Action, ls_Lable, ls_Data);
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

        public ActionResult Delete(String Id)
        {
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[3], ls_Data = new String[3];
            Int32 li_ReturnValue = 0;

            try
            {
                sp_PopulateBudgetTypeListResult BudgetTypeRow = objDALBudgetType.PopulateData().Where(c => c.BgdtType_Id.Equals(Id)).SingleOrDefault();

                li_ReturnValue = objDALBudgetType.DeleteById(Id);
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
                   
                    ls_Data[0] = BudgetTypeRow.BgdtType_Code;
                    ls_Data[1] = BudgetTypeRow.BgdtType_Title;
                    ls_Data[2] = BudgetTypeRow.BgdtType_Prefix;
                    
                    objAuditLog.SaveRecord(18, ls_UserId, ls_Action, ls_Lable, ls_Data);
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
