using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer.DB
{
    public class DALCustomer
    {
        public int SaveRecord(SETUP_Customer row_Customer)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_Customer Row_ExistingData = dbSCMS.SETUP_Customers.Where(c => c.Cust_Id.Equals(row_Customer.Cust_Id)).SingleOrDefault();

                if (Row_ExistingData != null)
                {
                    Row_ExistingData.Cust_Code = row_Customer.Cust_Code;
                    Row_ExistingData.Cmp_Id = row_Customer.Cmp_Id;
                    Row_ExistingData.Loc_Id = row_Customer.Loc_Id;
                    Row_ExistingData.CustType_Id = row_Customer.CustType_Id;
                    Row_ExistingData.Cust_Title = row_Customer.Cust_Title;
                    Row_ExistingData.Cust_Address1 = row_Customer.Cust_Address1;
                    Row_ExistingData.Cust_Address2 = row_Customer.Cust_Address2;
                    Row_ExistingData.Cust_Email = row_Customer.Cust_Email;
                    Row_ExistingData.Cust_Phone = row_Customer.Cust_Phone;
                    Row_ExistingData.Cust_Fax = row_Customer.Cust_Fax;

                }
                else
                {
                    dbSCMS.SETUP_Customers.InsertOnSubmit(row_Customer);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(row_Customer.Cust_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_Customer> GetAllCustomer()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Customers.Where(c => c.Cust_Active == 1).OrderBy(c => c.Cust_Id).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int DeleteRecordById(String CusID)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Setup_Customer where Cust_Id='" + CusID + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_Location> GetAllLocation()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Locations.Where(c => c.Loc_Active == 1).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
