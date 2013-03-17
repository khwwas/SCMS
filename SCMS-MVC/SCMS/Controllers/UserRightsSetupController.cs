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

        public ActionResult GetUserMenus(string GroupId)
        {
            List<sp_GetUserMenuRightsResult> MenuRights = new DALUserMenuRights().GetUserMenuRights(GroupId).OrderBy(c => c.Mnu_Level).ToList();
            ViewData["UserMenuRights"] = MenuRights;
            return PartialView("TreeView");
        }

        public string SaveUserRights(int GroupId, string UserMenus)
        {
            try
            {
                string[] UserMenuIds = UserMenus.Split(',');
                DALUserMenuRights objUserMenuRights = new DALUserMenuRights();
                int Success = objUserMenuRights.DeleteRecordByGroupId(GroupId);
                if (Success > 0)
                {
                    foreach (string userMenuId in UserMenuIds)
                    {
                        Security_UserRight userRightRow = new Security_UserRight();
                        string Code = "";
                        if (DALCommon.AutoCodeGeneration("Security_UserRights") == 1)
                        {
                            Code = DALCommon.GetMaximumCode("Security_UserRights");
                        }
                        userRightRow.UsrSec_Code = Code;
                        userRightRow.Grp_Id = GroupId;
                        userRightRow.Mnu_Id = Convert.ToInt32(userMenuId);
                        objUserMenuRights.SaveRecord(userRightRow);
                    }
                }
                return "1";
            }
            catch
            {
                return "0";
            }
        }

    }
}
