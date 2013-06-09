using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
namespace SCMS.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            ViewData["ddl_UserGroup"] = new SelectList(new DALUserGroup().PopulateData(), "UsrGrp_Id", "UsrGrp_Title", "ddl_UserGroup");
            return View("User");
        }

        public ActionResult SaveRecord(string ps_Code, string ps_Title, string ps_GroupId, string ps_Password)
        {
            DALUser objDal = new DALUser();
            Int32 li_ReturnValue = 0;

            try
            {
                SECURITY_User lrow_Data = new SECURITY_User();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SECURITY_User") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SECURITY_User");
                    }
                }

                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_Data.User_Id = ps_Code;
                    lrow_Data.User_Code = ps_Code;
                    lrow_Data.UsrGrp_Id = ps_GroupId;
                    lrow_Data.User_Title = ps_Title;
                    lrow_Data.User_Password = ps_Password;
                    lrow_Data.User_Active = 1;

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
            DALUser objDal = new DALUser();
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
