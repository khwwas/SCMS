using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALBudgetEntry
    {
        public int SaveBudgetMaster(GL_BgdtMaster newBudgetMaster)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                GL_BgdtMaster existingBudgetMaster = dbSCMS.GL_BgdtMasters.Where(c => c.BgdtMas_Id.Equals(newBudgetMaster.BgdtMas_Id)).SingleOrDefault();
                if (existingBudgetMaster != null)
                {
                    //existingBudgetMaster.BgdtMas_Code = newBudgetMaster.BgdtMas_Code;
                    existingBudgetMaster.BgdtMas_Date = newBudgetMaster.BgdtMas_Date;
                    existingBudgetMaster.BgdtMas_Status = newBudgetMaster.BgdtMas_Status;
                    existingBudgetMaster.BgdtType_Id = newBudgetMaster.BgdtType_Id;
                    existingBudgetMaster.Cldr_Id = newBudgetMaster.Cldr_Id;
                    existingBudgetMaster.Loc_Id = newBudgetMaster.Loc_Id;
                    existingBudgetMaster.BgdtMas_Remarks = newBudgetMaster.BgdtMas_Remarks;
                }
                else
                {
                    dbSCMS.GL_BgdtMasters.InsertOnSubmit(newBudgetMaster);
                }
                dbSCMS.SubmitChanges();
                return 1; // Convert.ToInt32(newBudgetMaster.BgdtMas_Id);
            }
            catch
            {
                return 0;
            }
        }

        public int SaveBudgetDetail(GL_BgdtDetail newBudgetDetail)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                GL_BgdtDetail existingBudgetDetail = dbSCMS.GL_BgdtDetails.Where(c => c.BgdtDet_Id.Equals(newBudgetDetail.BgdtDet_Id)).SingleOrDefault();
                if (existingBudgetDetail != null)
                {
                    existingBudgetDetail.BgdtDet_Month1 = newBudgetDetail.BgdtDet_Month1;
                    existingBudgetDetail.BgdtDet_Month2 = newBudgetDetail.BgdtDet_Month2;
                    existingBudgetDetail.BgdtDet_Month3 = newBudgetDetail.BgdtDet_Month3;
                    existingBudgetDetail.BgdtDet_Month4 = newBudgetDetail.BgdtDet_Month4;
                    existingBudgetDetail.BgdtDet_Month5 = newBudgetDetail.BgdtDet_Month5;
                    existingBudgetDetail.BgdtDet_Month6 = newBudgetDetail.BgdtDet_Month6;
                    existingBudgetDetail.BgdtDet_Month7 = newBudgetDetail.BgdtDet_Month7;
                    existingBudgetDetail.BgdtDet_Month8 = newBudgetDetail.BgdtDet_Month8;
                    existingBudgetDetail.BgdtDet_Month9 = newBudgetDetail.BgdtDet_Month9;
                    existingBudgetDetail.BgdtDet_Month10 = newBudgetDetail.BgdtDet_Month10;
                    existingBudgetDetail.BgdtDet_Month11 = newBudgetDetail.BgdtDet_Month11;
                    existingBudgetDetail.BgdtDet_Month12 = newBudgetDetail.BgdtDet_Month12;
                    existingBudgetDetail.BgdtDet_TotalAmount = newBudgetDetail.BgdtDet_TotalAmount;
                    existingBudgetDetail.BgdtDet_Remarks = newBudgetDetail.BgdtDet_Remarks;
                    existingBudgetDetail.ChrtAcc_Id = newBudgetDetail.ChrtAcc_Id;
                }
                else
                {
                    dbSCMS.GL_BgdtDetails.InsertOnSubmit(newBudgetDetail);
                }
                dbSCMS.SubmitChanges();
                return Convert.ToInt32(newBudgetDetail.BgdtDet_Id);
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteDetailRecordByMasterId(String GLMaster_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From GL_BgdtDetail where BgdtMas_Id='" + GLMaster_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public List<GL_BgdtDetail> GetAllDetailRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.GL_BgdtDetails.OrderBy(c => c.BgdtDet_Id).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<GL_BgdtMaster> GetAllMasterRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                //return dbSCMS.GL_BgdtMasters.Where(c => c.BgdtMas_Status == "Approved").OrderBy(c => c.BgdtMas_Id ).ToList();
                return dbSCMS.GL_BgdtMasters.OrderBy(c => c.BgdtMas_Id).ToList();
            }
            catch
            {
                return null;
            }
        }

        public int DeleteRecordById(String ps_Id)
        {
            int li_Result = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();

                li_Result = dbSCMS.ExecuteCommand("Update GL_BgdtMaster Set BgdtMas_Status='Cancelled' Where BgdtMas_Id='" + ps_Id + "'");
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// This function is used to retreive data in Budget entry console
        /// </summary>
        /// <param name="ps_AllLocation"></param>
        /// <param name="ps_Location"></param>
        /// <param name="ps_AllBudgetType"></param>
        /// <param name="ps_BudgetType"></param>
        /// <param name="ps_AllDate"></param>
        /// <param name="ps_DateFrom"></param>
        /// <param name="ps_DateTo"></param>
        /// <param name="ps_IncludeSecurity"></param>
        /// <returns></returns>
        public List<sp_BudgetConsoleResult> GetBudgetEntryConsoleData(int ps_AllLocation, string ps_Location, bool ps_IncludeSecurity)
        {
            try
            {
                //int ps_AllBudgetType, string ps_BudgetType, int ps_AllDate, string ps_DateFrom, string ps_DateTo,

                SECURITY_User user = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
                string UserLoginId = user.User_Id;
                string UserGroupId = user.UsrGrp_Id;
                string _Status = "Pending";

                //List<sp_GetUserBudgetTypesByUserIdResult> UserBudgetTypes = new DALUserMenuRights().GetUserBudgetTypesByUserId(UserLoginId).ToList();
                //if (UserBudgetTypes != null && UserBudgetTypes.Count > 0)
                //{
                //    UserBudgetTypes = UserBudgetTypes.Where(c => c.SelectedBudgetType != "0").ToList();
                //}
                //string[] BudgetTypeIds = UserBudgetTypes.Select(c => c.BgdtType_Id).ToArray();

                List<sp_GetUserLocationsByUserIdResult> UserLocations = new DALUserMenuRights().GetUserLocationsByUserId(UserLoginId).ToList();
                if (UserLocations != null && UserLocations.Count > 0)
                {
                    UserLocations = UserLocations.Where(c => c.SelectedLocation != "0").ToList();
                }
                string[] LocationsIds = UserLocations.Select(c => c.Loc_Id).ToArray();

                SCMSDataContext dbSCMS = Connection.Create();
                if (ps_IncludeSecurity == true)
                {
                    return dbSCMS.sp_BudgetConsole(ps_AllLocation, ps_Location).ToList().Where(c => LocationsIds.Contains(c.Loc_Id)).ToList();
                    //return dbSCMS.sp_BudgetConsole(ps_AllLocation, ps_Location).ToList().Where(c => LocationsIds.Contains(c.Loc_Id) && _Status.Contains(c.BgdtMas_Status)).ToList();
                }
                else
                {
                    return dbSCMS.sp_BudgetConsole(ps_AllLocation, ps_Location).ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        public List<GL_BgdtMaster> GetLastRecordByVchrType(string ps_LocationId, string ps_BudgetType)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.GL_BgdtMasters.Where(c => c.Loc_Id == ps_LocationId && c.BgdtType_Id == ps_BudgetType).ToList();
            }
            catch
            {
                return null;
            }
        }

    }
}
