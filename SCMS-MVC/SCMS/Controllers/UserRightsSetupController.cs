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

        public ActionResult Index()
        {
            List<sp_GetUserGroupListResult> userGroups = new DALUserGroup().GetAllData().ToList();
            ViewData["ddl_UserGroups"] = new SelectList(userGroups, "UsrGrp_Id", "UsrGrp_Title");
            List<sp_GetUserMenuRightsResult> MenuRights = new DALUserMenuRights().GetUserMenuRights("").Where(c => c.Mod_Id == SystemParameters.ModuleId && c.Mnu_Level != "0").ToList();
            ViewData["UserMenuRights"] = MenuRights;
            return View("UserRights");
        }

        public string GetUserMenus(string GroupId, string UserId)
        {
            List<sp_GetUserMenuRightsByUserIdResult> MenuRights = new DALUserMenuRights().GetUserMenuRightsByUserId(Convert.ToInt32(UserId).ToString()).Where(c => c.Mod_Id == SystemParameters.ModuleId && c.Mnu_Level != "0").ToList();
            string response = "";
            response += "<table id='MenuGrid' class='display' style='width: 100%; padding: 2px;'>";
            response += "<thead>";
            response += "<tr class='odd gradeX' style='line-height: 15px; cursor: pointer; text-align: left;";
            response += "background-color: #ccc;'>";
            response += "<th style='vertical-align: middle; width: 30%; padding-left: 3px;'>";
            response += "Menu Description";
            response += "</th>";
            response += "<th style='vertical-align: middle; width: 10%;'>";
            response += "Is Allowed";
            response += "</th>";
            response += "<th style='vertical-align: middle; width: 10%;'>";
            response += "Can Add";
            response += "</th>";
            response += "<th style='vertical-align: middle; width: 10%;'>";
            response += "Can Edit";
            response += "</th>";
            response += "<th style='vertical-align: middle; width: 10%;'>";
            response += "Can Delete";
            response += "</th>";
            response += "<th style='vertical-align: middle; width: 10%;'>";
            response += "Can Print";
            response += "</th>";
            response += "<th style='vertical-align: middle; width: 10%;'>";
            response += "Can Import";
            response += "</th>";
            response += "</tr>";
            response += "</thead>";
            response += "<tbody>";
            int count = 0;
            foreach (SCMSDataLayer.DB.sp_GetUserMenuRightsByUserIdResult menuRight in MenuRights)
            {
                count++;
                string node = menuRight.Mnu_Level.Contains(".") ? "Child" : "Parent";

                response += "<tr class='odd gradeX' style='line-height: 15px; cursor: pointer;'>";
                if (node == "Parent")
                {
                    response += "<th style='vertical-align: middle; text-align: left; padding-left: 3px;'>";
                    response += menuRight.Mnu_Description;
                    response += "</th>";
                }
                else
                {
                    response += "<td style='vertical-align: middle; padding-left: 50px;'>";
                    response += menuRight.Mnu_Description;
                    response += "</td>";
                }
                response += "<td style='vertical-align: middle;'>";
                response += "<input type='checkbox' class='allowed' value='ChkDesc" + menuRight.Mnu_Level + "' id='ChkDesc" + menuRight.Mnu_Id + "'";
                response += menuRight.SelectedMenu == 0 ? "" : "checked='checked'" + "/>";
                response += "</td>";
                if (menuRight.Mnu_Level == "3" || menuRight.Mnu_Level.Contains("3."))
                {
                    response += "<td></td>";
                    response += "<td></td>";
                    response += "<td></td>";
                    response += "<td></td>";
                    response += "<td></td>";
                }
                else
                {
                    response += "<td style='vertical-align: middle;'>";
                    response += "<input type='checkbox' class='add' value='ChkAdd" + menuRight.Mnu_Level + "' id='ChkAdd" + menuRight.Mnu_Id + "'";
                    response += menuRight.CanAdd == false ? "" : "checked='checked'" + "/>";
                    response += "</td>";
                    response += "<td style='vertical-align: middle;'>";
                    response += "<input type='checkbox' class='edit' value='ChkEdit" + menuRight.Mnu_Level + "' id='ChkEdit" + menuRight.Mnu_Id + "'";
                    response += menuRight.CanEdit == false ? "" : "checked='checked'" + "/>";
                    response += "</td>";
                    response += "<td style='vertical-align: middle;'>";
                    response += "<input type='checkbox' class='delete' value='ChkDelete" + menuRight.Mnu_Level + "'";
                    response += "id='ChkDelete" + menuRight.Mnu_Id + "'";
                    response += menuRight.CanDelete == false ? "" : "checked='checked'" + "/>";
                    response += "</td>";
                    response += "<td style='vertical-align: middle;'>";
                    response += "<input type='checkbox' class='print' value='ChkPrint" + menuRight.Mnu_Level + "' id='ChkPrint" + menuRight.Mnu_Id + "'";
                    response += menuRight.CanPrint == false ? "" : "checked='checked'" + "/>";
                    response += "</td>";
                    response += "<td style='vertical-align: middle;'>";
                    response += "<input type='checkbox' class='import' value='ChkImport" + menuRight.Mnu_Level + "'";
                    response += "id='ChkImport" + menuRight.Mnu_Id + "'";
                    response += menuRight.CanImport == false ? "" : "checked='checked'" + "/>";
                    response += "</td>";
                }
                response += "</tr>";
            }
            response += "</tbody>";
            response += "</table>";
            return response;
        }

        public string SaveUserRights(int GroupId, int? UserId, bool isGroup, string UserMenus)
        {
            try
            {
                string[] UserMenuIds = UserMenus.Split(',');
                DALUserMenuRights objUserMenuRights = new DALUserMenuRights();
                int Success = objUserMenuRights.DeleteRecordByGroupId(GroupId, UserId, SystemParameters.ModuleId);
                if (Success >= 0)
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
                        if (UserId != null)
                        {
                            userRightRow.UsrSec_UserId = UserId.ToString();
                        }
                        string[] cols = userMenuId.Split('~');
                        userRightRow.Mnu_Id = Convert.ToInt32(cols[0]);
                        userRightRow.UsrSec_Add = Convert.ToBoolean(cols[1]);
                        userRightRow.UsrSec_Edit = Convert.ToBoolean(cols[2]);
                        userRightRow.UsrSec_Delete = Convert.ToBoolean(cols[3]);
                        userRightRow.UsrSec_Print = Convert.ToBoolean(cols[4]);
                        userRightRow.UsrSec_Import = Convert.ToBoolean(cols[5]);
                        userRightRow.Mod_Id = SystemParameters.ModuleId;
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

        public string GetUsersByGroupId(string GroupId)
        {
            List<SCMSDataLayer.DB.sp_GetUserListResult> users = new List<SCMSDataLayer.DB.sp_GetUserListResult>();
            users = new SCMSDataLayer.DALUser().GetAllData();
            if (!String.IsNullOrEmpty(GroupId))
            {
                users = users.Where(u => u.UsrGrp_Id.Equals(GroupId)).ToList();
            }
            string response = "";
            foreach (SCMSDataLayer.DB.sp_GetUserListResult user in users)
            {
                response += "<table id='UserGrid' class=' display' style='width: 100%; padding: 2px;'>";
                response += "<tbody>";
                response += "<tr id='" + user.User_Id + "|" + user.UsrGrp_Id + "' class='odd gradeX' style='line-height: 15px; cursor: pointer;'>";
                response += "<td style='vertical-align: middle; width: 25%;'>";
                response += user.User_Code;
                response += "</td>";
                response += "<td style='vertical-align: middle; width: 35%;'>";
                response += user.User_Title;
                response += "</td>";
                response += "<td style='vertical-align: middle; width: 30%;'>";
                response += user.UsrGrp_Title;
                response += "</td>";
                response += "<td style='vertical-align: middle; text-align: right; padding-right: 3px;'>";
                response += "</td>";
                response += "</tr>";
                response += "</tbody>";
                response += "</table>";
            }

            return response;
        }

    }
}
