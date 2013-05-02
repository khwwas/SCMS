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
<table id="ChartOfAccountGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 5%; display: none;">
                Sr No.
            </th>
            <th style="width: 6%;">
                Action
            </th>
            <th style="width: 30%;">
                Code
            </th>
            <th style="width: 59%;">
                Title
            </th>
        </tr>
    </thead>
    <tbody>
        <%var lList_Data = new SCMSDataLayer.DALChartOfAccount().GetAllRecordsFirstLevel();

          if (lList_Data != null && lList_Data.Count > 0)
          {
              int count = 0;
              foreach (SCMSDataLayer.DB.SETUP_ChartOfAccount lRow_Data in lList_Data)
              {
                  string tempValue = "";
                  for (int index = 0; index < lRow_Data.ChrtAcc_Code.Length; index++)
                  {
                      if (index == 2 || index == 5 || index == 9 || index == 14 || index == 19 || index == 24)
                      {
                          tempValue += "-" + lRow_Data.ChrtAcc_Code[index];
                      }
                      else
                      {
                          tempValue += lRow_Data.ChrtAcc_Code[index];
                      }
                  }
                  lRow_Data.ChrtAcc_Code = tempValue;
                  count++;%>
        <tr class='odd' style='line-height: 15px;'>
            <td style="display: none;">
                <%=count %>
            </td>
            <td>
                <div onclick="javascript:return EditRecord('<%=lRow_Data.ChrtAcc_Id %>', '<%=lRow_Data.ChrtAcc_Code %>')"
                    style="width: 22px; padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
            <td id="txt_Code<%=lRow_Data.ChrtAcc_Id%>" style="vertical-align: middle;">
                <%=lRow_Data.ChrtAcc_Code%>
            </td>
            <td id="txt_Title<%=lRow_Data.ChrtAcc_Id%>" style="vertical-align: middle;">
                <%=lRow_Data.ChrtAcc_Title%>
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />