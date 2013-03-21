using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALCalendarType
    {
        public int SaveRecord(SETUP_CalendarType lrow_CalendarType)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_CalendarType lRow_ExistingData = dbSCMS.SETUP_CalendarTypes.Where(b => b.CldrType_Id.Equals(lrow_CalendarType.CldrType_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.CldrType_Title = lrow_CalendarType.CldrType_Title;
                    lRow_ExistingData.Loc_Id = lrow_CalendarType.Loc_Id;
                    lRow_ExistingData.Cmp_Id = lrow_CalendarType.Cmp_Id;
                    lRow_ExistingData.CldrType_Level = lrow_CalendarType.CldrType_Level;
                }
                else
                {
                    dbSCMS.SETUP_CalendarTypes.InsertOnSubmit(lrow_CalendarType);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(lrow_CalendarType.CldrType_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_CalendarType> GetAllRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_CalendarTypes.Where(c => c.CldrType_Active == 1).OrderBy(c => c.CldrType_Code).ToList();
            }
            catch (Exception ex)
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

        public List<SETUP_Company> GetAllCompanies()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Companies.Where(c => c.Cmp_Active == 1).ToList();
            }
            catch
            {
                return null;
            }
        }

        public int DeleteRecordById(String ps_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Setup_CalendarType where CldrType_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_Bank> PopulateData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Banks.Where(c => c.Bank_Active == 1).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
