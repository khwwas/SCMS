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
                DoB
            </th>
           
            <th style="width: 10%;">
                Mobile
            </th>
           
            <th style="width: 10%;">
                Email
            </th>
           

         </tr>
    </thead>
    <tbody>
        <% 
         var EmployeeData = new SCMSDataLayer.DALEmployee().GetAllData();
         if (EmployeeData != null && EmployeeData.Count > 0)
          {
              foreach (SCMSDataLayer.DB.SETUP_Employee RowEmployee in EmployeeData)
              {
                 
                  %>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td>
                <div onclick="javascript:return EditRecord('<%=RowEmployee.Emp_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px; vertical-align: middle" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=RowEmployee.Emp_Id %>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
           
            <td  style="vertical-align: middle;">
                <%=RowEmployee.Emp_Title%>
            </td>
            <td  style="vertical-align: middle;">
                <%=Convert.ToDateTime(RowEmployee.Emp_DoB).ToShortDateString()%>
            </td>
           
            <td style="vertical-align: middle;">
                <%=RowEmployee.Emp_Mobile%>
            </td>
           
            <td style="vertical-align: middle;">
                <%=RowEmployee.Emp_Email%>
            </td>
           
            
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
