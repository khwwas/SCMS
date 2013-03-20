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
<table id="CustomerTypeGrid" class="data display datatable">
    <thead>
        <tr>
            <th style="width: 10%;">
                Action
            </th>
            <th style="width: 10%;">
                Company
            </th>
            <th style="width: 10%;">
                Location
            </th>
            <th style="width: 10%;">
                Customer Type
            </th>
            <th style="width: 10%;">
                City
            </th>
            <th style="width: 10%;">
                Title
            </th>
            <th style="width: 10%;">
                Address 1
            </th>
            <th style="width: 10%;">
                Address 2
            </th>
            <th style="width: 10%;">
                Email
            </th>
            <th style="width: 10%;">
                Phone
            </th>
            <th style="width: 10%;">
                Fax
            </th>
        </tr>
    </thead>
    <tbody>
        <% //var lList_Data = new SCMSDataLayer.DALCustomerType().GetAllCustomer();
          var CustomerData = new SCMSDataLayer.DB.DALCustomer().GetAllCustomer();

          // Get location name show in list
          List<SCMSDataLayer.DB.SETUP_Location> ListLocations = new List<SCMSDataLayer.DB.SETUP_Location>();
          ListLocations = new SCMSDataLayer.DALLocation().GetAllLocation().ToList();

          // Get Company name show in list
          List<SCMSDataLayer.DB.SETUP_Company> ListCompanies = new List<SCMSDataLayer.DB.SETUP_Company>();
          ListCompanies = new SCMSDataLayer.DALCompany().GetAllCompanies().ToList();
          
          // Get all customer type
          List<SCMSDataLayer.DB.SETUP_CustomerType> ListCutomerType = new List<SCMSDataLayer.DB.SETUP_CustomerType>();
          ListCutomerType = new SCMSDataLayer.DALCustomerType().GetAllCustomerType().ToList();
          
          // Get all cities
          List<SCMSDataLayer.DB.SETUP_City> ListCity = new List<SCMSDataLayer.DB.SETUP_City>();
          ListCity = new SCMSDataLayer.DALCity().GetCities().ToList();

          if (CustomerData != null && CustomerData.Count > 0)
          {
              foreach (SCMSDataLayer.DB.SETUP_Customer RowCustomer in CustomerData)
              {
                  SCMSDataLayer.DB.SETUP_Company CompanyRow = ListCompanies.Where(c => c.Cmp_Id.Equals(RowCustomer.Cmp_Id)).SingleOrDefault();
                  SCMSDataLayer.DB.SETUP_Location LocationRow = ListLocations.Where(L => L.Loc_Id.Equals(RowCustomer.Loc_Id)).SingleOrDefault();
                  SCMSDataLayer.DB.SETUP_CustomerType CustomerTypeRow = ListCutomerType.Where(CT => CT.CustType_Id.Equals(RowCustomer.CustType_Id)).SingleOrDefault();
                  SCMSDataLayer.DB.SETUP_City CityRow = ListCity.Where(C => C.City_Id.Equals(RowCustomer.City_Id)).SingleOrDefault();
                  
                  %>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td>
                <div onclick="javascript:return EditRecord('<%=RowCustomer.Cust_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px; vertical-align: middle" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=RowCustomer.Cust_Id %>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
            <td id="ddl_Company<%=RowCustomer.Cust_Id%>" style="vertical-align: middle;">
                <%=CompanyRow.Cmp_Title%>
            </td>
            <td id="ddl_location<%=RowCustomer.Cust_Id%>" style="vertical-align: middle;">
                <%=LocationRow.Loc_Title%>
            </td>
            <td id="ddl_CustomerType<%=RowCustomer.Cust_Id%>" style="vertical-align: middle;">
                <%=CustomerTypeRow.CustType_Title%>
            </td>
            <td id="ddl_City<%=RowCustomer.Cust_Id%>" style="vertical-align: middle;">
                <%=CityRow.City_Title%>
            </td>
            <td id="txt_Title<%=RowCustomer.Cust_Id%>" style="vertical-align: middle;">
                <%=RowCustomer.Cust_Title%>
            </td>
            <td id="txt_Address1<%=RowCustomer.Cust_Id%>" style="vertical-align: middle;">
                <%=RowCustomer.Cust_Address1%>
            </td>
            <td id="txt_Address2<%=RowCustomer.Cust_Id%>" style="vertical-align: middle;">
                <%=RowCustomer.Cust_Address2%>
            </td>
            <td id="txt_Email<%=RowCustomer.Cust_Id%>" style="vertical-align: middle;">
                <%=RowCustomer.Cust_Email%>
            </td>
            <td id="txt_Phone<%=RowCustomer.Cust_Id%>" style="vertical-align: middle;">
                <%=RowCustomer.Cust_Phone%>
            </td>
            <td id="txt_Fax<%=RowCustomer.Cust_Id%>" style="vertical-align: middle;">
                <%=RowCustomer.Cust_Fax%>
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
<input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
