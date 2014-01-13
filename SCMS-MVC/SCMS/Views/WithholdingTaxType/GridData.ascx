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
<table id="CompanyGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 10%;">
                Action
            </th>
            <th style="width: 15%;">
                Account Code
            </th>
            <th style="width: 20%;">
                Account Title
            </th>
            <th style="width: 10%;">
                Dr.
            </th>
            <th style="width: 10%;">
                Cr.
            </th>
            <th style="width: 40%;">
                Remarks
            </th>
        </tr>
    </thead>
    <tbody>
        <%var VoucherDetails = new SCMSDataLayer.DALVoucherEntry().GetAllDetailRecords();
          if (VoucherDetails != null && VoucherDetails.Count > 0)
          {
              foreach (SCMSDataLayer.DB.GL_VchrDetail VoucherDetailRow in VoucherDetails)
              {%>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td style="float: left">
                <div onclick="javascript:return EditRecord('<%=VoucherDetailRow.VchDet_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px;" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=VoucherDetailRow.VchDet_Id %>')"
                    style="width: 22px; float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px;" />
                </div>
            </td>
            <td id="txt_Address1<%=VoucherDetailRow.VchDet_Id%>" style="vertical-align: middle;">
                <%=VoucherDetailRow.VchMas_DrAmount%>
            </td>
            <td id="txt_Address2<%=VoucherDetailRow.VchDet_Id%>" style="vertical-align: middle;">
                <%=VoucherDetailRow.VchMas_CrAmount%>
            </td>
            <td id="txt_Email<%=VoucherDetailRow.VchDet_Id%>" style="vertical-align: middle;">
                <%=VoucherDetailRow.VchDet_Remarks%>
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="VoucherId" value="<%=ViewData["VoucherId"] %>" />
<input type="hidden" id="VoucherCode" value="<%=ViewData["VoucherCode"] %>" />
<input type="hidden" id="ReturnValue" value="<%=ViewData["SaveResult"] %>" />
