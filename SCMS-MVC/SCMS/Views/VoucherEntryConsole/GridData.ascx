﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
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
<div class="CustomCell" style="width: 97px; height: 30px;">
    Location</div>
<div class="CustomCell" style="width: 270px; height: 30px;">
    <%= Html.DropDownList("ddl_Location", null, "All", new { style = "width:250px;", onchange = "VoucherEntryConsole(ddl_Location.value.toString(),ddl_VoucherType.value.toString())" })%>
</div>
<div class="CustomCell" style="width: 97px; height: 30px;">
    Voucher Type</div>
<div class="CustomCell" style="width: 270px; height: 30px;">
    <%= Html.DropDownList("ddl_VoucherType", null, "All", new { style = "width:250px;", onchange = "VoucherEntryConsole(ddl_Location.value,ddl_VoucherType.value)" })%>
</div>
<table id="VoucherEntryConsoleGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 6%;">
                Action
            </th>
            <th style="width: 10%;">
                Location
            </th>
            <th style="width: 10%;">
                Voucher Type
            </th>
            <th style="width: 8%;">
                Voucher #
            </th>
            <th style="width: 8%;">
                Date
            </th>
            <th style="width: 10%;">
                Dr.
            </th>
            <th style="width: 10%;">
                Cr.
            </th>
            <th style="width: 15%;">
                Diff
            </th>
            <th style="width: 13%;">
                Remarks
            </th>
            <th style="width: 10%;">
                Status
            </th>
        </tr>
    </thead>
    <tbody>
        <%List<SCMSDataLayer.DB.sp_VoucherEntryConsoleResult> DataList = new List<SCMSDataLayer.DB.sp_VoucherEntryConsoleResult>();

          DataList = new SCMSDataLayer.DALVoucherEntry().GetVoucherEntryConsoleData(Convert.ToInt32(ViewData["AllLoc"]), ViewData["LocationId"].ToString(),
                                                                                    Convert.ToInt32(ViewData["AllVchrType"]), ViewData["VoucherTypeId"].ToString(),
                                                                                    1, "", "", true);

          if (DataList != null && DataList.Count > 0)
          {
              foreach (SCMSDataLayer.DB.sp_VoucherEntryConsoleResult DataRow in DataList)
              {%>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td style="float: left">
                <%if (DataRow.VchMas_Status != "Cancelled")
                  { %>
                <div onclick="javascript:window.location='../Voucher/VoucherEntry?VoucherId=<%=DataRow.VchMas_Id %>'"
                    style="width: 22px; padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px;" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=DataRow.VchMas_Id %>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px;" />
                </div>
                <%} %>
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
            <td id="txt_TotalDrAmount" style="vertical-align: middle;">
                <%=string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", Convert.ToInt32(DataRow.TotalDrAmount))%>
            </td>
            <td id="txt_TotalCrAmount" style="vertical-align: middle;">
                <%=string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", Convert.ToInt32(DataRow.TotalCrAmount))%>
            </td>
            <td id="txt_DiffAmount" style="vertical-align: middle;">
                <%=string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", Convert.ToInt32(DataRow.DifferenceAmount))%>
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
