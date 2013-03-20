<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id='jqxTree' style='margin-left: 0px;'>
    <% 
        List<SCMSDataLayer.DB.sp_GetUserMenuRightsResult> MenuRights = (List<SCMSDataLayer.DB.sp_GetUserMenuRightsResult>)ViewData["UserMenuRights"];
        if (MenuRights != null && MenuRights.Count > 0)
        {
            int count = 0;
            Response.Write("<ul>");
            foreach (SCMSDataLayer.DB.sp_GetUserMenuRightsResult row in MenuRights)
            {
                count++;
                string node = row.Mnu_Level.Contains(".") ? "Child" : "Parent";
                if (node == "Parent")
                {
                    if (count > 1)
                    {
                        Response.Write("</ul>");
                        Response.Write("</li>");
                    }
                    Response.Write("<li id=" + row.Mnu_Id + " item-checked='" + (row.SelectedMenu > 0 ? "true" : "false") + "' item-expanded='true'>" + row.Mnu_Description);
                    Response.Write("<ul>");
                }
                else if (node == "Child")
                {
                    Response.Write("<li id=" + row.Mnu_Id + " item-checked='" + (row.SelectedMenu > 0 ? "true" : "false") + "'>" + row.Mnu_Description + "</li>");
                    if (count == MenuRights.Count)
                    {
                        Response.Write("</ul>");
                        Response.Write("</li>");
                    }
                }
            }
            Response.Write("</ul>");
        }
    %>
</div>
