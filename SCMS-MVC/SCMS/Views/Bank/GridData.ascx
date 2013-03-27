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

        </tr>
    </thead>
    <tbody>
        <% 
            
            var lList_Data = new SCMSDataLayer.DALBank().GetAllRecords();

            // Get location name show in list
            List<SCMSDataLayer.DB.SETUP_Location> ListLocations = new List<SCMSDataLayer.DB.SETUP_Location>();
            ListLocations = new SCMSDataLayer.DALLocation().GetAllLocation().ToList();

            if (lList_Data != null && lList_Data.Count > 0)
            {
                foreach (SCMSDataLayer.DB.SETUP_Bank lRow_Data in lList_Data)
                {
                    SCMSDataLayer.DB.SETUP_Location LocationRow = ListLocations.Where(L => L.Loc_Id.Equals(lRow_Data.Loc_Id)).SingleOrDefault();
                  
                  
        %>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td>
                <div onclick="javascript:return EditRecord('<%=lRow_Data.Bank_Id%>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px; vertical-align: middle" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=lRow_Data.Bank_Id%>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
             <td id="txt_Title<%=lRow_Data.Bank_Id%>" style="vertical-align: middle;">
                <%=lRow_Data.Bank_Title%>
            </td>
            <td id="ddl_location<%=lRow_Data.Bank_Id%>" style="vertical-align: middle;">
            <%if (LocationRow != null)
                  { %>
                <%=LocationRow.Loc_Title%>
                <%} %>
                
            </td>
           
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
