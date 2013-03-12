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
    }
}
