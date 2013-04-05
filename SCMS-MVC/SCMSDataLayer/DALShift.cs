using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALShift
    {
        public int SaveShift(SETUP_Shift newSetupShift)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_Shift existingSetupShift = dbSCMS.SETUP_Shifts.Where(c => c.Shft_Id.Equals(newSetupShift.Shft_Id)).SingleOrDefault();
                if (existingSetupShift != null)
                {
                    existingSetupShift.Shft_Title = newSetupShift.Shft_Title;
                    existingSetupShift.Shft_Abbreviation = newSetupShift.Shft_Abbreviation;
                    existingSetupShift.Shft_Active = newSetupShift.Shft_Active;
                    existingSetupShift.Shft_SortOrder = newSetupShift.Shft_SortOrder;
                    existingSetupShift.Shft_StartTime = newSetupShift.Shft_StartTime;
                    existingSetupShift.Shft_EndTime = newSetupShift.Shft_EndTime;
                   
                }
                else
                {
                    dbSCMS.SETUP_Shifts.InsertOnSubmit(newSetupShift);
                }
                dbSCMS.SubmitChanges();
                return Convert.ToInt32(newSetupShift.Shft_Id);
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteShiftById(String ShiftId)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                int result = dbSCMS.ExecuteCommand("Delete From SETUP_Shift where Shft_Id='" + ShiftId + "'");
                return 1;
            }
            catch
            {
                return 0;
            }
        }


        public List<SETUP_Shift> GetAllData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Shifts.Where(c => c.Shft_Active == 1).OrderBy(c => c.Shft_Id).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }
}
