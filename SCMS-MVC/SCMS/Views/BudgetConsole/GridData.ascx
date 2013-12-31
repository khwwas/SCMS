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
<div class="CustomCell" style="width: 97px; height: 30px;">
    Location</div>
<div class="CustomCell" style="width: 270px; height: 30px;">
    <%= Html.DropDownList("ddl_Location", null, "All", new { style = "width:250px;", onchange = "BudgetConsole(ddl_Location.value.toString())" })%>
</div>
<%--<div class="CustomCell" style="width: 97px; height: 30px;">
    Voucher Type</div>
<div class="CustomCell" style="width: 270px; height: 30px;">
    <%= Html.DropDownList("ddl_VoucherType", null, "All", new { style = "width:250px;", onchange = "BudgetConsole(ddl_Location.value())" })%>
</div>--%>
<table id="BudgetConsoleGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 6%;">
                Action
            </th>
            <th style="width: 10%;">
                Location
            </th>
            <th style="width: 10%;">
                Budget Year
            </th>
            <th style="width: 8%;">
                Budget #
            </th>
            <th style="width: 8%;">
                Date
            </th>
            <th style="width: 10%;">
                Approved Budget
            </th>
            <th style="width: 10%;">
                Actual Expense (As on today)
            </th>
            <th style="width: 15%;">
                Remaining Amount
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
        <%List<SCMSDataLayer.DB.sp_BudgetConsoleResult> DataList = new List<SCMSDataLayer.DB.sp_BudgetConsoleResult>();

          DataList = new SCMSDataLayer.DALBudgetEntry().GetBudgetEntryConsoleData(Convert.ToInt32(ViewData["AllLoc"]), ViewData["LocationId"].ToString(), true);

          if (DataList != null && DataList.Count > 0)
          {
              foreach (SCMSDataLayer.DB.sp_BudgetConsoleResult DataRow in DataList)
              {%>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td style="float: left">
                <%if (DataRow.BgdtMas_Status != "Cancelled")
                  { %>
                <div onclick="javascript:window.location='../Budget?p_BudgetId=<%=DataRow.BgdtMas_Id %>'"
                    style="width: 22px; padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px;" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=DataRow.BgdtMas_Id %>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px;" />
                </div>
                <%} %>
            </td>
            <td id="txt_Location<%=DataRow.BgdtMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.Loc_Title%>
            </td>
            <td id="txt_VoucherType<%=DataRow.BgdtMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.Cldr_Title%>
            </td>
            <td id="txt_Code<%=DataRow.BgdtMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.BgdtMas_Code%>
            </td>
            <td id="txt_Date<%=DataRow.BgdtMas_Id%>" style="vertical-align: middle;">
                <%=Convert.ToDateTime(DataRow.BgdtMas_Date).ToString("MM/dd/yyyy")%>
            </td>
            <td id="txt_ApprovedBudet" style="vertical-align: middle;">
                <%=string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", Convert.ToInt32(DataRow.ApprovedBudget))%>
            </td>
            <td id="txt_ActualExpense" style="vertical-align: middle;">
                <%=string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", Convert.ToInt32(DataRow.ActualExpense))%>
            </td>
            <td id="txt_RemainingAmount" style="vertical-align: middle;">
                <%=string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", Convert.ToInt32(DataRow.RemainingAmount))%>
            </td>
            <td id="txt_Remarks<%=DataRow.BgdtMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.BgdtMas_Remarks%>
            </td>
            <td id="txt_Status<%=DataRow.BgdtMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.BgdtMas_Status%>
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
