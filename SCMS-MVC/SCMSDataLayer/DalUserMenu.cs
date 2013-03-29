using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DalUserMenu
    {

        public int SaveUserMenu(Security_UserRight newSetupUserRightRow)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                //Security_UserRight existingSetupUserMenuRow = dbSCMS.Security_UserRights.Where(c => c.UsrSec_Id.Equals(newSetupUserRightRow.UsrGrp_Id)).SingleOrDefault();
                //if (existingSetupUserMenuRow != null)
                //{
                //existingSetupUserMenuRow.Cmp_Id = newSetupUserRightRow.Cmp_Id;
                //existingSetupUserMenuRow.Loc_Id = newSetupUserRightRow.Loc_Id;
                //existingSetupUserMenuRow.UsrGrp_Title = newSetupUserRightRow.UsrGrp_Title;
                //}
                //else
                //{
                dbSCMS.Security_UserRights.InsertOnSubmit(newSetupUserRightRow);
                //}
                dbSCMS.SubmitChanges();
                // return Convert.ToInt32(newSetupUserRightRow.UsrGrp_Id);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        
        public List<Security_MenuOption> GetAllUserMenu()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.Security_MenuOptions.Where(c => c.Mnu_Active == 1).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<Security_UserRight> GetAllUserRights()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.Security_UserRights.Where(c => c.UsrSec_Active == 1).ToList();
            }
            catch
            {
                return null;
            }
        }

    }
}
