using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALFunctionalArea
    {

        public int SaveFunctionalArea(SETUP_FunctionalArea newSetupFunctionalArea)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_FunctionalArea existingSetupFunctionalArea = dbSCMS.SETUP_FunctionalAreas.Where(c => c.FA_Id.Equals(newSetupFunctionalArea.FA_Id)).SingleOrDefault();
                if (existingSetupFunctionalArea != null)
                {
                    existingSetupFunctionalArea.FA_Title = newSetupFunctionalArea.FA_Title;
                    existingSetupFunctionalArea.Cmp_Id = newSetupFunctionalArea.Cmp_Id;
                    existingSetupFunctionalArea.Loc_Id = newSetupFunctionalArea.Loc_Id;
                    existingSetupFunctionalArea.FA_SortOrder = newSetupFunctionalArea.FA_SortOrder;
                    existingSetupFunctionalArea.FA_Active = newSetupFunctionalArea.FA_Active;

                }
                else
                {
                    dbSCMS.SETUP_FunctionalAreas.InsertOnSubmit(newSetupFunctionalArea);
                }
                dbSCMS.SubmitChanges();
                return Convert.ToInt32(newSetupFunctionalArea.FA_Id);
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteFunctionalAreaById(String FunctionalAreaId)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                int result = dbSCMS.ExecuteCommand("Delete From SETUP_FunctionalArea where FA_Id='" + FunctionalAreaId + "'");
                return 1;
            }
            catch
            {
                return 0;
            }
        }


        public List<sp_GetFunctionalAreaListResult> GetAllData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_GetFunctionalAreaList().ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<sp_GetFunctionalAreaListResult> PopulateData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_GetFunctionalAreaList().ToList();
            }
            catch
            {
                return null;
            }
        }

    }
}
