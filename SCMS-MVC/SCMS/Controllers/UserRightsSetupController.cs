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
<<<<<<< HEAD
            ViewData["ddl_UserGroups"] = new SelectList(userGroups, "UsrGrp_Id", "UsrGrp_Title", "");
            return View("UserRights");
        }

        public string GetUserMenus(string GroupId)
        {
            //List<sp_GetUserMenuRightsResult> MenuRights = new DALUserMenuRights().GetUserMenuRights(GroupId).OrderBy(c => c.Mnu_Level).ToList();
            string UserMenuList = "";
            //if (MenuRights != null && MenuRights.Count > 0)
            //{
            //    int count = 0;
            //    UserMenuList += "<ul>";
            //    foreach (sp_GetUserMenuRightsResult row in MenuRights)
            //    {
            //        count++;
            //        string node = row.Mnu_Level.Contains(".") ? "Child" : "Parent";
            //        if (node == "Parent")
            //        {
            //            if (UserMenuList.Length > 4)
            //            {
            //                UserMenuList += "</ul>";
            //                UserMenuList += "</li>";
            //            }
            //            UserMenuList += "<li item-checked='" + (row.SelectedMenu > 0 ? "true" : "false") + "' item-expanded='true'>" + row.Mnu_Description;
            //            UserMenuList += "<ul>";
            //        }
            //        else if (node == "Child")
            //        {
            //            UserMenuList += "<li item-checked='" + (row.SelectedMenu > 0 ? "true" : "false") + "'>" + row.Mnu_Description;
            //            if (count == MenuRights.Count)
            //            {
            //                UserMenuList += "</ul>";
            //                UserMenuList += "</li>";
            //            }
            //        }
            //    }
            //    UserMenuList += "</ul>";
            //}
            return UserMenuList;
=======
            if (String.IsNullOrEmpty(SelectedGroup))
            {
                SelectedGroup = userGroups[0].UsrGrp_Id;
            }
            ViewData["ddl_UserGroups"] = new SelectList(userGroups, "UsrGrp_Id", "UsrGrp_Title", SelectedGroup);
            List<sp_GetUserMenuRightsResult> MenuRights = new DALUserMenuRights().GetUserMenuRights(SelectedGroup).OrderBy(c => c.Mnu_Level).ToList();
            ViewData["UserMenuRights"] = MenuRights;
            return View("UserRights");
>>>>>>> ced69bb335154e3ea4aa4f28f74e2e1645b45fbc
        }

        //public string GetUserMenus(string GroupId)
        //{
        //    List<sp_GetUserMenuRightsResult> MenuRights = new DALUserMenuRights().GetUserMenuRights(GroupId).OrderBy(c => c.Mnu_Level).ToList();
        //    string UserMenuList = "";
            
        //    return UserMenuList;
        //}

    }
}
