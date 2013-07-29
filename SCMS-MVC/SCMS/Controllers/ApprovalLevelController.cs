using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class ApprovalLevelController : Controller 
    {
        //
        // GET: /ApprovalLevels/
        DALApprovalLevel objDalApprovalLevels = new DALApprovalLevel();

        public ActionResult Index()
        {
            return View("ApprovalLevel");
        }

        public ActionResult SaveRecord(String ps_Code, Int32 pi_Level, String ps_Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SYSTEM_ApprovalLevel lrow_ApprovalLevels = new SYSTEM_ApprovalLevel();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SYSTEM_ApprovalLevel") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SYSTEM_ApprovalLevel");
                    }
                }

                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_ApprovalLevels.AprvLvl_Id = ps_Code;
                    lrow_ApprovalLevels.AprvLvl_Code = ps_Code;
                    lrow_ApprovalLevels.AprvLvl_Level = pi_Level;
                    lrow_ApprovalLevels.AprvLvl_Title = ps_Title;
                    lrow_ApprovalLevels.AprvLvl_Active = 1;

                    li_ReturnValue = objDalApprovalLevels.SaveRecord(lrow_ApprovalLevels);
                    ViewData["SaveResult"] = li_ReturnValue;
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
                li_ReturnValue = objDalApprovalLevels.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

    }
}
