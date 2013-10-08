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
<table id="BankReconciliationGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 5%;">
                Action
            </th>
            <th style="width: 10%;">
                Location
            </th>
            <th style="width: 14%;">
                Bank
            </th>
            <th style="width: 8%;">
                Date
            </th>
            <th style="width: 10%;">
                Voucher #
            </th>
            <th style="width: 22%;">
                Remarks
            </th>
            <th style="width: 15%;">
                Amount
            </th>
            <th style="width: 8%;">
                Reconciled
            </th>
            <th style="width: 8%;">
                Reconciliation Date
            </th>
        </tr>
    </thead>
    <tbody>
        <%var DataList = new SCMSDataLayer.DALBankReconciliation().GetBankReconciliationData(1, "", 1, "", "");
          if (DataList != null && DataList.Count > 0)
          {
              foreach (SCMSDataLayer.DB.sp_BankReconciliationResult DataRow in DataList)
              {%>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td>
                <div onclick="javascript:return EditRecord('<%=DataRow.VchMas_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
            <td id="txt_Location<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.Loc_Title%>
            </td>
            <td id="txt_Bank<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.Bank_Title%>
            </td>
            <td id="txt_VoucherDate<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%=Convert.ToDateTime(DataRow.VchMas_Date).ToString("MM/dd/yyyy")%>
            </td>
            <td id="txt_Code<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.VchMas_Code%>
            </td>
            <td id="txt_Remarks<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.VchMas_Remarks%>
            </td>
            <td id="txt_TotalAmount<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%=DataRow.VchMas_CrAmount%>
            </td>
            <td id="txt_Reconciliation<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                    <%bool Reconcilation = false;
                      if (DataRow.VchMas_Reconciliation == null || DataRow.VchMas_Reconciliation <= 0 || DataRow.VchMas_Reconciliation.ToString() == "")
                      {
                          Reconcilation = false;
                      }
                      else
                      {
                          Reconcilation = true; 
                      }
                      Response.Write(Reconcilation);
                    %>
            </td>
            <td id="txt_ReconciliationDate<%=DataRow.VchMas_Id%>" style="vertical-align: middle;">
                <%string ReconcilationData = "";
                  if (DataRow.VchMas_ReconciliationDate != null && DataRow.VchMas_ReconciliationDate != DateTime.MinValue)
                  {
                      ReconcilationData = Convert.ToDateTime(DataRow.VchMas_ReconciliationDate).ToString("MM/dd/yyyy");
                  }
                  Response.Write(ReconcilationData);
                %>
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
