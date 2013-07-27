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
                Location
            </th>
        </tr>
    </thead>
    <tbody>
        <%var FunctionalAreaList = new SCMSDataLayer.DALFunctionalArea().GetAllData();
          if (FunctionalAreaList != null && FunctionalAreaList.Count > 0)
          {
              foreach (SCMSDataLayer.DB.sp_GetFunctionalAreaListResult FunctionalAreaRow in FunctionalAreaList)
              {%>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td style="float: left">
                <div onclick="javascript:return EditRecord('<%=FunctionalAreaRow.FA_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px;" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=FunctionalAreaRow.FA_Id %>')"
                    style="width: 22px; float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px;" />
                </div>
            </td>
            <td id="txt_Code<%=FunctionalAreaRow.FA_Id%>" style="vertical-align: middle;">
                <%=FunctionalAreaRow.FA_Code%>
            </td>
            
            <td id="txt_Title<%=FunctionalAreaRow.FA_Id%>" style="vertical-align: middle;">
                <%=FunctionalAreaRow.FA_Title%>
            </td>
           
            <td style="vertical-align: middle;">
                <%=FunctionalAreaRow.Loc_Title%>
                 <input type="hidden" id="ddl_location<%=FunctionalAreaRow.FA_Id%>" value="<%=FunctionalAreaRow.Loc_Id%>" />
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
