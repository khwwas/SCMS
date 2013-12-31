using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALVoucherTypeNarration
    {
        public int SaveRecord(SETUP_VoucherTypeNarration pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_VoucherTypeNarration lRow_ExistingData = dbSCMS.SETUP_VoucherTypeNarrations.Where(c => c.VchrTypeNarr_Id.Equals(pRow_NewData.VchrTypeNarr_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.VchrTypeNarr_Title = pRow_NewData.VchrTypeNarr_Title;
                }
                else
                {
                    dbSCMS.SETUP_VoucherTypeNarrations.InsertOnSubmit(pRow_NewData);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(pRow_NewData.VchrTypeNarr_Id);
            }
            catch( Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return li_ReturnValue;
        }

        public int DeleteRecordById(String ps_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SETUP_VoucherTypeNarration where VchrTypeNarr_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public List<sp_GetVoucherTypeNarrationListResult> GetAllData()
        {
            try
            {
                //List<sp_GetUserVoucherTypesByUserIdResult> UserVoucherTypes = new DALUserMenuRights().GetUserVoucherTypesByUserId(DALCommon.UserLoginId).ToList();
                //if (UserVoucherTypes != null && UserVoucherTypes.Count > 0)
                //{
                //    UserVoucherTypes = UserVoucherTypes.Where(c => c.SelectedVoucherType != "0").ToList();
                //}
                //string[] VoucherTypeIds = UserVoucherTypes.Select(c => c.VchrType_Id).ToArray();
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_GetVoucherTypeNarrationList().ToList();
            }
            catch
            {
                return null;
            }
        }

    }
}
