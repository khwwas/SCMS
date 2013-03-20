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
            <th style="width: 15%;">
                Prefix
            </th>
            <th style="width: 24%;">
                Title
            </th>
            <th style="width: 6%;">
                Initialize Code On
            </th>
            <%--<th style="width: 15%;">
                Location
            </th>--%>
        </tr>
    </thead>
    <tbody>
        <%var VoucherTypeList = new SCMSDataLayer.DALVoucherType().GetAllData();
          if (VoucherTypeList != null && VoucherTypeList.Count > 0)
          {
              foreach (SCMSDataLayer.DB.sp_GetVoucherTypesListResult VoucherTypeRow in VoucherTypeList)
              {%>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td style="float: left">
                <div onclick="javascript:return EditRecord('<%=VoucherTypeRow.VchrType_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px;" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=VoucherTypeRow.VchrType_Id %>')"
                    style="width: 22px; float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px;" />
                </div>
            </td>
            <td id="txt_Code<%=VoucherTypeRow.VchrType_Id%>" style="vertical-align: middle;">
                <%=VoucherTypeRow.VchrType_Code%>
            </td>
            <td id="txt_Prefix<%=VoucherTypeRow.VchrType_Id%>" style="vertical-align: middle;">
                <%=VoucherTypeRow.VchrType_Prefix%>
            </td>
            <td id="txt_Title<%=VoucherTypeRow.VchrType_Id%>" style="vertical-align: middle;">
                <%=VoucherTypeRow.VchrType_Title%>
            </td>
            <td id="txt_CodeInitialization<%=VoucherTypeRow.VchrType_Id%>" style="vertical-align: middle;">
                <% if (VoucherTypeRow.VchrType_CodeInitialization == Convert.ToInt32(SCMSDataLayer.CodeInitialization.Month))
                   {
                       Response.Write("Month");
                   }
                   else
                   {
                       Response.Write("Year");
                   }
                %>
            </td>
            <%--<td id="txt_Location<%=VoucherTypeRow.VchrType_Id%>" style="vertical-align: middle;">
                <%=VoucherTypeRow.Loc_Title%>
            </td>--%>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
