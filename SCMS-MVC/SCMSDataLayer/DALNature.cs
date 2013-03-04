using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALNature
    {
        public int SaveRecord(SYSTEM_Nature pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SYSTEM_Nature lRow_ExistingData = dbSCMS.SYSTEM_Natures.Where(c => c.Natr_Id.Equals(pRow_NewData.Natr_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.Natr_Title = pRow_NewData.Natr_Title;
                }
                else
                {
                    dbSCMS.SYSTEM_Natures.InsertOnSubmit(pRow_NewData);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(pRow_NewData.Natr_Id);
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
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SYSTEM_Nature where Natr_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public List<SYSTEM_Nature> GetAllRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SYSTEM_Natures.OrderBy(c => c.Natr_Code).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<SYSTEM_Nature> PopulateData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SYSTEM_Natures.Where(c => c.Natr_Active == 1).OrderBy(c => c.Natr_Code).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
