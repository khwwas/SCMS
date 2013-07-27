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
<table id="ChartOfAccountGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 5%; display: none;">
                Sr No.
            </th>
            <th style="width: 6%;">
                Action
            </th>
            <th style="width: 20%;">
                Code
            </th>
            <th style="width: 23%;">
                Title
            </th>
            <th style="width: 10%;">
                Level
            </th>
            <th style="width: 12%;">
                Budget Level
            </th>
            <th style="width: 12%;">
                Type
            </th>
            <th style="width: 12%;">
                Nature
            </th>
            <%--<th style="width: 12%;">
                Account Nature
            </th>--%>
        </tr>
    </thead>
    <tbody>
        <%var lList_Data = new SCMSDataLayer.DALChartOfAccount().GetAllRecords();
          var IList_AccountNature = new SCMSDataLayer.DALAccountNature().GetAllRecords();
          var IList_Nature = new SCMSDataLayer.DALNature().GetAllRecords();
          if (lList_Data != null && lList_Data.Count > 0)
          {
              int count = 0;
              foreach (SCMSDataLayer.DB.SETUP_ChartOfAccount lRow_Data in lList_Data)
              {
                  string tempValue = "";
                  string Title = lRow_Data.ChrtAcc_Title.Replace("'", "&#39");
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
                <div onclick="javascript:return DeleteRecord('<%=lRow_Data.ChrtAcc_Id %>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
            <td id="txt_Code<%=lRow_Data.ChrtAcc_Id%>" style="vertical-align: middle;">
                <%if (lRow_Data.ChrtAcc_Level == 2)
                  {
                      lRow_Data.ChrtAcc_Code = "&nbsp; " + lRow_Data.ChrtAcc_Code;
                  }
                %>
                <%else if (lRow_Data.ChrtAcc_Level == 3)
                  {
                      lRow_Data.ChrtAcc_Code = "&nbsp; &nbsp; " + lRow_Data.ChrtAcc_Code;
                  }
                %>
                <%else if (lRow_Data.ChrtAcc_Level == 4)
                  {
                      lRow_Data.ChrtAcc_Code = "&nbsp; &nbsp; &nbsp; " + lRow_Data.ChrtAcc_Code;
                  }
                %>
                <%else if (lRow_Data.ChrtAcc_Level == 5)
                  {
                      lRow_Data.ChrtAcc_Code = "&nbsp; &nbsp; &nbsp; &nbsp; " + lRow_Data.ChrtAcc_Code;
                  }
                %>
                <%else if (lRow_Data.ChrtAcc_Level == 6)
                  {
                      lRow_Data.ChrtAcc_Code = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" + lRow_Data.ChrtAcc_Code;
                  }
                %>
                <%=lRow_Data.ChrtAcc_Code%>
            </td>
            <td id="txt_Title<%=lRow_Data.ChrtAcc_Id%>" style="vertical-align: middle;">
                <%if (lRow_Data.ChrtAcc_Level == 2)
                  {
                      lRow_Data.ChrtAcc_Title = "&nbsp; " + lRow_Data.ChrtAcc_Title;
                  }
                %>
                <%else if (lRow_Data.ChrtAcc_Level == 3)
                  {
                      lRow_Data.ChrtAcc_Title = "&nbsp; &nbsp; " + lRow_Data.ChrtAcc_Title;
                  }
                %>
                <%else if (lRow_Data.ChrtAcc_Level == 4)
                  {
                      lRow_Data.ChrtAcc_Title = "&nbsp; &nbsp; &nbsp; " + lRow_Data.ChrtAcc_Title;
                  }
                %>
                <%else if (lRow_Data.ChrtAcc_Level == 5)
                  {
                      lRow_Data.ChrtAcc_Title = "&nbsp; &nbsp; &nbsp; &nbsp; " + lRow_Data.ChrtAcc_Title;
                  }
                %>
                <%else if (lRow_Data.ChrtAcc_Level == 6)
                  {
                      lRow_Data.ChrtAcc_Title = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" + lRow_Data.ChrtAcc_Title;
                  }
                %>
                <%=Title%>
            </td>
            <td id="txt_AccountLevel<%=lRow_Data.ChrtAcc_Id%>" style="vertical-align: middle;">
                <%=lRow_Data.ChrtAcc_Level%>
                <input type="hidden" id="ChrtAcc_Level<%=lRow_Data.ChrtAcc_Id%>" value="<%=lRow_Data.ChrtAcc_Level%>" />
            </td>
            <td id="txt_AccountBudgetLevel<%=lRow_Data.ChrtAcc_Id%>" style="vertical-align: middle;">
                <%=lRow_Data.ChrtAcc_BudgetLevel%>
                <input type="hidden" id="ChrtAcc_BudgetLevel<%=lRow_Data.ChrtAcc_Id%>" value="<%=lRow_Data.ChrtAcc_BudgetLevel%>" />
            </td>
            <td id="txt_AccountType<%=lRow_Data.ChrtAcc_Id%>" style="vertical-align: middle;">
                <%if (lRow_Data.ChrtAcc_Type == 1)
                  {
                      Response.Write("Group");
                  }
                  else
                  {
                      Response.Write("Detail");
                  } %>
                <input type="hidden" id="ChrtAcc_Type<%=lRow_Data.ChrtAcc_Id%>" value="<%=lRow_Data.ChrtAcc_Type%>" />
            </td>
            <td id="txt_NatureId<%=lRow_Data.ChrtAcc_Id%>" style="vertical-align: middle;">
                <%String Nature = IList_Nature.Where(c => c.Natr_Id.Equals(lRow_Data.Natr_Id)).SingleOrDefault().Natr_Title;
                  if (Nature == "None")
                  {
                      Nature = "";
                  }
                  Response.Write(Nature);
                %>
                <input type="hidden" id="Natr_Id<%=lRow_Data.ChrtAcc_Id%>" value="<%=lRow_Data.Natr_Id%>" />
            </td>
            <%--<td id="txt_AccountNatureId<%=lRow_Data.ChrtAcc_Id%>" style="vertical-align: middle;">
                <%String AccountNature = IList_AccountNature.Where(c => c.AccNatr_Id.Equals(lRow_Data.AccNatr_Id)).SingleOrDefault().AccNatr_Title;
                  if (AccountNature == "None")
                  {
                      AccountNature = "";
                  }
                  Response.Write(AccountNature);
                %>
                <input type="hidden" id="AccNatr_Id<%=lRow_Data.ChrtAcc_Id%>" value="<%=lRow_Data.AccNatr_Id%>" />
            </td>--%>
            <%--<input type="hidden" id="ChrtAcc_Active<%=lRow_Data.ChrtAcc_Id%>" value="<%=lRow_Data.ChrtAcc_Active%>" />--%>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />