using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALCustomerType
    {
        public int SaveRecord(SETUP_CustomerType pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_CustomerType lRow_ExistingData = dbSCMS.SETUP_CustomerTypes.Where(c => c.CustType_Id.Equals(pRow_NewData.CustType_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.CustType_Title = pRow_NewData.CustType_Title;
                }
                else
                {
                    dbSCMS.SETUP_CustomerTypes.InsertOnSubmit(pRow_NewData);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(pRow_NewData.CustType_Id);
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
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Setup_CustomerType where CustType_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_CustomerType> GetAllRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_CustomerTypes.Where(c => c.CustType_Active == 1).OrderBy(c => c.CustType_Code).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<SETUP_CustomerType> GetAllCustomerType()
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.SETUP_CustomerTypes.Where(c => c.CustType_Active == 1).OrderBy(c => c.CustType_Id).ToList(); ;
        }

    }
}
