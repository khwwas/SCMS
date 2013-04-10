using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALModules
    {
        public List<SETUP_Module> GetAllData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Modules.ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<SETUP_Module> GetUserModules(string GroupId)
        {
            SCMSDataContext dbSCMS = Connection.Create();
            string Query = "";

            Query += " Select * ";
            Query += "   From SETUP_Module ";
            Query += "  Where SETUP_Module.Mod_Id in ( Select Distinct Mod_Id ";
            Query += "                                   From Security_UserRights, ";
            Query += "                                        SECURITY_User ";
            Query += "                                  Where Security_UserRights.Grp_Id = '" + GroupId + "' ) ";

            List<SETUP_Module> modulesList = (List<SETUP_Module>)dbSCMS.ExecuteQuery<SETUP_Module>(Query, "").ToList();
            return modulesList;
        }
    }
}
