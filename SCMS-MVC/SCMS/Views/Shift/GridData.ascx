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
            <th style="width: 20%;">
                Title
            </th>
            <th style="width: 10%;">
                Abbreviation
            </th>
            <th style="width: 15%;">
                Start Time
            </th>
            <th style="width: 15%;">
                End Time 
            </th>
         </tr>
    </thead>
    <tbody>
        <% 
         var ShiftData = new SCMSDataLayer.DALShift().GetAllData();
         if (ShiftData != null && ShiftData.Count > 0)
          {
              foreach (SCMSDataLayer.DB.SETUP_Shift RowShift in ShiftData)
              {
                 
                  %>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td>
                <div onclick="javascript:return EditRecord('<%=RowShift.Shft_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px; vertical-align: middle" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=RowShift.Shft_Id %>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
           
            <td id="txt_Title<%=RowShift.Shft_Id%>" style="vertical-align: middle;">
                <%=RowShift.Shft_Title%>
            </td>
            <td id="txt_Abbreviation<%=RowShift.Shft_Id%>" style="vertical-align: middle;">
                <%=RowShift.Shft_Abbreviation%>
            </td>
            <td id="txt_StartTime<%=RowShift.Shft_Id%>" style="vertical-align: middle;">
                <%=Convert.ToDateTime(RowShift.Shft_StartTime).ToShortTimeString()%>
            </td>
            <td id="txt_EndTime<%=RowShift.Shft_Id%>" style="vertical-align: middle;">
                <%=Convert.ToDateTime(RowShift.Shft_EndTime).ToShortTimeString()%>
            </td>
            
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
