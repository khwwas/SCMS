using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALMaritalStatus
    {
        public int Save(SETUP_MaritalStatus newSetupMaritalStatus)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_MaritalStatus existingMaritalStatus = dbSCMS.SETUP_MaritalStatus.Where(c => c.MS_Id.Equals(newSetupMaritalStatus.MS_Id)).SingleOrDefault();
                if (existingMaritalStatus != null)
                {
                    existingMaritalStatus.MS_Title = newSetupMaritalStatus.MS_Title;
                    existingMaritalStatus.MS_Abbreviation = newSetupMaritalStatus.MS_Abbreviation;
                    existingMaritalStatus.MS_Active = newSetupMaritalStatus.MS_Active;
                    existingMaritalStatus.MS_SortOrder = newSetupMaritalStatus.MS_SortOrder;
                }
                else
                {
                    dbSCMS.SETUP_MaritalStatus.InsertOnSubmit(newSetupMaritalStatus);
                }
                dbSCMS.SubmitChanges();
                return Convert.ToInt32(newSetupMaritalStatus.MS_Id);
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteById(string Id)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                int result = dbSCMS.ExecuteCommand("Delete From SETUP_MaritalStatus where MS_Id='" + Id + "'");
                return 1;
            }
            catch
            {
                return 0;
            }
        }


        public List<SETUP_MaritalStatus> GetAllData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_MaritalStatus.Where(c => c.MS_Active == 1).OrderBy(c => c.MS_Id).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
