using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALApprovalLevel
    {
        public int SaveRecord(SYSTEM_ApprovalLevel pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SYSTEM_ApprovalLevel lRow_ExistingData = dbSCMS.SYSTEM_ApprovalLevels.Where(c => c.AprvLvl_Id.Equals(pRow_NewData.AprvLvl_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.AprvLvl_Level = pRow_NewData.AprvLvl_Level;
                    lRow_ExistingData.AprvLvl_Title = pRow_NewData.AprvLvl_Title;
                }
                else
                {
                    dbSCMS.SYSTEM_ApprovalLevels.InsertOnSubmit(pRow_NewData);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(pRow_NewData.AprvLvl_Id);
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
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SYSTEM_ApprovalLevel Where AprvLvl_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        //public List<SYSTEM_ApprovalLevel> GetAllRecords()
        //{
        //    try
        //    {
        //        //SCMSDataContext dbSCMS = Connection.Create();

        //        ////dbSCMS.SYSTEM_ApprovalLevels.Where(



        //        //return dbSCMS.SYSTEM_ApprovalLevels.Where(c => c.AprvLvl_Active == 1).OrderBy(c => c.AprvLvl_Code).ToList();
        //        return null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// Use This method to get all locations without Security
        /// </summary>
        /// <returns></returns>
        public List<sp_GetApprovalLevelListResult> GetAllData()
        {
            try
            {
                //string UserLoginId = DALCommon.UserLoginId;
                //List<sp_GetUserLocationsByUserIdResult> UserLocations = new DALUserMenuRights().GetUserLocationsByUserId(UserLoginId).ToList();
                //if (UserLocations != null && UserLocations.Count > 0)
                //{
                //    UserLocations = UserLocations.Where(c => c.SelectedLocation != "0").ToList();
                //}
                //string[] LocationsIds = UserLocations.Select(c => c.AprvLvl_Id).ToArray();
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_GetApprovalLevelList().ToList();//.Where(c => LocationsIds.Contains(c.AprvLvl_Id)).ToList();
            }
            catch
            {
                return null;
            }
        }

        //// Use for the drop downs to implement Security
        //public List<sp_PopulateLocationListResult> PopulateData()
        //{
        //    try
        //    {
        //        SECURITY_User user = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
        //        string UserLoginId = user.User_Id;
        //        string UserGroupId = user.UsrGrp_Id;

        //        List<sp_GetUserLocationsByUserIdResult> UserLocations = new DALUserMenuRights().GetUserLocationsByUserId(UserLoginId).ToList();
        //        if (UserLocations != null && UserLocations.Count > 0)
        //        {
        //            UserLocations = UserLocations.Where(c => c.SelectedLocation != "0").ToList();
        //        }
        //        string[] LocationsIds = UserLocations.Select(c => c.AprvLvl_Id).ToArray();
        //        SCMSDataContext dbSCMS = Connection.Create();
        //        return dbSCMS.sp_PopulateLocationList().ToList().Where(c => LocationsIds.Contains(c.AprvLvl_Id)).ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public List<SYSTEM_ApprovalLevel> GetAllApprovalLevel() 
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SYSTEM_ApprovalLevels.ToList(); 
            }
            catch
            {
                return null;
            }
        }
    }
}
