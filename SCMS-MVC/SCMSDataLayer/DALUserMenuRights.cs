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

        public List<sp_GetUserMenuRightsByUserIdResult> GetUserMenuRightsByUserId(string UserId)
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.sp_GetUserMenuRightsByUserId(UserId).ToList();
        }

        public List<sp_GetUserLocationsByUserIdResult> GetUserLocationsByUserId(string UserId)
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.sp_GetUserLocationsByUserId(UserId).ToList();
        }

        public List<sp_GetUserLocationsByGroupIdResult> GetUserLocationsByGroupId(string GourpId)
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.sp_GetUserLocationsByGroupId(GourpId).ToList();
        }

        public List<sp_GetUserVoucherTypesByGroupIdResult> GetUserVoucherTypesByGroupId(string GourpId)
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.sp_GetUserVoucherTypesByGroupId(GourpId).ToList();
        }

        public List<sp_GetUserVoucherTypesByUserIdResult> GetUserVoucherTypesByUserId(string UserId)
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.sp_GetUserVoucherTypesByUserId(UserId).ToList();
        }

        public List<sp_GetUserChartOfAccountByGroupIdResult> GetUserChartOfAccountByGroupId(string GourpId)
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.sp_GetUserChartOfAccountByGroupId(GourpId).ToList();
        }

        public List<sp_GetUserChartOfAccountByUserIdResult> GetUserChartOfAccountByUserId(string UserId)
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.sp_GetUserChartOfAccountByUserId(UserId).ToList();
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

        public int SetUserLocations(Security_UserLocation pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                dbSCMS.Security_UserLocations.InsertOnSubmit(pRow_NewData);
                dbSCMS.SubmitChanges();
                li_ReturnValue = Convert.ToInt32(pRow_NewData.UserLoc_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public int SetUserVoucherTypes(Security_UserVoucherType pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                dbSCMS.Security_UserVoucherTypes.InsertOnSubmit(pRow_NewData);
                dbSCMS.SubmitChanges();
                li_ReturnValue = Convert.ToInt32(pRow_NewData.UserVchrTyp_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public int SetUserChartOfAccount(Security_UserChartOfAccount pRow_NewData)
        {
            int li_ReturnValue = 0;
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                dbSCMS.Security_UserChartOfAccounts.InsertOnSubmit(pRow_NewData);
                dbSCMS.SubmitChanges();
                li_ReturnValue = Convert.ToInt32(pRow_NewData.UserCOA_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public List<Security_MenuOption> MenuOptions()
        {
            SCMSDataContext dbSCMS = Connection.Create();
            return dbSCMS.Security_MenuOptions.ToList();
        }

        public int DeleteRecordByGroupId(int Grp_Id, int? UserId, int ModuleId)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                if (UserId != null)
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserRights where Grp_Id=" + Grp_Id + " And UsrSec_UserId=" + UserId + " And Mod_Id=" + ModuleId);
                }
                else
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserRights where Grp_Id=" + Grp_Id + " And Mod_Id=" + ModuleId);
                }
            }
            catch
            {
                li_ReturnValue = -1;
            }

            return li_ReturnValue;
        }

        public int DeleteLocationsByGroupId(string Grp_Id, string UserId)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                if (UserId != null)
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserLocations where UsrGrp_Id='" + Grp_Id + "' And User_Id='" + UserId + "'");
                }
                else
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserLocations where UsrGrp_Id='" + Grp_Id + "'");
                }
            }
            catch
            {
                li_ReturnValue = -1;
            }

            return li_ReturnValue;
        }

        public int DeleteVoucherTypesByGroupId(string Grp_Id, string UserId)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                if (UserId != null)
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserVoucherTypes where UserGrp_Id='" + Grp_Id + "' And User_Id='" + UserId + "'");
                }
                else
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserVoucherTypes where UserGrp_Id='" + Grp_Id + "'");
                }
            }
            catch
            {
                li_ReturnValue = -1;
            }

            return li_ReturnValue;
        }

        public int DeleteChartOfAccountsByGroupId(string Grp_Id, string UserId)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                if (UserId != null)
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserChartOfAccount where UserGrp_Id='" + Grp_Id + "' And User_Id='" + UserId + "'");
                }
                else
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Security_UserChartOfAccount where UserGrp_Id='" + Grp_Id + "'");
                }
            }
            catch
            {
                li_ReturnValue = -1;
            }

            return li_ReturnValue;
        }

        public int CopyUserRights(string CurrentUserId, string NewUserId, string SelectedTab)
        {
            try
            {
                string GroupId = "";
                GroupId = new DALUser().GetAllData().Where(c => c.User_Id.Equals(NewUserId)).SingleOrDefault().UsrGrp_Id;

                SCMSDataContext dbSCMS = Connection.Create();
                if (SelectedTab == "Menus")
                {
                    List<Security_UserRight> currentUserMenuRights = dbSCMS.Security_UserRights.Where(c => c.UsrSec_UserId.Equals(Convert.ToInt32(CurrentUserId))).ToList();

                    List<Security_UserRight> newUserMenuRights = dbSCMS.Security_UserRights.Where(c => c.UsrSec_UserId.Equals(Convert.ToInt32(NewUserId))).ToList();

                    if (newUserMenuRights != null && newUserMenuRights.Count > 0)
                    {
                        dbSCMS.Security_UserRights.DeleteAllOnSubmit(newUserMenuRights);
                        dbSCMS.SubmitChanges();
                    }

                    if (currentUserMenuRights != null && currentUserMenuRights.Count > 0)
                    {
                        foreach (Security_UserRight userRight in currentUserMenuRights)
                        {
                            Security_UserRight newUserRight = new Security_UserRight();

                            newUserRight.Grp_Id = Convert.ToInt32(GroupId); //userRight.Grp_Id;
                            newUserRight.Mnu_Id = userRight.Mnu_Id;
                            newUserRight.Mod_Id = userRight.Mod_Id;
                            newUserRight.UsrSec_UserId = Convert.ToInt32(NewUserId).ToString();
                            newUserRight.UsrSec_Add = userRight.UsrSec_Add;
                            newUserRight.UsrSec_Edit = userRight.UsrSec_Edit;
                            newUserRight.UsrSec_Delete = userRight.UsrSec_Delete;
                            newUserRight.UsrSec_Print = userRight.UsrSec_Print;
                            newUserRight.UsrSec_Import = userRight.UsrSec_Import;

                            SaveRecord(newUserRight);
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (SelectedTab == "Locations")
                {

                    List<Security_UserLocation> currentUserLocations = dbSCMS.Security_UserLocations.Where(c => c.User_Id.Equals(CurrentUserId)).ToList();

                    List<Security_UserLocation> newUserLocations = dbSCMS.Security_UserLocations.Where(c => c.User_Id.Equals(NewUserId)).ToList();

                    if (newUserLocations != null && newUserLocations.Count > 0)
                    {
                        dbSCMS.Security_UserLocations.DeleteAllOnSubmit(newUserLocations);
                        dbSCMS.SubmitChanges();
                    }

                    if (currentUserLocations != null && currentUserLocations.Count > 0)
                    {
                        foreach (Security_UserLocation userLocation in currentUserLocations)
                        {
                            Security_UserLocation newUserLocation = new Security_UserLocation();

                            newUserLocation.UsrGrp_Id = GroupId;
                            newUserLocation.User_Id = NewUserId;
                            newUserLocation.Loc_Id = userLocation.Loc_Id;

                            SetUserLocations(newUserLocation);
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (SelectedTab == "VoucherTypes")
                {
                    List<Security_UserVoucherType> currentVoucherTypes = dbSCMS.Security_UserVoucherTypes.Where(c => c.User_Id.Equals(CurrentUserId)).ToList();

                    List<Security_UserVoucherType> newVouchertTypes = dbSCMS.Security_UserVoucherTypes.Where(c => c.User_Id.Equals(NewUserId)).ToList();

                    if (newVouchertTypes != null && newVouchertTypes.Count > 0)
                    {
                        dbSCMS.Security_UserVoucherTypes.DeleteAllOnSubmit(newVouchertTypes);
                        dbSCMS.SubmitChanges();
                    }

                    if (currentVoucherTypes != null && currentVoucherTypes.Count > 0)
                    {
                        foreach (Security_UserVoucherType userVoucherType in currentVoucherTypes)
                        {
                            Security_UserVoucherType newUserVoucherType = new Security_UserVoucherType();

                            newUserVoucherType.UserGrp_Id = GroupId;
                            newUserVoucherType.User_Id = NewUserId;
                            newUserVoucherType.VchrType_Id = userVoucherType.VchrType_Id;

                            SetUserVoucherTypes(newUserVoucherType);
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (SelectedTab == "ChartOfAccounts")
                {
                    List<Security_UserChartOfAccount> currentChartOfAccounts = dbSCMS.Security_UserChartOfAccounts.Where(c => c.User_Id.Equals(CurrentUserId)).ToList();

                    List<Security_UserChartOfAccount> newChartOfAccounts = dbSCMS.Security_UserChartOfAccounts.Where(c => c.User_Id.Equals(NewUserId)).ToList();

                    if (newChartOfAccounts != null && newChartOfAccounts.Count > 0)
                    {
                        dbSCMS.Security_UserChartOfAccounts.DeleteAllOnSubmit(newChartOfAccounts);
                        dbSCMS.SubmitChanges();
                    }

                    if (currentChartOfAccounts != null && currentChartOfAccounts.Count > 0)
                    {
                        foreach (Security_UserChartOfAccount userChartOfAccount in currentChartOfAccounts)
                        {
                            Security_UserChartOfAccount newUserChartOfAccount = new Security_UserChartOfAccount();

                            newUserChartOfAccount.UserGrp_Id = GroupId;
                            newUserChartOfAccount.User_Id = NewUserId;
                            newUserChartOfAccount.ChrtAcc_Id = userChartOfAccount.ChrtAcc_Id;

                            SetUserChartOfAccount(newUserChartOfAccount);
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }

                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
