using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALWithholdingTaxType 
    { 
        //public int SaveRecord(SETUP_WithholdingTaxType lrow_CalendarType)
        //{
        //    int li_ReturnValue = 0;

        //    try
        //    {
        //        SCMSDataContext dbSCMS = Connection.Create();
        //        SETUP_WithholdingTaxType lRow_ExistingData = dbSCMS.SETUP_WithholdingTaxTypes.Where(b => b.CldrType_Id.Equals(lrow_CalendarType.CldrType_Id)).SingleOrDefault();

        //        if (lRow_ExistingData != null)
        //        {
        //            lRow_ExistingData.CldrType_Title = lrow_CalendarType.CldrType_Title;
        //            lRow_ExistingData.Loc_Id = lrow_CalendarType.Loc_Id;
        //            lRow_ExistingData.Cmp_Id = lrow_CalendarType.Cmp_Id;
        //            lRow_ExistingData.CldrType_Level = lrow_CalendarType.CldrType_Level;
        //        }
        //        else
        //        {
        //            dbSCMS.SETUP_WithholdingTaxTypes.InsertOnSubmit(lrow_CalendarType);
        //        }
        //        dbSCMS.SubmitChanges();

        //        li_ReturnValue = Convert.ToInt32(lrow_CalendarType.CldrType_Id);
        //    }
        //    catch
        //    {
        //        return 0;
        //    }

        //    return li_ReturnValue;
        //}

        //public List<SETUP_WithholdingTaxType> GetAllRecords()
        //{
        //    try
        //    {
        //        SCMSDataContext dbSCMS = Connection.Create();
        //        return dbSCMS.SETUP_WithholdingTaxTypes.Where(c => c.CldrType_Active == 1).OrderBy(c => c.CldrType_Code).ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public List<SETUP_Location> GetAllLocation()
        //{
        //    try
        //    {
        //        SCMSDataContext dbSCMS = Connection.Create();
        //        return dbSCMS.SETUP_Locations.Where(c => c.Loc_Active == 1).ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public List<SETUP_Company> GetAllCompanies()
        //{
        //    try
        //    {
        //        SCMSDataContext dbSCMS = Connection.Create();
        //        return dbSCMS.SETUP_Companies.Where(c => c.Cmp_Active == 1).ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public int DeleteRecordById(String ps_Id)
        //{
        //    int li_ReturnValue = 0;

        //    try
        //    {
        //        SCMSDataContext dbSCMS = Connection.Create();
        //        li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SETUP_WithholdingTaxType where CldrType_Id='" + ps_Id + "'");
        //    }
        //    catch
        //    {
        //        li_ReturnValue = 0;
        //    }

        //    return li_ReturnValue;
        //}

        //public List<SETUP_Bank> PopulateData()
        //{
        //    try
        //    {
        //        SCMSDataContext dbSCMS = Connection.Create();
        //        return dbSCMS.SETUP_Banks.Where(c => c.Bank_Active == 1).ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public List<SETUP_WithholdingTaxType> Retrieve()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_WithholdingTaxTypes.Where(c=> c.WthHoldType_Active == 1 ).OrderBy(c => c.WthHoldType_Code).ToList();
            }
            catch
            {
                return null;
            }
        }

        public int DeleteDetailRecordByMasterId(String GLMaster_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From GL_BgdtDetail where BgdtMas_Id='" + GLMaster_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public int Save(GL_BgdtMaster newBudgetMaster)
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                GL_BgdtMaster existingBudgetMaster = dbSCMS.GL_BgdtMasters.Where(c => c.BgdtMas_Id.Equals(newBudgetMaster.BgdtMas_Id)).SingleOrDefault();
                if (existingBudgetMaster != null)
                {
                    existingBudgetMaster.BgdtMas_Date = newBudgetMaster.BgdtMas_Date;
                    existingBudgetMaster.BgdtMas_Status = newBudgetMaster.BgdtMas_Status;
                    existingBudgetMaster.BgdtType_Id = newBudgetMaster.BgdtType_Id;
                    existingBudgetMaster.Cldr_Id = newBudgetMaster.Cldr_Id;
                    existingBudgetMaster.Loc_Id = newBudgetMaster.Loc_Id;
                    existingBudgetMaster.BgdtMas_Remarks = newBudgetMaster.BgdtMas_Remarks;
                }
                else
                {
                    dbSCMS.GL_BgdtMasters.InsertOnSubmit(newBudgetMaster);
                }
                dbSCMS.SubmitChanges();
            }
            catch
            {
                return 0;
            }

            return 1;
        }

    }
}
