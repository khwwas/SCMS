using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALBankReconciliation
    {
        public int SaveRecord(GL_VchrMaster lrow_VchrMaster)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                GL_VchrMaster lRow_ExistingData = dbSCMS.GL_VchrMasters.Where(c => c.VchMas_Id.Equals(lrow_VchrMaster.VchMas_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.VchMas_Reconciliation = lrow_VchrMaster.VchMas_Reconciliation;
                    lRow_ExistingData.VchMas_ReconciliationDate = lrow_VchrMaster.VchMas_ReconciliationDate;
                }
                else
                {
                    //dbSCMS.SETUP_BankAccounts.InsertOnSubmit(lrow_BankAccount);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(lrow_VchrMaster.VchMas_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public List<sp_BankReconciliationResult> GetBankReconciliationData(int ps_AllLocation, string ps_Location,
                                                                           int ps_AllDate, string ps_DateFrom, string ps_DateTo)
        {
            string UserLoginId = "";
            string[] LocationsIds;

            try
            {
                SECURITY_User _User = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
                UserLoginId = _User.User_Id;

                List<sp_GetUserLocationsByUserIdResult> UserLocations = new DALUserMenuRights().GetUserLocationsByUserId(UserLoginId).ToList();
                if (UserLocations != null && UserLocations.Count > 0)
                {
                    UserLocations = UserLocations.Where(c => c.SelectedLocation != "0").ToList();
                }
                LocationsIds = UserLocations.Select(c => c.Loc_Id).ToArray();

                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_BankReconciliation(ps_AllLocation, ps_Location, ps_AllDate, ps_DateFrom, ps_DateTo).ToList().Where(c => LocationsIds.Contains(c.Loc_Id)).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
