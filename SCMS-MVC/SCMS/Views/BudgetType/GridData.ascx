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
<table id="BudgetTypeGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 6%;">
                Action
            </th>
            <th style="width: 6%;">
                Code
            </th>
            <th style="width: 6%;">
                Prefix
            </th>
            <th style="width: 30%;">
                Title
            </th>
        </tr>
    </thead>
    <tbody>
        <%var BudgetTypeList = new SCMSDataLayer.DALBudgetType().PopulateData();
          if (BudgetTypeList != null && BudgetTypeList.Count > 0)
          {
              foreach (SCMSDataLayer.DB.sp_PopulateBudgetTypeListResult BudgetTypeRow in BudgetTypeList)
              {%>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td style="float: left">
                <div onclick="javascript:return EditRecord('<%=BudgetTypeRow.BgdtType_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px;" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=BudgetTypeRow.BgdtType_Id %>')"
                    style="width: 22px; float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px;" />
                </div>
            </td>
            <td id="txt_Code<%=BudgetTypeRow.BgdtType_Id%>" style="vertical-align: middle;">
                <%=BudgetTypeRow.BgdtType_Code%>
            </td>
            <td id="txt_Prefix<%=BudgetTypeRow.BgdtType_Id%>" style="vertical-align: middle;">
                <%=BudgetTypeRow.BgdtType_Prefix%>
            </td>
            <td id="txt_Title<%=BudgetTypeRow.BgdtType_Id%>" style="vertical-align: middle;">
                <%=BudgetTypeRow.BgdtType_Title%>
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
