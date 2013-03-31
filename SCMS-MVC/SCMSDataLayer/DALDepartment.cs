using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALDepartment
    {
        public int SaveRecord(SETUP_Department lrow_Department)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_Department lRow_ExistingData = dbSCMS.SETUP_Departments.Where(c => c.Dpt_Id.Equals(lrow_Department.Dpt_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.Dpt_Title = lrow_Department.Dpt_Title;
                    lRow_ExistingData.Loc_Id = lrow_Department.Loc_Id;
                }
                else
                {
                    dbSCMS.SETUP_Departments.InsertOnSubmit(lrow_Department);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(lrow_Department.Dpt_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_Department> GetAllRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Departments.Where(c => c.Dpt_Active == 1).OrderBy(c => c.Dpt_Code).ToList();
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


        public int DeleteRecordById(String ps_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SETUP_Department where Dpt_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

   }
}
