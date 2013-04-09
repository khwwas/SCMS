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
            <th style="width: 10%;">
                Title
            </th>
            <th style="width: 15%;">
                Remarks
            </th>

            <th style="width: 15%;">
                Location
            </th>

            <th style="width: 15%;">
                Functional Area
            </th>

        </tr>
    </thead>
    <tbody>
        <%var JobPositionList = new SCMSDataLayer.DALJobPosition().GetAllData();
          if (JobPositionList != null && JobPositionList.Count > 0)
          {
              foreach (SCMSDataLayer.DB.sp_GetJobPositionListResult JobPositionRow in JobPositionList)
              {%>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td style="float: left">
                <div onclick="javascript:return EditRecord('<%=JobPositionRow.JP_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px;" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=JobPositionRow.JP_Id %>')"
                    style="width: 22px; float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px;" />
                </div>
            </td>
            <td id="txt_Code<%=JobPositionRow.JP_Id%>" style="vertical-align: middle;">
                <%=JobPositionRow.JP_Code%>
            </td>
            
            <td id="txt_Title<%=JobPositionRow.JP_Id%>" style="vertical-align: middle;">
                <%=JobPositionRow.JP_Title%>
            </td>
           
            <td id="txt_Remarks<%=JobPositionRow.JP_Id%>" style="vertical-align: middle;">
                <%=JobPositionRow.JP_Remarks%>
            </td>

            <td style="vertical-align: middle;">
                <%=JobPositionRow.Loc_Title%>
            </td>

            <td  style="vertical-align: middle;">
                <%=JobPositionRow.FA_Title%>

                <input type="hidden" id="ddl_location<%=JobPositionRow.JP_Id%>" value="<%=JobPositionRow.Loc_Id%>" />
                <input type="hidden" id="ddl_functionalarea<%=JobPositionRow.JP_Id%>" value="<%=JobPositionRow.FA_Id%>" />
                <input type="hidden" id="ddl_departement<%=JobPositionRow.JP_Id%>" value="<%=JobPositionRow.Dpt_Id%>" />
                <input type="hidden" id="ddl_jobtitle<%=JobPositionRow.JP_Id%>" value="<%=JobPositionRow.JT_Id%>" />

            </td>
            

        </tr>
              
        <%}
        }
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
