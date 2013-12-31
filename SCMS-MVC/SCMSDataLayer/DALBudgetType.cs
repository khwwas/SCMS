using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALBudgetType
    { 
        public int Save(SYSTEM_BudgetType newSetupBudgetType)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SYSTEM_BudgetType existingSetupVoucherType = dbSCMS.SYSTEM_BudgetTypes.Where(c => c.BgdtType_Id.Equals(newSetupBudgetType.BgdtType_Id)).SingleOrDefault();
                if (existingSetupVoucherType != null)
                {
                    existingSetupVoucherType.BgdtType_Title = newSetupBudgetType.BgdtType_Title;
                    existingSetupVoucherType.BgdtType_Prefix = newSetupBudgetType.BgdtType_Prefix;
                    existingSetupVoucherType.BgdtType_Active = newSetupBudgetType.BgdtType_Active;
                    existingSetupVoucherType.BgdtType_SortOrder = newSetupBudgetType.BgdtType_SortOrder;
                }
                else
                {
                    dbSCMS.SYSTEM_BudgetTypes.InsertOnSubmit(newSetupBudgetType);
                }
                dbSCMS.SubmitChanges();
                return Convert.ToInt32(newSetupBudgetType.BgdtType_Id);
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteById(String p_Id) 
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                int result = dbSCMS.ExecuteCommand("Delete From SYSTEM_BudgetType where BgdtType_Id='" + p_Id + "'");
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        ///// <summary>
        ///// Use GetAllData Method To Get Voucher Types With Security
        ///// </summary>
        ///// <returns></returns>
        //public List<sp_GetBudgetTypesListResult> GetAllData()
        //{
        //    try
        //    {
        //        //SECURITY_User user = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
        //        //string UserLoginId = user.User_Id;
        //        //string UserGroupId = user.UsrGrp_Id;

        //        List<sp_GetUserBudgetTypesByUserIdResult> UserVoucherTypes = new DALUserMenuRights().GetUserVoucherTypesByUserId(UserLoginId).ToList();
        //        //if (UserVoucherTypes != null && UserVoucherTypes.Count > 0)
        //        //{
        //        //    UserVoucherTypes = UserVoucherTypes.Where(c => c.SelectedVoucherType != "0").ToList();
        //        //}
        //        string[] VoucherTypeIds = UserBudgetTypes.Select(c => c.Bgdt).ToArray();
        //        SCMSDataContext dbSCMS = Connection.Create();
        //        return dbSCMS.sp_GetBudgetTypesList().ToList().Where(c => B .Contains(c.BgdtType_Id)).ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// Use PopulateData method to get all budget types without security to use on the budget type setup grid
        /// </summary>
        /// <returns></returns>
        public List<sp_PopulateBudgetTypeListResult> PopulateData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_PopulateBudgetTypeList().ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
