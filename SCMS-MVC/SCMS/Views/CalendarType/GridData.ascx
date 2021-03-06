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
<table id="CityGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 5%;">
                Action
            </th>
            <th style="width: 10%;">
                Code
            </th>
            <th style="width: 15%;">
                Title
            </th>
            <!--
            <th style="width: 10%;">
                Level
            </th>
            <th style="width: 10%;">
                Company
            </th>
            <th style="width: 10%;">
                Location
            </th>-->
        </tr>
    </thead>
    <tbody>
        <% 
            
            var lList_Data = new SCMSDataLayer.DALCalendarType().GetAllRecords();

            // Get location name show in list
            List<SCMSDataLayer.DB.SETUP_Location> ListLocations = new List<SCMSDataLayer.DB.SETUP_Location>();
            ListLocations = new SCMSDataLayer.DALLocation().GetAllLocation().ToList();

            // Get Company name show in list
            List<SCMSDataLayer.DB.SETUP_Company> ListCompanies = new List<SCMSDataLayer.DB.SETUP_Company>();
            ListCompanies = new SCMSDataLayer.DALCompany().GetAllCompanies().ToList();

            if (lList_Data != null && lList_Data.Count > 0)
            {
                foreach (SCMSDataLayer.DB.SETUP_CalendarType lRow_Data in lList_Data)
                {
                    SCMSDataLayer.DB.SETUP_Company CompanyRow = ListCompanies.Where(c => c.Cmp_Id.Equals(lRow_Data.Cmp_Id)).SingleOrDefault();
                    SCMSDataLayer.DB.SETUP_Location LocationRow = ListLocations.Where(L => L.Loc_Id.Equals(lRow_Data.Loc_Id)).SingleOrDefault();
                  
                  
        %>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td>
                <div onclick="javascript:return EditRecord('<%=lRow_Data.CldrType_Id%>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px; vertical-align: middle" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=lRow_Data.CldrType_Id%>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
            <td id="Td1" style="vertical-align: middle;">
                <%=lRow_Data.CldrType_Code%>
            </td>
            <td id="txt_Title<%=lRow_Data.CldrType_Id%>" style="vertical-align: middle;">
                <%=lRow_Data.CldrType_Title%>
            </td>
        </tr>
        <%}
            }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
