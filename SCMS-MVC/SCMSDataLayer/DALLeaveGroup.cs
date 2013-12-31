using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALLeaveGroup
    {
        public int SaveRecord(SETUP_LeaveGroup lrow_LeaveGroup)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_LeaveGroup lRow_ExistingData = dbSCMS.SETUP_LeaveGroups.Where(c => c.LevGrp_Id.Equals(lrow_LeaveGroup.LevGrp_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.LevGrp_Title = lrow_LeaveGroup.LevGrp_Title;
                    lRow_ExistingData.Loc_Id = lrow_LeaveGroup.Loc_Id;
                }
                else
                {
                    dbSCMS.SETUP_LeaveGroups.InsertOnSubmit(lrow_LeaveGroup);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(lrow_LeaveGroup.LevGrp_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_LeaveGroup> GetAllRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_LeaveGroups.Where(c => c.LevGrp_Active == 1).OrderBy(c => c.LevGrp_Code).ToList();
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


        public int DeleteRecordById(String ps_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SETUP_LeaveGroup where LevGrp_Id='" + ps_Id + "'");
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
