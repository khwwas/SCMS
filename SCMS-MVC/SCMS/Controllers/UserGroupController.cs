using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
namespace SCMS.Controllers
{
    public class UserGroupController : Controller
    {
        //
        // GET: /User Group/
        DALUserGroup objDal = new DALUserGroup();

        public ActionResult Index()
        {
            return View("UserGroup");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SECURITY_UserGroup lrow_Data = new SECURITY_UserGroup();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SECURITY_UserGroup") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SECURITY_UserGroup");
                    }
                }

                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_Data.UsrGrp_Id = ps_Code;
                    lrow_Data.UsrGrp_Code = ps_Code;
                    lrow_Data.UsrGrp_Title = ps_Title;
                    lrow_Data.UsrGrp_Active = 1;

                    li_ReturnValue = objDal.SaveRecord(lrow_Data);
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
                li_ReturnValue = objDal.DeleteRecordById(_pId);
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
