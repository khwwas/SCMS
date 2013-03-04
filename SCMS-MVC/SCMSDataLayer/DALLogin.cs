using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALLogin
    {
        SCMSDataContext dbSCMS = Connection.Create();
        public SECURITY_User ValidateUser(string ps_UserName, string ps_Password)
        {
            try
            {
                SECURITY_User user = dbSCMS.SECURITY_Users.Where(tbl => tbl.User_Title.Equals(ps_UserName) && tbl.User_Password.Equals(ps_Password)).SingleOrDefault();
                return user;
            }
            catch
            {
                return null;
            }
        }

    }
}
