using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
   public class DALBankAccount
    {
       public int SaveRecord(SETUP_BankAccount lrow_BankAccount)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_BankAccount lRow_ExistingData = dbSCMS.SETUP_BankAccounts.Where(c => c.BankAcc_Id.Equals(lrow_BankAccount.BankAcc_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.BankAcc_Title = lrow_BankAccount.BankAcc_Title;
                    lRow_ExistingData.Loc_Id = lrow_BankAccount.Loc_Id;
                    lRow_ExistingData.Cmp_Id = lrow_BankAccount.Cmp_Id;
                    lRow_ExistingData.Bank_Id = lrow_BankAccount.Bank_Id;
                }
                else
                {
                    dbSCMS.SETUP_BankAccounts.InsertOnSubmit(lrow_BankAccount);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(lrow_BankAccount.BankAcc_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_BankAccount> GetAllRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_BankAccounts.Where(c => c.BankAcc_Active == 1).OrderBy(c => c.BankAcc_Code).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SETUP_Location> GetAllLocation()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Locations.Where(c => c.Loc_Active == 1).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<SETUP_Company> GetAllCompanies()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Companies.Where(c => c.Cmp_Active == 1).ToList();
            }
            catch
            {
                return null;
            }
        }

        public int DeleteRecordById(String ps_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Setup_BankAccount where BankAcc_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_BankAccount> PopulateData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_BankAccounts.Where(c => c.BankAcc_Active == 1).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
