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
<table id="CustomerTypeGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 7%;">
                Action
            </th>
            <th style="width: 15%;">
                Title
            </th>
            <th style="width: 10%;">
                Abbreviation
            </th>
            <th style="width: 8%;">
                Start Time
            </th>
            <th style="width: 8%;">
                End Time 
            </th>

            <th style="width: 10%;">
               Break Start Time
            </th>
            <th style="width: 10%;">
               Break End Time 
            </th>
            <th style="width: 10%;">
               Break Duration
            </th>
            <th style="width: 8%;">
              Grace In
            </th>

            <th style="width: 10%;">
              Grace Early
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
            <td id="txt_BreakStartTime<%=RowShift.Shft_Id%>" style="vertical-align: middle;">
                <%=Convert.ToDateTime(RowShift.Shift_BreakStartTime).ToShortTimeString()%>
            </td>

            <td id="txt_BreakEndTime<%=RowShift.Shft_Id%>" style="vertical-align: middle;">
                <%=Convert.ToDateTime(RowShift.Shift_BreakEndTime).ToShortTimeString()%>
            </td>


            <td id="txt_BreakDuration<%=RowShift.Shft_Id%>" style="vertical-align: middle;">
                <%=Convert.ToDateTime(RowShift.Shift_BreakDuration).ToString("hh:mm")%>
            </td>

            <td  style="vertical-align: middle;">
                <%=RowShift.Shift_GraceIn%>
                 <input type="hidden" id="txt_GraceIn<%=RowShift.Shft_Id%>" value="<%=RowShift.Shift_GraceIn%>" />
            </td>

            <td style="vertical-align: middle;">
                <input type="hidden" id="txt_GraceEarly<%=RowShift.Shft_Id%>" value="<%=RowShift.Shift_GraceEarly%>" />
                <%=RowShift.Shift_GraceEarly%>
            </td>

        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
