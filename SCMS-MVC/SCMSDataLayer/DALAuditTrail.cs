using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALAuditTrail
    {
        public int SaveRecord(SYSTEM_AuditTrail lrow_AuditTrail)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                dbSCMS.SYSTEM_AuditTrails.InsertOnSubmit(lrow_AuditTrail);
                dbSCMS.SubmitChanges();
                li_ReturnValue = Convert.ToInt32(lrow_AuditTrail.AdtTrl_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }
    }
}
