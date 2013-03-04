﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALChartOfAccount
    {
        public int SaveRecord(SETUP_ChartOfAccount pRow_NewData)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_ChartOfAccount lRow_ExistingData = dbSCMS.SETUP_ChartOfAccounts.Where(c => c.ChrtAcc_Id.Equals(pRow_NewData.ChrtAcc_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.ChrtAcc_Title = pRow_NewData.ChrtAcc_Title;
                    lRow_ExistingData.ChrtAcc_Level = pRow_NewData.ChrtAcc_Level;
                    lRow_ExistingData.ChrtAcc_BudgetLevel = pRow_NewData.ChrtAcc_BudgetLevel;
                    lRow_ExistingData.ChrtAcc_Type = pRow_NewData.ChrtAcc_Type;
                    lRow_ExistingData.ChrtAcc_Active = pRow_NewData.ChrtAcc_Active;
                    lRow_ExistingData.Natr_Id = pRow_NewData.Natr_Id;
                    lRow_ExistingData.AccNatr_Id = pRow_NewData.AccNatr_Id;
                }
                else
                {
                    dbSCMS.SETUP_ChartOfAccounts.InsertOnSubmit(pRow_NewData);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = 1;//Convert.ToInt32(pRow_NewData.ChrtAcc_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public int DeleteRecordById(String ps_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Setup_ChartOfAccount where ChrtAcc_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_ChartOfAccount> GetAllRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                List<SETUP_ChartOfAccount> chartOfAccountList = new List<SETUP_ChartOfAccount>();
                chartOfAccountList = dbSCMS.ExecuteQuery<SETUP_ChartOfAccount>("Select * From SETUP_ChartOfAccount Order By ChrtAcc_Code,ChrtAcc_Level").ToList();
                return chartOfAccountList;
                //return dbSCMS.SETUP_ChartOfAccounts.OrderBy(c => c.ChrtAcc_Code).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<SETUP_ChartOfAccount> GetChartOfAccountForDropDown()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                List<SETUP_ChartOfAccount> ChartOfAccountList = new List<SETUP_ChartOfAccount>();
                ChartOfAccountList = dbSCMS.ExecuteQuery<SETUP_ChartOfAccount>("Select ChrtAcc_Id,ChrtAcc_Title+'['+ChrtAcc_Code+']' as ChrtAcc_Title From SETUP_ChartOfAccount Order By ChrtAcc_Code,ChrtAcc_Level").ToList();
                return ChartOfAccountList;
            }
            catch
            {
                return null;
            }
        }
    }
}
