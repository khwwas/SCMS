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
<table id="CityGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 10%;">
                Action
            </th>
            <th style="width: 10%;">
                Title
            </th>
            <th style="width: 10%;">
                Location
            </th>
            <th style="width: 10%;">
                Bank
            </th>

        </tr>
    </thead>
    <tbody>
        <% 
            
            var lList_Data = new SCMSDataLayer.DALBankAccount().GetAllRecords();

            // Get location name show in list
            List<SCMSDataLayer.DB.SETUP_Location> ListLocations = new List<SCMSDataLayer.DB.SETUP_Location>();
            ListLocations = new SCMSDataLayer.DALLocation().GetAllLocation().ToList();


            // Get Bank name show in list
            List<SCMSDataLayer.DB.SETUP_Bank> ListBanks = new List<SCMSDataLayer.DB.SETUP_Bank>();
            ListBanks = new SCMSDataLayer.DALBank().PopulateData().ToList();

            if (lList_Data != null && lList_Data.Count > 0)
            {
                foreach (SCMSDataLayer.DB.SETUP_BankAccount lRow_Data in lList_Data)
                {
                    SCMSDataLayer.DB.SETUP_Location LocationRow = ListLocations.Where(L => L.Loc_Id.Equals(lRow_Data.Loc_Id)).SingleOrDefault();
                    SCMSDataLayer.DB.SETUP_Bank BankRow = ListBanks.Where(B => B.Bank_Id.Equals(lRow_Data.Bank_Id)).SingleOrDefault();
                  
                  
        %>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td>
                <div onclick="javascript:return EditRecord('<%=lRow_Data.BankAcc_Id%>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px; vertical-align: middle" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=lRow_Data.BankAcc_Id%>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
            <td id="txt_Title<%=lRow_Data.BankAcc_Id%>" style="vertical-align: middle;">
                <%=lRow_Data.BankAcc_Title%>
            </td>
            <td id="ddl_location<%=lRow_Data.BankAcc_Id%>" style="vertical-align: middle;">
            <%if (LocationRow != null)
                  { %>
                <%=LocationRow.Loc_Title%>
                <%} %>
                
            </td>
            <td id="ddl_bank<%=lRow_Data.BankAcc_Id%>" style="vertical-align: middle;">
            <%if (BankRow != null)
                  { %>
                <%=BankRow.Bank_Title%>
                <%} %>
                
            </td>

        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
