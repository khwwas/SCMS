<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<style type="text/css">
    select
    {
        background: none;
        width: auto;
        padding: 0;
        margin: 0;
        border-radius: 0px;
    }
    input[type="text"]
    {
        margin-bottom: 0;
    }
</style>
<table id="CompanyGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 70px;">
                Company Name
            </th>
            <th style="width: 70px;">
                Location Name
            </th>
            <th style="width: 70px;">
                User Group Name
            </th>
            <th style="width: 70px;">
                User Name
            </th>
            <th style="width: 80px;">
                &nbsp;
            </th>
        </tr>
    </thead>
    <tbody>
        <%
          // Get all users 
          List<SCMSDataLayer.DB.SECURITY_User> ListUsers = new List<SCMSDataLayer.DB.SECURITY_User>();
          ListUsers = new SCMSDataLayer.DALUser().GetAllUser().ToList();

          // Get location name show in list
          List<SCMSDataLayer.DB.SETUP_Location> ListLocations = new List<SCMSDataLayer.DB.SETUP_Location>();
          ListLocations = new SCMSDataLayer.DALLocation().GetAllLocation().ToList();

          // Get Company name show in list
          List<SCMSDataLayer.DB.SETUP_Company> ListCompanies = new List<SCMSDataLayer.DB.SETUP_Company>();
          ListCompanies = new SCMSDataLayer.DALCompany().GetAllCompanies().ToList();

          // Get All User Groups
          List<SCMSDataLayer.DB.SECURITY_UserGroup> ListGroupUser = new List<SCMSDataLayer.DB.SECURITY_UserGroup>();
          ListGroupUser = new SCMSDataLayer.DALUserGroup().GetAllUserGroup().ToList();

          var UserList = new SCMSDataLayer.DALUser().GetAllUser();
          if (UserList != null && UserList.Count > 0)
          {
              foreach (SCMSDataLayer.DB.SECURITY_User UserRow in ListUsers)
              {
                  SCMSDataLayer.DB.SECURITY_UserGroup UserGroupRow = ListGroupUser.Where(c => c.UsrGrp_Id.Equals(UserRow.UsrGrp_Id)).SingleOrDefault();
                  SCMSDataLayer.DB.SETUP_Company CompanyRow = ListCompanies.Where(c => c.Cmp_Id.Equals(UserRow.Cmp_Id)).SingleOrDefault();
                  SCMSDataLayer.DB.SETUP_Location LocationRow = ListLocations.Where(L => L.Loc_Id.Equals(UserRow.Loc_Id)).SingleOrDefault(); 
        %>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td id="ddl_Company<%=UserRow.User_Id %>" style="vertical-align: middle;">
                <input type="hidden" value="<%=CompanyRow.Cmp_Id %>" />
                <%=CompanyRow.Cmp_Title%>
            </td>
            <td id="ddl_Location<%=UserRow.User_Id%>" style="vertical-align: middle;">
                <input type="hidden" value="<%=LocationRow.Loc_Id %>" />
                <%=LocationRow.Loc_Title%>
            </td>
            <td id="ddl_UserGroup<%=UserRow.User_Id%>" style="vertical-align: middle;">
                <input type="hidden" value="<%=UserGroupRow.UsrGrp_Id %>" />
                <%=UserGroupRow.UsrGrp_Title%>
            </td>
            <td id="txt_User<%=UserRow.User_Id%>" style="vertical-align: middle;">
                <%=UserRow.User_Name%>
            </td>
            
            <td style="float: right;">
                <div onclick="javascript:return EditRecord('<%=UserRow.User_Id %>')" style="width: 32px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 32px;" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=UserRow.User_Id %>')" style="width: 32px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 32px;" />
                </div>
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
