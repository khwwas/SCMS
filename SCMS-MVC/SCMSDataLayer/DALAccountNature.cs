using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALAccountNature
    {
        public int SaveRecord(SYSTEM_AccountNature pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SYSTEM_AccountNature lRow_ExistingData = dbSCMS.SYSTEM_AccountNatures.Where(c => c.AccNatr_Id.Equals(pRow_NewData.AccNatr_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.AccNatr_Title = pRow_NewData.AccNatr_Title;
                }
                else
                {
                    dbSCMS.SYSTEM_AccountNatures.InsertOnSubmit(pRow_NewData);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(pRow_NewData.AccNatr_Id);
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
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SYSTEM_AccountNature where AccNatr_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public List<SYSTEM_AccountNature> GetAllRecords() 
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SYSTEM_AccountNatures.OrderBy(c => c.AccNatr_Code).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<SYSTEM_AccountNature> PopulateData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SYSTEM_AccountNatures.Where(c => c.AccNatr_Active == 1).OrderBy(c => c.AccNatr_Code).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
