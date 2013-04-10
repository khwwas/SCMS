using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;
namespace SCMSDataLayer
{
    public class DALCompany
    {
        public int SaveCompany(SETUP_Company newSetupCompanyRow)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_Company existingSetupCompanyRow = dbSCMS.SETUP_Companies.Where(c => c.Cmp_Id.Equals(newSetupCompanyRow.Cmp_Id)).SingleOrDefault();
                if (existingSetupCompanyRow != null)
                {
                    existingSetupCompanyRow.Cmp_Title = newSetupCompanyRow.Cmp_Title;
                    existingSetupCompanyRow.Cmp_Address1 = newSetupCompanyRow.Cmp_Address1;
                    existingSetupCompanyRow.Cmp_Address2 = newSetupCompanyRow.Cmp_Address2;
                    existingSetupCompanyRow.Cmp_Email = newSetupCompanyRow.Cmp_Email;
                    existingSetupCompanyRow.Cmp_Phone = newSetupCompanyRow.Cmp_Phone;
                    existingSetupCompanyRow.Cmp_Fax = newSetupCompanyRow.Cmp_Fax;
                }
                else
                {
                    dbSCMS.SETUP_Companies.InsertOnSubmit(newSetupCompanyRow);
                }
                dbSCMS.SubmitChanges();
                return Convert.ToInt32(newSetupCompanyRow.Cmp_Id);
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteCompanyByCompanyId(String companyId)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                int result = dbSCMS.ExecuteCommand("Delete From Setup_Company where Cmp_Id='" + companyId + "'");
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        //public List<SETUP_Company> GetAllCompanies()
        //{
        //    try
        //    {
        //        SCMSDataContext dbSCMS = Connection.Create();

        //        //return dbSCMS.sp_GetCompanyList.Where(c => c.Cmp_Active == 1).ToList();

        //        //return dbSCMS.SETUP_Companies.Where(c => c.Cmp_Active == 1).ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public List<sp_GetCompanyListResult> GetAllData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_GetCompanyList().ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<sp_PopulateCompanyListResult> PopulateData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_PopulateCompanyList().ToList();
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

        public List<SETUP_Company> GetCmpByCode(string ps_Code)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Companies.Where(c => c.Cmp_Code == ps_Code).ToList();
            }
            catch
            {
                return null;
            }
        }

    }
}
