using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
   public class DALBank
    {
       public int SaveRecord(SETUP_Bank lrow_Bank)
       {
           int li_ReturnValue = 0;

           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               SETUP_Bank lRow_ExistingData = dbSCMS.SETUP_Banks.Where(c => c.Bank_Id.Equals(lrow_Bank.Bank_Id)).SingleOrDefault();

               if (lRow_ExistingData != null)
               {
                   lRow_ExistingData.Bank_Title = lrow_Bank.Bank_Title;
                   //lRow_ExistingData.Loc_Id = lrow_Bank.Loc_Id;
               }
               else
               {
                   dbSCMS.SETUP_Banks.InsertOnSubmit(lrow_Bank);
               }
               dbSCMS.SubmitChanges();

               li_ReturnValue = Convert.ToInt32(lrow_Bank.Bank_Id);
           }
           catch
           {
               return 0;
           }

           return li_ReturnValue;
       }

       public List<SETUP_Bank> GetAllRecords()
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               return dbSCMS.SETUP_Banks.Where(c => c.Bank_Active == 1).OrderBy(c => c.Bank_Code).ToList();
           }
           catch (Exception ex)
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
               li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Setup_Bank where Bank_Id='" + ps_Id + "'");
           }
           catch
           {
               li_ReturnValue = 0;
           }

           return li_ReturnValue;
       }

       public List<SETUP_Bank> PopulateData()
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               return dbSCMS.SETUP_Banks.Where(c => c.Bank_Active== 1).ToList();
           }
           catch
           {
               return null;
           }
       }
    }
}
