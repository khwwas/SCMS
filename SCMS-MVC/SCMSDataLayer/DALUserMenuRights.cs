using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALUserMenuRights
    {
        public List<sp_GetUserMenuRightsResult> GetUserMenuRights(string GourpId)
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.sp_GetUserMenuRights(GourpId).ToList();
        }

        public List<sp_GetUserMenuRightsByUserIdResult> GetUserMenuRightsByUserId(string UserId)
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.sp_GetUserMenuRightsByUserId(UserId).ToList();
        }

        public List<sp_GetUserLocationsByUserIdResult> GetUserLocationsByUserId(string UserId)
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.sp_GetUserLocationsByUserId(UserId).ToList();
        }

        public List<sp_GetUserLocationsByGroupIdResult> GetUserLocationsByGroupId(string GourpId)
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.sp_GetUserLocationsByGroupId(GourpId).ToList();
        }

        public int SaveRecord(Security_UserRight pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                dbSCMS.Security_UserRights.InsertOnSubmit(pRow_NewData);
                dbSCMS.SubmitChanges();
                li_ReturnValue = Convert.ToInt32(pRow_NewData.Mnu_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public int SetUserLocations(Security_UserLocation pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                dbSCMS.Security_UserLocations.InsertOnSubmit(pRow_NewData);
                dbSCMS.SubmitChanges();
                li_ReturnValue = Convert.ToInt32(pRow_NewData.UserLoc_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public List<Security_MenuOption> MenuOptions()
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.Security_MenuOptions.ToList();
        }

        public int DeleteRecordByGroupId(int Grp_Id, int? UserId, int ModuleId)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                if (UserId != null)
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserRights where Grp_Id=" + Grp_Id + " And UsrSec_UserId=" + UserId + " And Mod_Id=" + ModuleId);
                }
                else
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserRights where Grp_Id=" + Grp_Id + " And Mod_Id=" + ModuleId);
                }
            }
            catch
            {
                li_ReturnValue = -1;
            }

            return li_ReturnValue;
        }

        public int DeleteLocationsByGroupId(string Grp_Id, string UserId)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                if (UserId != null)
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserLocations where UsrGrp_Id='" + Grp_Id + "' And User_Id='" + UserId + "'");
                }
                else
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserLocations where UsrGrp_Id='" + Grp_Id + "'");
                }
            }
            catch
            {
                li_ReturnValue = -1;
            }

            return li_ReturnValue;
        }
    }
}
