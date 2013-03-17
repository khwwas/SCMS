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

        public int DeleteRecordByGroupId(int Grp_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserRights where Grp_Id=" + Grp_Id);
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }
    }
}
