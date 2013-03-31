using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALJobPosition
    {
        public int SaveJobPosition(SETUP_JobPosition newSetupJobPosition)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_JobPosition existingSetupJobPosition = dbSCMS.SETUP_JobPositions.Where(c => c.JP_Id.Equals(newSetupJobPosition.JP_Id)).SingleOrDefault();
                if (existingSetupJobPosition != null)
                {
                    existingSetupJobPosition.JP_Title = newSetupJobPosition.JP_Title;
                    existingSetupJobPosition.Cmp_Id = newSetupJobPosition.Cmp_Id;
                    existingSetupJobPosition.Loc_Id = newSetupJobPosition.Loc_Id;
                    existingSetupJobPosition.JP_SortOrder = newSetupJobPosition.JP_SortOrder;
                    existingSetupJobPosition.JP_Active = newSetupJobPosition.JP_Active;
                    existingSetupJobPosition.JP_Remarks = newSetupJobPosition.JP_Remarks;
                    existingSetupJobPosition.JT_Id = newSetupJobPosition.JT_Id;
                    existingSetupJobPosition.Dpt_Id = newSetupJobPosition.Dpt_Id;
                    existingSetupJobPosition.FA_Id = newSetupJobPosition.FA_Id;

                }
                else
                {
                    dbSCMS.SETUP_JobPositions.InsertOnSubmit(newSetupJobPosition);
                }
                dbSCMS.SubmitChanges();
                return Convert.ToInt32(newSetupJobPosition.JP_Id);
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteJobPositionById(String JobPositionId)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                int result = dbSCMS.ExecuteCommand("Delete From SETUP_JobPosition where JP_Id='" + JobPositionId + "'");
                return 1;
            }
            catch
            {
                return 0;
            }
        }


        public List<sp_GetJobPositionListResult> GetAllData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_GetJobPositionList().ToList();
            }
            catch
            {
                return null;
            }
        }

        

    }
}
