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
           
         </tr>
    </thead>
    <tbody>
        <% 
         var GenderData = new SCMSDataLayer.DALGender().GetAllData();
         if (GenderData != null && GenderData.Count > 0)
          {
              foreach (SCMSDataLayer.DB.SETUP_Gender RowGender in GenderData)
              {
                 
                  %>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td>
                <div onclick="javascript:return EditRecord('<%=RowGender.Gndr_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px; vertical-align: middle" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=RowGender.Gndr_Id %>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
           
            <td id="txt_Title<%=RowGender.Gndr_Id%>" style="vertical-align: middle;">
                <%=RowGender.Gndr_Title%>
            </td>
            <td id="txt_Abbreviation<%=RowGender.Gndr_Id%>" style="vertical-align: middle;">
                <%=RowGender.Gndr_Abbreviation%>
            </td>
           
            
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
