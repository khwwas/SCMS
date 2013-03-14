using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class UserRightsSetupController : Controller
    {
        //
        // GET: /UserRightsSetup/

        public ActionResult Index(string SelectedGroup)
        {
            List<sp_GetUserGroupListResult> userGroups = new DALUserGroup().GetAllData().ToList();
            if (String.IsNullOrEmpty(SelectedGroup))
            {
                SelectedGroup = userGroups[0].UsrGrp_Id;
            }
            ViewData["ddl_UserGroups"] = new SelectList(userGroups, "UsrGrp_Id", "UsrGrp_Title", SelectedGroup);
            List<sp_GetUserMenuRightsResult> MenuRights = new DALUserMenuRights().GetUserMenuRights(SelectedGroup).OrderBy(c => c.Mnu_Level).ToList();
            ViewData["UserMenuRights"] = MenuRights;
            return View("UserRights");
        }

        //public string GetUserMenus(string GroupId)
        //{
        //    List<sp_GetUserMenuRightsResult> MenuRights = new DALUserMenuRights().GetUserMenuRights(GroupId).OrderBy(c => c.Mnu_Level).ToList();
        //    string UserMenuList = "";

        //    return UserMenuList;
        //}

    }
}
