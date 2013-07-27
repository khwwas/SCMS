<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<style type="text/css">
    select
    {
        background: none;
        width: auto;
        padding: 0;
        margin: 0;
        border-radius: 0px;
        -webkit-border-radius: 0px;
        -moz-border-radius: 0px;
    }
    input[type="text"]
    {
        margin-bottom: 0;
    }
</style>
<table id="LocationGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 6%;">
                Action
            </th>
            <th style="width: 6%;">
                Code
            </th>
            <th style="width: 48%;">
                Title
            </th>
        </tr>
    </thead>
    <tbody>
        <%var lList_Data = new SCMSDataLayer.DALApprovalLevel().GetAllData();
          if (lList_Data != null && lList_Data.Count > 0)
          {
              foreach (SCMSDataLayer.DB.sp_GetApprovalLevelListResult lRow_Data in lList_Data)
              {%>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td>
                <div onclick="javascript:return EditRecord('<%=lRow_Data.AprvLvl_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px; vertical-align: middle" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=lRow_Data.AprvLvl_Id %>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
            <td id="txt_Code<%=lRow_Data.AprvLvl_Id%>" style="vertical-align: middle;">
                <%=lRow_Data.AprvLvl_Code%>
            </td>
            <td id="txt_Title<%=lRow_Data.AprvLvl_Id%>" style="vertical-align: middle;">
                <%=lRow_Data.AprvLvl_Title%>
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
