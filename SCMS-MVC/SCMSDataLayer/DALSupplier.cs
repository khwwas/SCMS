using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer.DB
{
    public class DALSupplier
    {
        public int SaveRecord(SETUP_Supplier row_Supplier)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_Supplier Row_ExistingData = dbSCMS.SETUP_Suppliers.Where(c => c.Supp_Id.Equals(row_Supplier.Supp_Id)).SingleOrDefault();

                if (Row_ExistingData != null)
                {
                    Row_ExistingData.Supp_Code = row_Supplier.Supp_Code;
                    Row_ExistingData.Cmp_Id = row_Supplier.Cmp_Id;
                    Row_ExistingData.Loc_Id = row_Supplier.Loc_Id;
                    Row_ExistingData.SuppType_Id = row_Supplier.Supp_Id;
                    Row_ExistingData.Supp_Title = row_Supplier.Supp_Title;
                    Row_ExistingData.Supp_Address1 = row_Supplier.Supp_Address1;
                    Row_ExistingData.Supp_Address2 = row_Supplier.Supp_Address2;
                    Row_ExistingData.Supp_Email = row_Supplier.Supp_Email;
                    Row_ExistingData.Supp_Phone = row_Supplier.Supp_Phone;
                    Row_ExistingData.Supp_Fax = row_Supplier.Supp_Fax;

                }
                else
                {
                    dbSCMS.SETUP_Suppliers.InsertOnSubmit(row_Supplier);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(row_Supplier.Supp_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_Supplier> GetAllSupplier()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Suppliers.Where(c => c.Supp_Active == 1).OrderBy(c => c.Supp_Id).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int DeleteRecordById(String SuppID)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Setup_Supplier where Supp_Id='" + SuppID + "'");
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
