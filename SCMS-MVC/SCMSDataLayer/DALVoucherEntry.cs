using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALVoucherEntry
    {
        public int SaveVoucherMaster(GL_VchrMaster newVoucherMaster)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                GL_VchrMaster existingVoucherMaster = dbSCMS.GL_VchrMasters.Where(c => c.VchMas_Id.Equals(newVoucherMaster.VchMas_Id)).SingleOrDefault();
                if (existingVoucherMaster != null)
                {
                    existingVoucherMaster.VchMas_Code = newVoucherMaster.VchMas_Code;
                    existingVoucherMaster.VchMas_Date = newVoucherMaster.VchMas_Date;
                    existingVoucherMaster.VchMas_Status = newVoucherMaster.VchMas_Status;
                    existingVoucherMaster.VchrType_Id = newVoucherMaster.VchrType_Id;
                    existingVoucherMaster.VchMas_Remarks = newVoucherMaster.VchMas_Remarks;
                    existingVoucherMaster.Loc_Id = newVoucherMaster.Loc_Id;
                    existingVoucherMaster.SyncStatus = false;
                }
                else
                {
                    dbSCMS.GL_VchrMasters.InsertOnSubmit(newVoucherMaster);
                }
                dbSCMS.SubmitChanges();
                return Convert.ToInt32(newVoucherMaster.VchMas_Id);
            }
            catch
            {
                return 0;
            }
        }

        public int SaveVoucherDetail(GL_VchrDetail newVoucherDetail)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                GL_VchrDetail existingVoucherDetail = dbSCMS.GL_VchrDetails.Where(c => c.VchDet_Id.Equals(newVoucherDetail.VchDet_Id)).SingleOrDefault();
                if (existingVoucherDetail != null)
                {
                    existingVoucherDetail.VchMas_DrAmount = newVoucherDetail.VchMas_DrAmount;
                    existingVoucherDetail.VchMas_CrAmount = newVoucherDetail.VchMas_CrAmount;
                    existingVoucherDetail.VchDet_Remarks = newVoucherDetail.VchDet_Remarks;
                    existingVoucherDetail.ChrtAcc_Id = newVoucherDetail.ChrtAcc_Id;
                }
                else
                {
                    dbSCMS.GL_VchrDetails.InsertOnSubmit(newVoucherDetail);
                }
                dbSCMS.SubmitChanges();
                return Convert.ToInt32(newVoucherDetail.VchDet_Id);
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
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From GL_VchrDetail where VchMas_Id='" + GLMaster_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public List<GL_VchrDetail> GetAllDetailRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.GL_VchrDetails.OrderBy(c => c.VchDet_Id).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<GL_VchrMaster> GetAllMasterRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                //return dbSCMS.GL_VchrMasters.Where(c => c.VchMas_Status == "Approved").OrderBy(c => c.VchMas_Id ).ToList();
                return dbSCMS.GL_VchrMasters.OrderBy(c => c.VchMas_Id).ToList();
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

                //li_Result = dbSCMS.ExecuteCommand("Delete From GL_VchrDetail Where VchMas_Id='" + ps_Id + "'");

                //li_Result = dbSCMS.ExecuteCommand("Delete From GL_VchrMaster Where VchMas_Id='" + ps_Id + "'");
                li_Result = dbSCMS.ExecuteCommand("Update GL_VchrMaster Set VchMas_Status='Cancelled' Where VchMas_Id='" + ps_Id + "'");
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// This function is used to retreive data in voucher entry console
        /// </summary>
        /// <param name="ps_AllLocation"></param>
        /// <param name="ps_Location"></param>
        /// <param name="ps_AllVoucherType"></param>
        /// <param name="ps_VoucherType"></param>
        /// <param name="ps_AllDate"></param>
        /// <param name="ps_DateFrom"></param>
        /// <param name="ps_DateTo"></param>
        /// <param name="ps_IncludeSecurity"></param>
        /// <returns></returns>
        public List<sp_VoucherEntryConsoleResult> GetVoucherEntryConsoleData(int ps_AllLocation, string ps_Location, int ps_AllVoucherType, string ps_VoucherType,
                                                                             int ps_AllDate, string ps_DateFrom, string ps_DateTo, bool ps_IncludeSecurity)
        {
            try
            {
                SECURITY_User user = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
                string UserLoginId = user.User_Id;
                string UserGroupId = user.UsrGrp_Id;
                string _Status = "Pending";

                List<sp_GetUserVoucherTypesByUserIdResult> UserVoucherTypes = new DALUserMenuRights().GetUserVoucherTypesByUserId(UserLoginId).ToList();
                if (UserVoucherTypes != null && UserVoucherTypes.Count > 0)
                {
                    UserVoucherTypes = UserVoucherTypes.Where(c => c.SelectedVoucherType != "0").ToList();
                }
                string[] VoucherTypeIds = UserVoucherTypes.Select(c => c.VchrType_Id).ToArray();

                List<sp_GetUserLocationsByUserIdResult> UserLocations = new DALUserMenuRights().GetUserLocationsByUserId(UserLoginId).ToList();
                if (UserLocations != null && UserLocations.Count > 0)
                {
                    UserLocations = UserLocations.Where(c => c.SelectedLocation != "0").ToList();
                }
                string[] LocationsIds = UserLocations.Select(c => c.Loc_Id).ToArray();

                SCMSDataContext dbSCMS = Connection.Create();
                if (ps_IncludeSecurity == true)
                {
                    //return dbSCMS.sp_VoucherEntryConsole(ps_AllLocation, ps_Location, ps_AllVoucherType, ps_VoucherType, ps_AllDate, ps_DateFrom, ps_DateTo).ToList().Where(c => VoucherTypeIds.Contains(c.VchrType_Id) && LocationsIds.Contains(c.Loc_Id) && _Status.Contains (c.VchMas_Status) ).ToList();
                    return dbSCMS.sp_VoucherEntryConsole(ps_AllLocation, ps_Location, ps_AllVoucherType, ps_VoucherType, ps_AllDate, ps_DateFrom, ps_DateTo).ToList().Where(c => VoucherTypeIds.Contains(c.VchrType_Id) && LocationsIds.Contains(c.Loc_Id) ).ToList();
                }
                else
                {
                    return dbSCMS.sp_VoucherEntryConsole(ps_AllLocation, ps_Location, ps_AllVoucherType, ps_VoucherType, ps_AllDate, ps_DateFrom, ps_DateTo).ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        public List<GL_VchrMaster> GetLastRecordByVchrType(string ps_LocationId, string ps_VoucherType)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.GL_VchrMasters.Where(c => c.Loc_Id == ps_LocationId && c.VchrType_Id == ps_VoucherType).ToList();
            }
            catch
            {
                return null;
            }
        }

    }
}
