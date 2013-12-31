using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
   public class DALGender
    {
       public int Save(SETUP_Gender newSetupGender)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_Gender existingGender = dbSCMS.SETUP_Genders.Where(c => c.Gndr_Id.Equals(newSetupGender.Gndr_Id)).SingleOrDefault();
                if (existingGender != null)
                {
                    existingGender.Gndr_Title = newSetupGender.Gndr_Title;
                    existingGender.Gndr_Abbreviation = newSetupGender.Gndr_Abbreviation;
                    existingGender.Gndr_Active = newSetupGender.Gndr_Active;
                    existingGender.Gndr_SortOrder = newSetupGender.Gndr_SortOrder;
                }
                else
                {
                    dbSCMS.SETUP_Genders.InsertOnSubmit(newSetupGender);
                }
                dbSCMS.SubmitChanges();
                return Convert.ToInt32(newSetupGender.Gndr_Id);
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
                int result = dbSCMS.ExecuteCommand("Delete From SETUP_Gender where Gndr_Id='" + Id + "'");
                return 1;
            }
            catch
            {
                return 0;
            }
        }


        public List<SETUP_Gender> GetAllData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Genders.Where(c => c.Gndr_Active == 1).OrderBy(c => c.Gndr_Id).ToList();
            }
            catch
            {
                return null;
            }
        }


    }
}
