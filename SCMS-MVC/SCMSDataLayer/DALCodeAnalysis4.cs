using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALCodeAnalysis4
    {
        public int SaveRecord(SETUP_CodeAnalysis4 lrow_CodeAnalysis4)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();

                SETUP_CodeAnalysis4 lRow_ExistingData = dbSCMS.SETUP_CodeAnalysis4s.Where(c => c.CA_Id.Equals(lrow_CodeAnalysis4.CA_Id)).SingleOrDefault();
                    if (lRow_ExistingData != null)
                    {
                        lRow_ExistingData.CA_Title = lrow_CodeAnalysis4.CA_Title;
                        lRow_ExistingData.Loc_Id = lrow_CodeAnalysis4.Loc_Id;
                    }
                    else
                    {
                        dbSCMS.SETUP_CodeAnalysis4s.InsertOnSubmit(lrow_CodeAnalysis4);
                    }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(lrow_CodeAnalysis4.CA_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

       public List<SETUP_CodeAnalysis4> GetAllRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_CodeAnalysis4s.Where(c => c.CA_Active == 1).OrderBy(c => c.CA_Code).ToList();
            }
            catch
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


        public int DeleteRecordById(String ps_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SETUP_CodeAnalysis4 where CA_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

    }
}
