using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALLocation
    {
        public int SaveRecord(SETUP_Location pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_Location lRow_ExistingData = dbSCMS.SETUP_Locations.Where(c => c.Loc_Id.Equals(pRow_NewData.Loc_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.Loc_Title = pRow_NewData.Loc_Title;
                }
                else
                {
                    dbSCMS.SETUP_Locations.InsertOnSubmit(pRow_NewData);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(pRow_NewData.Loc_Id);
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
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Setup_Location where Loc_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        //public List<SETUP_Location> GetAllRecords()
        //{
        //    try
        //    {
        //        //SCMSDataContext dbSCMS = Connection.Create();

        //        ////dbSCMS.SETUP_Locations.Where(



        //        //return dbSCMS.SETUP_Locations.Where(c => c.Loc_Active == 1).OrderBy(c => c.Loc_Code).ToList();
        //        return null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public List<sp_GetLocationListResult> GetAllData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_GetLocationList().ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<sp_PopulateLocationListResult> PopulateData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_PopulateLocationList().ToList();
            }
            catch
            {
                return null;
            }
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
