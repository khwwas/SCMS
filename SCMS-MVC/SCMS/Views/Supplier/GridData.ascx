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
                Supplier Type
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
            var SupplierData = new SCMSDataLayer.DB.DALSupplier().GetAllSupplier();

          // Get location name show in list
          List<SCMSDataLayer.DB.SETUP_Location> ListLocations = new List<SCMSDataLayer.DB.SETUP_Location>();
          ListLocations = new SCMSDataLayer.DALLocation().GetAllLocation().ToList();

          // Get Company name show in list
          List<SCMSDataLayer.DB.SETUP_Company> ListCompanies = new List<SCMSDataLayer.DB.SETUP_Company>();
          ListCompanies = new SCMSDataLayer.DALCompany().GetAllCompanies().ToList();

          // Get all Supplier type
          List<SCMSDataLayer.DB.SETUP_SupplierType> ListSupplierType = new List<SCMSDataLayer.DB.SETUP_SupplierType>();
          ListSupplierType = new SCMSDataLayer.DALSupplierType().GetAllSupplierType().ToList();
          
          // Get all cities
          List<SCMSDataLayer.DB.SETUP_City> ListCity = new List<SCMSDataLayer.DB.SETUP_City>();
          ListCity = new SCMSDataLayer.DALCity().GetCities().ToList();

          if (SupplierData != null && SupplierData.Count > 0)
          {
              foreach (SCMSDataLayer.DB.SETUP_Supplier RowSupplier in SupplierData)
              {
                  SCMSDataLayer.DB.SETUP_Company CompanyRow = ListCompanies.Where(c => c.Cmp_Id.Equals(RowSupplier.Cmp_Id)).SingleOrDefault();
                  SCMSDataLayer.DB.SETUP_Location LocationRow = ListLocations.Where(L => L.Loc_Id.Equals(RowSupplier.Loc_Id)).SingleOrDefault();
                  SCMSDataLayer.DB.SETUP_SupplierType SupplierTypeRow = ListSupplierType.Where(CT => CT.SuppType_Id.Equals(RowSupplier.SuppType_Id)).SingleOrDefault();
                  SCMSDataLayer.DB.SETUP_City CityRow = ListCity.Where(C => C.City_Id.Equals(RowSupplier.City_Id)).SingleOrDefault();
                  
                  %>
        <tr class='odd gradeX' style='line-height: 15px;'>
            <td>
                <div onclick="javascript:return EditRecord('<%=RowSupplier.Supp_Id %>')" style="width: 22px;
                    padding-right: 5px; float: left; cursor: pointer;">
                    <img alt="Edit" src="../../img/edit.png" style="width: 22px; vertical-align: middle" />
                </div>
                <div onclick="javascript:return DeleteRecord('<%=RowSupplier.Supp_Id %>')" style="width: 22px;
                    float: left; cursor: pointer;">
                    <img alt="Delete" src="../../img/delete.png" style="width: 22px; vertical-align: middle" />
                </div>
            </td>
            <td id="ddl_Company<%=RowSupplier.Supp_Id%>" style="vertical-align: middle;">
                <%=CompanyRow.Cmp_Title%>
            </td>
            <td id="ddl_location<%=RowSupplier.Supp_Id%>" style="vertical-align: middle;">
                <%=LocationRow.Loc_Title%>
            </td>
            <td id="ddl_SupplierType<%=RowSupplier.Supp_Id%>" style="vertical-align: middle;">
                <%=SupplierTypeRow.SuppType_Title%>
            </td>
            <td id="ddl_City<%=RowSupplier.Supp_Id%>" style="vertical-align: middle;">
                <%=CityRow.City_Title%>
            </td>
            <td id="txt_Title<%=RowSupplier.Supp_Id%>" style="vertical-align: middle;">
                <%=RowSupplier.Supp_Title%>
            </td>
            <td id="txt_Address1<%=RowSupplier.Supp_Id%>" style="vertical-align: middle;">
                <%=RowSupplier.Supp_Address1%>
            </td>
            <td id="txt_Address2<%=RowSupplier.Supp_Id%>" style="vertical-align: middle;">
                <%=RowSupplier.Supp_Address2%>
            </td>
            <td id="txt_Email<%=RowSupplier.Supp_Id%>" style="vertical-align: middle;">
                <%=RowSupplier.Supp_Email%>
            </td>
            <td id="txt_Phone<%=RowSupplier.Supp_Id%>" style="vertical-align: middle;">
                <%=RowSupplier.Supp_Phone%>
            </td>
            <td id="txt_Fax<%=RowSupplier.Supp_Id%>" style="vertical-align: middle;">
                <%=RowSupplier.Supp_Fax%>
            </td>
        </tr>
        <%}
          }
          
        %>
    </tbody>
</table>
