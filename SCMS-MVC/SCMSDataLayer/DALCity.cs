using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALCity
    {
        public int SaveRecord(SETUP_City pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_City lRow_ExistingData = dbSCMS.SETUP_Cities.Where(c => c.City_Id.Equals(pRow_NewData.City_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.City_Title = pRow_NewData.City_Title;
                }
                else
                {
                    dbSCMS.SETUP_Cities.InsertOnSubmit(pRow_NewData);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(pRow_NewData.City_Id);
            }
            catch (Exception ex)
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
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Setup_City where City_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_City> GetAllRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Cities.Where(c => c.City_Active == 1).OrderBy(c => c.City_Code).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SETUP_City> GetCities()
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.SETUP_Cities.Where(c => c.City_Active == 1).OrderBy(c => c.City_Id).ToList();
        }

    }
}
