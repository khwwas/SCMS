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
<table id="VoucherEntryConsoleGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 6%;">
                Action
            </th>
            <th style="width: 15%;">
                Location
            </th>
            <th style="width: 15%;">
                Voucher Type
            </th>
            <th style="width: 15%;">
                Code
            </th>
            <th style="width: 13%;">
                Date
            </th>
            <th style="width: 25%;">
                Remarks
            </th>
            <th style="width: 11%;">
                Status
            </th>
        </tr>
    </thead>
    <tbody>
        <%var DataList = new SCMSDataLayer.DALVoucherEntry().GetVoucherEntryConsoleData(1, "", 1, "", 1, "", "");
          if (DataList != null && DataList.Count > 0)
          {
              foreach (SCMSDataLayer.DB.sp_VoucherEntryConsoleResult DataRow in DataList)
              {%>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td style="float: left">
                <div onclick="javascript:window.location='../Voucher/VoucherEntry?VoucherId=<%=DataRow.VchMas_Id %>'"
                    style="width: 22px; padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px;" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=DataRow.VchMas_Id %>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px;" />
                </div>
            </td>
            <td id="txt_Location<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.Loc_Title%>
            </td>
            <td id="txt_VoucherType<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.VchrType_Title%>
            </td>
            <td id="txt_Code<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.VchMas_Code%>
            </td>
            <td id="txt_Date<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%=Convert.ToDateTime(DataRow.VchMas_Date).ToString("MM/dd/yyyy")%>
            </td>
            <td id="txt_Remarks<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.VchMas_Remarks%>
            </td>
            <td id="txt_Status<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.VchMas_Status%>
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
