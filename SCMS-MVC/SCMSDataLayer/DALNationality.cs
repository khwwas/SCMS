using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
   public class DALNationality
   {
       public int Save(SETUP_Nationality newSetupNationality)
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               SETUP_Nationality existingNationality = dbSCMS.SETUP_Nationalities.Where(c => c.Natn_Id.Equals(newSetupNationality.Natn_Id)).SingleOrDefault();
               if (existingNationality != null)
               {
                   existingNationality.Natn_Title = newSetupNationality.Natn_Title;
                   existingNationality.Natn_Abbreviation = newSetupNationality.Natn_Abbreviation;
                   existingNationality.Natn_Active = newSetupNationality.Natn_Active;
                   existingNationality.Natn_SortOrder = newSetupNationality.Natn_SortOrder;
               }
               else
               {
                   dbSCMS.SETUP_Nationalities.InsertOnSubmit(newSetupNationality);
               }
               dbSCMS.SubmitChanges();
               return Convert.ToInt32(newSetupNationality.Natn_Id);
           }
           catch
           {
               return 0;
           }
       }

       public int DeleteById(string Id)
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               int result = dbSCMS.ExecuteCommand("Delete From SETUP_Nationality where Natn_Id='" + Id + "'");
               return 1;
           }
           catch
           {
               return 0;
           }
       }


       public List<SETUP_Nationality> GetAllData()
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               return dbSCMS.SETUP_Nationalities.Where(c => c.Natn_Active == 1).OrderBy(c => c.Natn_Id).ToList();
           }
           catch (Exception ex)
           {
               return null;
           }
       }

   }
}
