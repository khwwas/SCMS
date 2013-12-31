using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALReligion
    {
        public int Save(SETUP_Religion newSetupReligion)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_Religion existingGender = dbSCMS.SETUP_Religions.Where(c => c.Rlgn_Id.Equals(newSetupReligion.Rlgn_Id)).SingleOrDefault();
                if (existingGender != null)
                {
                    existingGender.Rlgn_Title = newSetupReligion.Rlgn_Title;
                    existingGender.Rlgn_Abbreviation = newSetupReligion.Rlgn_Abbreviation;
                    existingGender.Rlgn_Active = newSetupReligion.Rlgn_Active;
                    existingGender.Rlgn_SortOrder = newSetupReligion.Rlgn_SortOrder;
                }
                else
                {
                    dbSCMS.SETUP_Religions.InsertOnSubmit(newSetupReligion);
                }
                dbSCMS.SubmitChanges();
                return Convert.ToInt32(newSetupReligion.Rlgn_Id);
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
                int result = dbSCMS.ExecuteCommand("Delete From SETUP_Religion where Rlgn_Id='" + Id + "'");
                return 1;
            }
            catch
            {
                return 0;
            }
        }


        public List<SETUP_Religion> GetAllData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Religions.Where(c => c.Rlgn_Active == 1).OrderBy(c => c.Rlgn_Id).ToList();
            }
            catch
            {
                return null;
            }
        }

    }
}
