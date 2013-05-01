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
                    lRow_ExistingData.ChrtAcc_Code = pRow_NewData.ChrtAcc_Code;
                    lRow_ExistingData.ChrtAcc_Title = pRow_NewData.ChrtAcc_Title;
                    lRow_ExistingData.ChrtAcc_Level = pRow_NewData.ChrtAcc_Level;
                    lRow_ExistingData.ChrtAcc_BudgetLevel = pRow_NewData.ChrtAcc_BudgetLevel;
                    lRow_ExistingData.ChrtAcc_Type = pRow_NewData.ChrtAcc_Type;
                    lRow_ExistingData.ChrtAcc_Active = pRow_NewData.ChrtAcc_Active;
                    lRow_ExistingData.Natr_Id = pRow_NewData.Natr_Id;
                    //lRow_ExistingData.AccNatr_Id = pRow_NewData.AccNatr_Id;
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
                List<SETUP_ChartOfAccount> chartOfAccountList = (List<SETUP_ChartOfAccount>)dbSCMS.ExecuteQuery<SETUP_ChartOfAccount>("Select * from SETUP_ChartOfAccount where ChrtAcc_Code like (Select ChrtAcc_Code From SETUP_ChartOfAccount where ChrtAcc_Id = '" + ps_Id + "')+'%'", "").ToList();
                if (chartOfAccountList != null && chartOfAccountList.Count == 1)
                {
                    li_ReturnValue = dbSCMS.ExecuteCommand("Delete From Setup_ChartOfAccount where ChrtAcc_Id='" + ps_Id + "'");
                }
            }
            catch (Exception ex)
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
            string _Sql = "";
            try
            {


                // ChrtAcc_Type

                _Sql += "  Select SETUP_ChartOfAccount.ChrtAcc_Id, ";
                _Sql += "         SETUP_ChartOfAccount.ChrtAcc_Title + ' [' + SETUP_ChartOfAccount.ChrtAcc_Code + ']'  as ChrtAcc_Title ";
                _Sql += "    From SETUP_ChartOfAccount ";
                _Sql += "   Where ( IsNULL( SETUP_ChartOfAccount.ChrtAcc_Type, 0 ) = 2 ) ";
                _Sql += "Order By SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "         SETUP_ChartOfAccount.ChrtAcc_Level";


                SCMSDataContext dbSCMS = Connection.Create();
                List<SETUP_ChartOfAccount> ChartOfAccountList = new List<SETUP_ChartOfAccount>();
                ChartOfAccountList = dbSCMS.ExecuteQuery<SETUP_ChartOfAccount>(_Sql).ToList();
                return ChartOfAccountList;
            }
            catch
            {
                return null;
            }
        }

        #region Chart of Account Modification
        public List<SETUP_ChartOfAccount> GetAllRecordsFirstLevel()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                List<SETUP_ChartOfAccount> chartOfAccountList = new List<SETUP_ChartOfAccount>();
                chartOfAccountList = dbSCMS.ExecuteQuery<SETUP_ChartOfAccount>("Select * From SETUP_ChartOfAccount Where ChrtAcc_Level = 1 Order By ChrtAcc_Code,ChrtAcc_Level").ToList();
                return chartOfAccountList;
            }
            catch
            {
                return null;
            }
        }

        public int ReplaceOldCode_WithNewCode(String ps_OldCode, string ps_NewCode)
        {
            int li_ReturnValue = 0;
            string _Sql = "";

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();

                _Sql += "  Update Setup_ChartOfAccount ";
                _Sql += "     Set '" + ps_NewCode + "' + SubString(ChrtAcc_Code, LEN( '" + ps_NewCode + "' ) + 1, LEN( ChrtAcc_Code ) ) ";
                _Sql += "   Where Left( ChrtAcc_Code, Len( '" + ps_OldCode + "' ) ) = '" + ps_OldCode + "' ";

                li_ReturnValue = dbSCMS.ExecuteCommand(_Sql);
            }
            catch (Exception ex)
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }
        #endregion
    }
}
