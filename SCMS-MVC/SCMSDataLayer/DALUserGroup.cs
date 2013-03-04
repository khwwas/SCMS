using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALUserGroup
    {
        public int SaveRecord(SECURITY_UserGroup pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SECURITY_UserGroup lRow_ExistingData = dbSCMS.SECURITY_UserGroups.Where(c => c.UsrGrp_Id.Equals(pRow_NewData.UsrGrp_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.UsrGrp_Title = pRow_NewData.UsrGrp_Title;
                }
                else
                {
                    dbSCMS.SECURITY_UserGroups.InsertOnSubmit(pRow_NewData);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(pRow_NewData.UsrGrp_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public int DeleteRecordById(String ps_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SECURITY_UserGroup where UsrGrp_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        //public List<SECURITY_UserGroup> GetAllRecords() 
        //{
        //    try
        //    {
        //        SCMSDataContext dbSCMS = Connection.Create();
        //        return dbSCMS.SECURITY_UserGroups.Where(c => c.UsrGrp_Active == 1).OrderBy(c => c.UsrGrp_Code).ToList();
        //    }
        //    catch(Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public List<sp_GetUserGroupListResult> GetAllData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_GetUserGroupList().ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<sp_PopulateUserGroupListResult> PopulateData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_PopulateUserGroupList().ToList();
            }
            catch
            {
                return null;
            }
        }

    }
}
