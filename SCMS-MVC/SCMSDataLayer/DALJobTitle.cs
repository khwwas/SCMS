using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALJobTitle
    {
        public int SaveRecord(SETUP_JobTitle lrow_JobTitle)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_JobTitle lRow_ExistingData = dbSCMS.SETUP_JobTitles.Where(c => c.JT_Id.Equals(lrow_JobTitle.JT_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.JT_Title = lrow_JobTitle.JT_Title;
                    lRow_ExistingData.Loc_Id = lrow_JobTitle.Loc_Id;
                }
                else
                {
                    dbSCMS.SETUP_JobTitles.InsertOnSubmit(lrow_JobTitle);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(lrow_JobTitle.JT_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_JobTitle> GetAllRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_JobTitles.Where(c => c.JT_Active == 1).OrderBy(c => c.JT_Code).ToList();
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


        public int DeleteRecordById(String ps_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SETUP_JobTitle where JT_Id='" + ps_Id + "'");
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
