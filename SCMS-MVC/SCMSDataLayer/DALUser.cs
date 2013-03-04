using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALUser
    {
        public int SaveRecord(SECURITY_User pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SECURITY_User lRow_ExistingData = dbSCMS.SECURITY_Users.Where(c => c.User_Id.Equals(pRow_NewData.User_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.User_Title = pRow_NewData.User_Title;
                    lRow_ExistingData.UsrGrp_Id = pRow_NewData.UsrGrp_Id;
                    lRow_ExistingData.User_Password = pRow_NewData.User_Password;
                }
                else
                {
                    dbSCMS.SECURITY_Users.InsertOnSubmit(pRow_NewData);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(pRow_NewData.User_Id);
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
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SECURITY_User where User_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        //public List<SECURITY_User> GetAllRecords() 
        //{
        //    string _Sql = "";

        //    try
        //    {
        //        SCMSDataContext dbSCMS = Connection.Create();


        //        return dbSCMS.SECURITY_Users.Where(c => c.User_Active == 1).OrderBy(c => c.User_Code).ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        public List<sp_GetUserListResult> GetAllData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_GetUserList().ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<sp_PopulateLocationListResult> PopulateData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_PopulateLocationList().ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
