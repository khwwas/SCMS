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
<table id="CustomerTypeGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 10%;">
                Action
            </th>
            <th style="width: 10%;">
                Department
            </th>
            <th style="width: 10%;">
                Location
            </th>
           
            <th style="width: 10%;">
                Employee Type
            </th>
           
            <th style="width: 10%;">
                Leave Type
            </th>
           
            <th style="width: 10%;">
                Leave Group
            </th>
            <th style="width: 10%;">
             Shft
            </th>
            

         </tr>
    </thead>
    <tbody>
        <% 
         string id = ViewData["EmpId"] != null ? ViewData["EmpId"].ToString() : "0";
         var EmployeePlacmentData = new SCMSDataLayer.DALEmpPlacement().GetAllData(id);
         if (EmployeePlacmentData != null && EmployeePlacmentData.Count > 0)
          {
              foreach (SCMSDataLayer.DB.sp_GetEmployeePlacementsResult RowEmployee in EmployeePlacmentData)
              {
                 
        %>
          <tr class='odd gradeX' style='line-height: 15px;'>
            <td>
                <div onclick="javascript:return EditRecord('<%=RowEmployee.Plcmt_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px; vertical-align: middle" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=RowEmployee.Plcmt_Id %>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
           
            <td  style="vertical-align: middle;">
                <%=RowEmployee.Dpt_Title%>
                <input type="hidden" id="ddl_Department<%=RowEmployee.Plcmt_Id%>" value="<%=RowEmployee.Dpt_Id%>" />
            </td>
            <td  style="vertical-align: middle;">
                <%=RowEmployee.Loc_Title%>
                <input type="hidden" id="ddl_Location<%=RowEmployee.Plcmt_Id%>" value="<%=RowEmployee.Loc_Id%>" />
            </td>
           
            <td style="vertical-align: middle;">
                <%=RowEmployee.JT_Title%>
                <input type="hidden" id="ddl_EmployeeType<%=RowEmployee.Plcmt_Id%>" value="<%=RowEmployee.JT_Id%>" />
            </td>
           
            <td style="vertical-align: middle;">
                <%=RowEmployee.LevTyp_Title%>
                <input type="hidden" id="ddl_LeaveType<%=RowEmployee.Plcmt_Id%>" value="<%=RowEmployee.LevTyp_Id%>" />
            </td>

            <td style="vertical-align: middle;">
                <%=RowEmployee.LevGrp_Title%>
                <input type="hidden" id="ddl_LeaveGroup<%=RowEmployee.Plcmt_Id%>" value="<%=RowEmployee.LevGrp_Id%>" />
            </td>

            <td style="vertical-align: middle;">
                <%=RowEmployee.Shft_Title%>
                <input type="hidden" id="ddl_Shift<%=RowEmployee.Plcmt_Id%>" value="<%=RowEmployee.Shft_Id%>" />
            </td>

        </tr>
        <%}
       }
       %>
    </tbody>
</table>
<input type="hidden" id="SavePlacementResult" value='<%=ViewData["SaveResult"] %>' />
