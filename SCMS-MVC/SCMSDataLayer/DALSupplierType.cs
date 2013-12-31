using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALSupplierType
    {
        public int SaveRecord(SETUP_SupplierType pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_SupplierType lRow_ExistingData = dbSCMS.SETUP_SupplierTypes.Where(c => c.SuppType_Id.Equals(pRow_NewData.SuppType_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.SuppType_Title = pRow_NewData.SuppType_Title;
                }
                else
                {
                    dbSCMS.SETUP_SupplierTypes.InsertOnSubmit(pRow_NewData);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(pRow_NewData.SuppType_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public int DeleteRecordById(String ps_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Setup_SupplierType where SuppType_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_SupplierType> GetAllRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_SupplierTypes.Where(c => c.SuppType_Active == 1).OrderBy(c => c.SuppType_Code).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<SETUP_SupplierType> GetAllSupplierType()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_SupplierTypes.Where(c => c.SuppType_Active == 1).OrderBy(c => c.SuppType_Id).ToList();
            }
            catch
            {
                return null;
            }
        }

    }
}
