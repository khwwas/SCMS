using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALVoucherType
    {
        public int SaveVoucherType(SETUP_VoucherType newSetupVoucherType)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_VoucherType existingSetupVoucherType = dbSCMS.SETUP_VoucherTypes.Where(c => c.VchrType_Id.Equals(newSetupVoucherType.VchrType_Id)).SingleOrDefault();
                if (existingSetupVoucherType != null)
                {
                    existingSetupVoucherType.VchrType_Title = newSetupVoucherType.VchrType_Title;
                    existingSetupVoucherType.VchrType_Prefix = newSetupVoucherType.VchrType_Prefix;
                    existingSetupVoucherType.VchrType_Active = newSetupVoucherType.VchrType_Active;
                    existingSetupVoucherType.VchrType_CodeInitialization = newSetupVoucherType.VchrType_CodeInitialization;
                    existingSetupVoucherType.VchrType_SortOrder = newSetupVoucherType.VchrType_SortOrder;
                    //existingSetupVoucherType.Loc_Id = newSetupVoucherType.Loc_Id;
                    existingSetupVoucherType.Cmp_Id = newSetupVoucherType.Cmp_Id;

                }
                else
                {
                    dbSCMS.SETUP_VoucherTypes.InsertOnSubmit(newSetupVoucherType);
                }
                dbSCMS.SubmitChanges();
                return Convert.ToInt32(newSetupVoucherType.VchrType_Id);
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteVoucherTypeById(String VoucherTypeId)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                int result = dbSCMS.ExecuteCommand("Delete From SETUP_VoucherType where VchrType_Id='" + VoucherTypeId + "'");
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Use GetAllData Method To Get Voucher Types With Security
        /// </summary>
        /// <returns></returns>
        public List<sp_GetVoucherTypesListResult> GetAllData()
        {
            try
            {
                SECURITY_User user = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
                string UserLoginId = user.User_Id;
                string UserGroupId = user.UsrGrp_Id;

                List<sp_GetUserVoucherTypesByUserIdResult> UserVoucherTypes = new DALUserMenuRights().GetUserVoucherTypesByUserId(UserLoginId).ToList();
                if (UserVoucherTypes != null && UserVoucherTypes.Count > 0)
                {
                    UserVoucherTypes = UserVoucherTypes.Where(c => c.SelectedVoucherType != "0").ToList();
                }
                string[] VoucherTypeIds = UserVoucherTypes.Select(c => c.VchrType_Id).ToArray();
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_GetVoucherTypesList().ToList().Where(c => VoucherTypeIds.Contains(c.VchrType_Id)).ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Use PopulateData method to get all voucher types without security to use on the voucher type setup grid
        /// </summary>
        /// <returns></returns>
        public List<sp_PopulateVoucherTypeListResult> PopulateData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_PopulateVoucherTypeList().ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
