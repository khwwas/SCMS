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
            <th style="width: 6%;">
                Action
            </th>
            <th style="width: 6%;">
                Code
            </th>
            <th style="width: 24%;">
                Title
            </th>
            <th style="width: 15%;">
                Abbreviation
            </th>
            <th style="width: 6%;">
                Count
            </th>
            <th style="width: 15%;">
                Location
            </th>
        </tr>
    </thead>
    <tbody>
        <%var LeaveTypeList = new SCMSDataLayer.DALLeaveType().GetAllData();
          if (LeaveTypeList != null && LeaveTypeList.Count > 0)
          {
              foreach (SCMSDataLayer.DB.sp_GetLeaveTypesListResult LeaveTypeRow in LeaveTypeList)
              {%>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td style="float: left">
                <div onclick="javascript:return EditRecord('<%=LeaveTypeRow.LevTyp_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px;" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=LeaveTypeRow.LevTyp_Id %>')"
                    style="width: 22px; float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px;" />
                </div>
            </td>
            <td id="txt_Code<%=LeaveTypeRow.LevTyp_Id%>" style="vertical-align: middle;">
                <%=LeaveTypeRow.LevTyp_Code%>
            </td>
            <td id="txt_Title<%=LeaveTypeRow.LevTyp_Id%>" style="vertical-align: middle;">
                <%=LeaveTypeRow.LevTyp_Title%>
            </td>
            <td id="txt_Abbreviation<%=LeaveTypeRow.LevTyp_Id%>" style="vertical-align: middle;">
                <%=LeaveTypeRow.LevTyp_Abbreviation%>
            </td>
            <td  style="vertical-align: middle;">
               <%=LeaveTypeRow.LevTyp_Count%>
               <input type="hidden" id="txt_Count<%=LeaveTypeRow.LevTyp_Id%>" value="<%=LeaveTypeRow.LevTyp_Count%>" />
            </td>
            <td style="vertical-align: middle;">
                <%=LeaveTypeRow.Loc_Title%>
                 <input type="hidden" id="ddl_location<%=LeaveTypeRow.LevTyp_Id%>" value="<%=LeaveTypeRow.Loc_Id%>" />
               
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
