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
            <th style="width: 28%;">
                Title
            </th>
            <th style="width: 15%;">
                Address 1
            </th>
            <th style="width: 15%;">
                Address 2
            </th>
            <th style="width: 10%;">
                Email
            </th>
            <th style="width: 10%;">
                Phone
            </th>
            <th style="width: 10%;">
                Fax
            </th>
        </tr>
    </thead>
    <tbody>
        <%var CompaniesList = new SCMSDataLayer.DALCompany().GetAllData();
          if (CompaniesList != null && CompaniesList.Count > 0)
          {
              foreach (SCMSDataLayer.DB.sp_GetCompanyListResult CompanyRow in CompaniesList)
              {%>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td style="float: left">
                <div onclick="javascript:return EditRecord('<%=CompanyRow.Cmp_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px;" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=CompanyRow.Cmp_Id %>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px;" />
                </div>
            </td>
            <td id="txt_Code<%=CompanyRow.Cmp_Id%>" style="vertical-align: middle;">
                <%=CompanyRow.Cmp_Code%>
            </td>
            <td id="txt_Title<%=CompanyRow.Cmp_Id%>" style="vertical-align: middle;">
                <%=CompanyRow.Cmp_Title%>
            </td>
            <td id="txt_Address1<%=CompanyRow.Cmp_Id%>" style="vertical-align: middle;">
                <%=CompanyRow.Cmp_Address1%>
            </td>
            <td id="txt_Address2<%=CompanyRow.Cmp_Id%>" style="vertical-align: middle;">
                <%=CompanyRow.Cmp_Address2%>
            </td>
            <td id="txt_Email<%=CompanyRow.Cmp_Id%>" style="vertical-align: middle;">
                <%=CompanyRow.Cmp_Email%>
            </td>
            <td id="txt_Phone<%=CompanyRow.Cmp_Id%>" style="vertical-align: middle;">
                <%=CompanyRow.Cmp_Phone%>
            </td>
            <td id="txt_Fax<%=CompanyRow.Cmp_Id%>" style="vertical-align: middle;">
                <%=CompanyRow.Cmp_Fax%>
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
