using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class UserMenuController : Controller
    {
        //
        // GET: /UserMenu/
        DalUserMenu objDalUserMenu = new DalUserMenu();
        public ActionResult Index()
        {
            return View("UserMenu");
        }

        // Insetion User Menu
        public ActionResult SaveUserMenu(String UserMenu, int UserGroup)
        {
            try
            {
                Security_UserRight setupUserMenuRow = new Security_UserRight();
                //if (String.IsNullOrEmpty(Code))
                //{
                //    Code = DALCommon.GetMaximumCode("SECURITY_UserGroup", "UsrGrp_Id");
                //}
                //String UserGroupId = "0";
                //if (!String.IsNullOrEmpty(Code))
                //{
                setupUserMenuRow.UsrSec_Code = UserGroup.ToString();
                setupUserMenuRow.Grp_Id = UserGroup;
                setupUserMenuRow.Mnu_Id = Convert.ToInt32(UserMenu);
                setupUserMenuRow.UsrSec_Active = 1;
                UserMenuId = objDalUserMenu.SaveUserMenu(setupUserMenuRow).ToString();
                ViewData["UserGroupId"] = UserMenuId;
                //}
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }


        public object UserMenuId { get; set; }
    }
}
