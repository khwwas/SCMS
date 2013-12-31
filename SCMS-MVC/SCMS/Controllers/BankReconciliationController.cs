using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class BankReconciliationController : Controller
    {
        //
        // GET: /BankReconciliation/
        public ActionResult Index()
        {
            return View("BankReconciliation");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Reconciled, String ps_ReconciliationDate)
        {
            DALBankReconciliation objDalBankReconciliation = new DALBankReconciliation();
            GL_VchrMaster lrow_BankReconciliation = new GL_VchrMaster();
            String IsAuditTrail = "";
            // ls_Action = "Edit", 
            String[] ls_Lable = new String[5], ls_Data = new String[5];
            Int32 li_ReturnValue = 0;

            try
            {
                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_BankReconciliation.VchMas_Id = ps_Code;
                    if (ps_Reconciled.ToLower() == "On".ToLower())
                    {
                        lrow_BankReconciliation.VchMas_Reconciliation = 1;
                    }
                    else
                    {
                        lrow_BankReconciliation.VchMas_Reconciliation = 0;
                    }
                    lrow_BankReconciliation.VchMas_ReconciliationDate = Convert.ToDateTime(ps_ReconciliationDate);

                    li_ReturnValue = objDalBankReconciliation.SaveRecord(lrow_BankReconciliation);
                    ViewData["SaveResult"] = li_ReturnValue;

                    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];

                    //// Audit Trail Entry Section
                    //if (li_ReturnValue > 0 && IsAuditTrail == "1")
                    //{
                    //    DALAuditLog objAuditLog = new DALAuditLog();

                    //    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    //    ls_Lable[0] = "Code";
                    //    ls_Lable[1] = "Reconciliation";
                    //    ls_Lable[2] = "Reconciliation Date";


                    //    ls_Data[0] = ps_Code;
                    //    ls_Data[1] = ps_Reconciled;
                    //    ls_Data[2] = ps_ReconciliationDate;

                    //    objAuditLog.SaveRecord(12, ls_UserId, ls_Action, ls_Lable, ls_Data);
                    //}
                }
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

    }
}
