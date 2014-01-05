using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SCMSDataLayer;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System.ComponentModel;
//using System.Collection.Generic.List;
//using System.Reflection;
using SCMSDataLayer.DB;

using System.Globalization;

namespace SCMS.Reports
{
    public partial class ViewReport : System.Web.UI.Page
    {
        string ls_ReportName = "", ls_Company = "", ls_ApplicationName = "", ls_User = "";

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(SCMS.Reports.ReportParameters.ReportName))
                {
                    ls_ReportName = SCMS.Reports.ReportParameters.ReportName;

                    if (SCMS.SystemParameters.ModuleDesc == null ||
                        SCMS.SystemParameters.ModuleDesc.Trim() == "")
                    {
                        ls_ApplicationName = "Application";
                    }
                    else
                    {
                        ls_ApplicationName = SCMS.SystemParameters.ModuleDesc;
                    }

                    if (SCMS.SystemParameters.CurrentCmpName == null ||
                        SCMS.SystemParameters.CurrentCmpName.Trim() == "")
                    {
                        ls_Company = "Demo. Company";
                    }
                    else
                    {
                        ls_Company = SCMS.SystemParameters.CurrentCmpName;
                    }

                    SECURITY_User lobj_User = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
                    if (lobj_User.User_Title == null ||
                        lobj_User.User_Title.Trim() == "")
                    {
                        ls_User = "Administrator";
                    }
                    else
                    {
                        ls_User = lobj_User.User_Title.Trim();
                    }

                    BindReports();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        #endregion

        #region Function
        void BindReports()
        {
            DALReports _dalReports = new DALReports();
            DataSet _ds = new DataSet();
            ReportDocument _ReportDocument = new ReportDocument();

            try
            {
                #region Setups
                #region Company Logo
                DataTable _dtImage = new DataTable();
                DataRow _dRow;
                FileStream _fs;
                BinaryReader _br;

                string _ServerPath = "";

                _ServerPath = Server.MapPath("../");

                _dtImage.TableName = "Logo";
                _dtImage.Columns.Add("CmpLogo", System.Type.GetType("System.Byte[]"));
                _dRow = _dtImage.NewRow();

                if (File.Exists(_ServerPath + "\\img\\logo.png"))
                {
                    _fs = new FileStream(_ServerPath + "\\img\\logo.png", FileMode.Open);
                }
                else
                {
                    _fs = new FileStream(_ServerPath + "\\img\\logo.png", FileMode.Open);
                }

                _br = new BinaryReader(_fs);
                Byte[] _imgbyte = new Byte[_fs.Length + 1];
                _imgbyte = _br.ReadBytes(Convert.ToInt32((_fs.Length + 1)));
                _dRow[0] = _imgbyte;
                _dtImage.Rows.Add(_dRow);

                //_br.Close();
                _fs.Close();
                #endregion

                #region Company
                if (ls_ReportName.ToLower() == "Company".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptCompany.rpt");
                    _ds = new Datasets.dsCompany();

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("Company"))
                    {
                        _ds.Tables.Remove("Company");
                    }

                    _ds = _dalReports.ListOfCompanies();
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SummaryInfo.ReportTitle = "List Of Companies";
                }
                #endregion

                #region Location
                if (ls_ReportName.ToLower() == "Location".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptLocation.rpt");
                    _ds = new Datasets.dsLocation();

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("Location"))
                    {
                        _ds.Tables.Remove("Location");
                    }

                    _ds = _dalReports.ListOfLocations();
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SummaryInfo.ReportTitle = "List Of Locations";
                }
                #endregion

                #region City
                if (ls_ReportName.ToLower() == "City".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptCity.rpt");
                    _ds = new Datasets.dsCity();

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("City"))
                    {
                        _ds.Tables.Remove("City");
                    }

                    _ds = _dalReports.ListOfCities();
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SummaryInfo.ReportTitle = "List Of Cities";
                }
                #endregion

                #region Voucher Types
                if (ls_ReportName.ToLower() == "VoucherTypes".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptVoucherTypes.rpt");
                    _ds = new Datasets.dsVoucherTypes();

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("VoucherTypes"))
                    {
                        _ds.Tables.Remove("VoucherTypes");
                    }

                    _ds = _dalReports.ListOfVoucherTypes();
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SummaryInfo.ReportTitle = "List Of Voucher Types";
                }
                #endregion

                #region Chart Of Account
                if (ls_ReportName.ToLower() == "ChartOfAccount".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptChartOfAccount.rpt");
                    _ds = new Datasets.dsChartOfAccount();

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("ChartOfAccount"))
                    {
                        _ds.Tables.Remove("ChartOfAccount");
                    }

                    _ds = _dalReports.ListOfChartOfAccounts(ReportParameters.Level);
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SummaryInfo.ReportTitle = "List Of Chart Of Account";
                }
                #endregion
                #endregion

                #region Voucher Document
                else if (ls_ReportName.ToLower() == "VoucherDocument".ToLower())
                {
                    if (SCMS.Reports.ReportParameters.VoucherPrint.ToLower() == "Detail".ToLower())
                    {
                        _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptVoucherDocument.rpt");
                    }
                    else if (SCMS.Reports.ReportParameters.VoucherPrint.ToLower() == "Summary".ToLower())
                    {
                        _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptVoucherDocumentSummary.rpt");
                    }
                    _ds = new Datasets.dsVoucherDocument();
                    string ls_Location = "", ls_VoucherTypes = "", ls_DocFrom = "", ls_DocTo = "", ls_VchrStatus = "All";
                    int li_AllDoc = 0, li_AllVchrStatus = 0, li_AllDate = 0;
                    DateTime ldt_DateFrom, ldt_Dateto;

                    ls_Location = SCMS.Reports.ReportParameters.Location;
                    ls_VoucherTypes = SCMS.Reports.ReportParameters.VoucherTypes;
                    li_AllDoc = SCMS.Reports.ReportParameters.AllDoc;
                    ls_DocFrom = SCMS.Reports.ReportParameters.DocFrom;
                    ls_DocTo = SCMS.Reports.ReportParameters.DocTo;
                    li_AllVchrStatus = SCMS.Reports.ReportParameters.AllVchrStatus;
                    ls_VchrStatus = SCMS.Reports.ReportParameters.VoucherStatus;
                    li_AllDate = SCMS.Reports.ReportParameters.AllDate;
                    ldt_DateFrom = SCMS.Reports.ReportParameters.DateFrom;
                    ldt_Dateto = SCMS.Reports.ReportParameters.DateTo;

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("VoucherDocument"))
                    {
                        _ds.Tables.Remove("VoucherDocument");
                    }

                    _ds = _dalReports.VoucherDocument(ls_Location, ls_VoucherTypes, li_AllDoc, ls_DocFrom, ls_DocTo,
                                                      li_AllVchrStatus, ls_VchrStatus, li_AllDate, ldt_DateFrom, ldt_Dateto);
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    if (SCMS.Reports.ReportParameters.VoucherPrint.ToLower() == "Detail".ToLower())
                    {
                        _ReportDocument.SummaryInfo.ReportTitle = "Voucher Detail";
                    }
                    else if (SCMS.Reports.ReportParameters.VoucherPrint.ToLower() == "Summary".ToLower())
                    {
                        _ReportDocument.SummaryInfo.ReportTitle = "Voucher Summary";
                    }
                }
                #endregion

                #region Ledger Detail - Location Wise
                else if (ls_ReportName.ToLower() == "LedgerDtLocWise".ToLower())
                {

                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptLedger_LocWise.rpt");
                    _ds = new Datasets.dsLedger();
                    string ls_Location = "", ls_AccCodeFrom = "", ls_AccCodeTo = "";
                    int li_AllAccCode = 0, li_AllDate = 0;
                    DateTime ldt_DateFrom, ldt_Dateto;

                    ls_Location = SCMS.Reports.ReportParameters.Location;
                    li_AllAccCode = SCMS.Reports.ReportParameters.AllAccCode;
                    ls_AccCodeFrom = SCMS.Reports.ReportParameters.AccCodeFrom;
                    ls_AccCodeTo = SCMS.Reports.ReportParameters.AccCodeTo;
                    li_AllDate = SCMS.Reports.ReportParameters.AllDate;
                    ldt_DateFrom = SCMS.Reports.ReportParameters.DateFrom;
                    ldt_Dateto = SCMS.Reports.ReportParameters.DateTo;

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("LedgerDetail"))
                    {
                        _ds.Tables.Remove("LedgerDetail");
                    }

                    _ds = _dalReports.LedgerDetail_LocWise(ls_Location, li_AllAccCode, ls_AccCodeFrom, ls_AccCodeTo, li_AllDate, ldt_DateFrom, ldt_Dateto);
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SummaryInfo.ReportTitle = "Ledger Detail - Location Wise";
                    _ReportDocument.SetParameterValue("pm_AllDate", li_AllDate);
                    _ReportDocument.SetParameterValue("pm_DateFrom", ldt_DateFrom);
                    _ReportDocument.SetParameterValue("pm_DateTo", ldt_Dateto);
                }
                #endregion

                #region Ledger Detail - Account Wise
                else if (ls_ReportName.ToLower() == "LedgerDtAccWise".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptLedger_AccWise.rpt");
                    _ds = new Datasets.dsLedger();
                    string ls_Location = "", ls_AccCodeFrom = "", ls_AccCodeTo = "";
                    int li_AllAccCode = 0, li_AllDate = 0;
                    DateTime ldt_DateFrom, ldt_Dateto;

                    ls_Location = SCMS.Reports.ReportParameters.Location;
                    li_AllAccCode = SCMS.Reports.ReportParameters.AllAccCode;
                    ls_AccCodeFrom = SCMS.Reports.ReportParameters.AccCodeFrom;
                    ls_AccCodeTo = SCMS.Reports.ReportParameters.AccCodeTo;
                    li_AllDate = SCMS.Reports.ReportParameters.AllDate;
                    ldt_DateFrom = SCMS.Reports.ReportParameters.DateFrom;
                    ldt_Dateto = SCMS.Reports.ReportParameters.DateTo;

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("LedgerDetail"))
                    {
                        _ds.Tables.Remove("LedgerDetail");
                    }

                    _ds = _dalReports.LedgerDetail_AccWise(ls_Location, li_AllAccCode, ls_AccCodeFrom, ls_AccCodeTo, li_AllDate, ldt_DateFrom, ldt_Dateto);
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SummaryInfo.ReportTitle = "Ledger Detail - Account Wise";
                    _ReportDocument.SetParameterValue("pm_AllDate", li_AllDate);
                    _ReportDocument.SetParameterValue("pm_DateFrom", ldt_DateFrom);
                    _ReportDocument.SetParameterValue("pm_DateTo", ldt_Dateto);
                }
                #endregion

                #region Trial Balance
                else if (ls_ReportName.ToLower() == "TrialBalance".ToLower())
                {
                    string ls_Location = "", ls_AccCodeFrom = "", ls_AccCodeTo = "";
                    int li_AllAccCode = 0, li_AllDate = 0, li_ChrtOfAccLevel = 5;
                    DateTime ldt_DateFrom, ldt_Dateto;
                    _ds = new Datasets.dsTrialBalance();

                    ls_Location = SCMS.Reports.ReportParameters.Location;
                    li_AllAccCode = SCMS.Reports.ReportParameters.AllAccCode;
                    ls_AccCodeFrom = SCMS.Reports.ReportParameters.AccCodeFrom;
                    ls_AccCodeTo = SCMS.Reports.ReportParameters.AccCodeTo;
                    li_AllDate = SCMS.Reports.ReportParameters.AllDate;
                    ldt_DateFrom = SCMS.Reports.ReportParameters.DateFrom;
                    ldt_Dateto = SCMS.Reports.ReportParameters.DateTo;
                    li_ChrtOfAccLevel = SCMS.Reports.ReportParameters.Level;

                    if (SCMS.Reports.ReportParameters.TrialActivity == "Activity")
                    {
                        _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptTrialBalance.rpt");
                        _ReportDocument.SummaryInfo.ReportTitle = "Trial Balance - Activity";
                    }
                    else if (SCMS.Reports.ReportParameters.TrialActivity == "Opening")
                    {
                        _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptTrialBalance-Opening.rpt");
                        _ReportDocument.SummaryInfo.ReportTitle = "Trial Balance - As at " + ldt_DateFrom.ToString("dd/MMM/yyyy");
                    }
                    else if (SCMS.Reports.ReportParameters.TrialActivity == "Closing")
                    {
                        _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptTrialBalance-Closing.rpt");
                        _ReportDocument.SummaryInfo.ReportTitle = "Trial Balance - As at " + ldt_Dateto.ToString("dd/MMM/yyyy");
                    }
                    else if (SCMS.Reports.ReportParameters.TrialActivity == "ActivityAll")
                    {
                        _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptTrialBalance-ActivityAll.rpt");
                        _ReportDocument.SummaryInfo.ReportTitle = "Trial Balance - As at " + ldt_Dateto.ToString("dd/MMM/yyyy");
                    }

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("TrialBalance"))
                    {
                        _ds.Tables.Remove("TrialBalance");
                    }

                    _ds = _dalReports.TrialBalance(ls_Location, li_AllAccCode, ls_AccCodeFrom, ls_AccCodeTo, li_AllDate, ldt_DateFrom,
                                                   ldt_Dateto, li_ChrtOfAccLevel, SCMS.Reports.ReportParameters.TrialActivity);
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SetParameterValue("pm_AllDate", li_AllDate);
                    _ReportDocument.SetParameterValue("pm_DateFrom", ldt_DateFrom);
                    _ReportDocument.SetParameterValue("pm_DateTo", ldt_Dateto);
                }
                #endregion

                #region Income Statement
                else if (ls_ReportName.ToLower() == "IncomeStatement".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptIncomeStatement.rpt");
                    _ds = new Datasets.dsIncomeStatement();
                    string ls_Location = "";
                    int li_Level = 0, li_Year = 0;

                    ls_Location = SCMS.Reports.ReportParameters.Location;
                    li_Level = SCMS.Reports.ReportParameters.Level;
                    li_Year = SCMS.Reports.ReportParameters.Year;

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("IncomeStatement"))
                    {
                        _ds.Tables.Remove("IncomeStatement");
                    }

                    _ds = _dalReports.IncomeStatement(ls_Location, li_Level, li_Year);
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SummaryInfo.ReportTitle = "Income Statement";
                    _ReportDocument.SetParameterValue("pm_CurrentYear", li_Year);
                    _ReportDocument.SetParameterValue("pm_PreviousYear", li_Year - 1);
                }
                #endregion

                #region Balance Sheet
                else if (ls_ReportName.ToLower() == "BalanceSheet".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptBalanceSheet.rpt");
                    _ds = new Datasets.dsBalanceSheet();
                    string ls_Location = "";
                    int li_Level = 0, li_Year = 0;

                    ls_Location = SCMS.Reports.ReportParameters.Location;
                    li_Level = SCMS.Reports.ReportParameters.Level;
                    li_Year = SCMS.Reports.ReportParameters.Year;

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("Assets"))
                    {
                        _ds.Tables.Remove("Assets");
                    }

                    if (_ds.Tables.Contains("Equity"))
                    {
                        _ds.Tables.Remove("Equity");
                    }

                    _ds = _dalReports.BalanceSheet(ls_Location, li_Level, li_Year);
                    _ds.Tables.Add(_ds.Tables["Assets"].Copy());
                    _ds.Tables.Add(_ds.Tables["Equity"].Copy());
                    //_dsBalanceSheet.Tables[0].TableName = "BalanceSheet";

                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SummaryInfo.ReportTitle = "Balance Sheet";
                    _ReportDocument.SetParameterValue("pm_CurrentYear", li_Year);
                    _ReportDocument.SetParameterValue("pm_PreviousYear", li_Year - 1);
                }
                #endregion

                #region Audit Trial
                else if (ls_ReportName.ToLower() == "AuditTrail".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptAuditTrail.rpt");
                    _ds = new Datasets.dsAuditTrail();
                    int li_AllDate = 0;
                    DateTime ldt_DateFrom, ldt_Dateto;

                    //string ls_Location = "";
                    //int li_Level = 0, li_Year = 0;

                    //ls_Location = SCMS.Reports.ReportParameters.Location;
                    //li_Level = SCMS.Reports.ReportParameters.Level;
                    //li_Year = SCMS.Reports.ReportParameters.Year;

                    li_AllDate = SCMS.Reports.ReportParameters.AllDate;
                    ldt_DateFrom = SCMS.Reports.ReportParameters.DateFrom;
                    ldt_Dateto = SCMS.Reports.ReportParameters.DateTo;

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("AuditTrail"))
                    {
                        _ds.Tables.Remove("AuditTrail");
                    }

                    _ds = _dalReports.AuditTrail(li_AllDate, ldt_DateFrom, ldt_Dateto);
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    //for (int _Index = 0; _Index <= _dsAuditTrail.Tables[0].Rows.Count - 1; _Index++)
                    //{
                    //    string _Data = "";
                    //    int _IndexOf = 0;

                    //    if (_dsAuditTrail.Tables[0].Rows[_Index]["AdtTrl_DataDump"] != null &&
                    //        _dsAuditTrail.Tables[0].Rows[_Index]["AdtTrl_DataDump"].ToString() != "")
                    //    {
                    //        _Data = _dsAuditTrail.Tables[0].Rows[_Index]["AdtTrl_DataDump"].ToString();

                    //        for (int _Index2 = 0; 
                    //        _Data = _Data + Environment.NewLine ;
                    //    }
                    //}

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SummaryInfo.ReportTitle = "Audit Log";
                    //_ReportDocument.SetParameterValue("pm_CurrentYear", li_Year);
                    //_ReportDocument.SetParameterValue("pm_PreviousYear", li_Year - 1);
                }
                #endregion

                #region Budget Summary
                else if (ls_ReportName.ToLower() == "BudgetSummary".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptBudgetSummary.rpt");
                    _ds = new Datasets.dsBudgetSummary();
                    string ls_Location = "", ls_Calendar = "";
                    int li_AllCalendar = 0;

                    ls_Location = SCMS.Reports.ReportParameters.Location;
                    li_AllCalendar = SCMS.Reports.ReportParameters.AllCalendar;
                    ls_Calendar = SCMS.Reports.ReportParameters.sCalendar;

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("BudgetSummary"))
                    {
                        _ds.Tables.Remove("BudgetSummary");
                    }

                    _ds = _dalReports.BudgetSummary(ls_Location, li_AllCalendar, ls_Calendar);
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SummaryInfo.ReportTitle = "Budget Summary";
                }
                #endregion

                #region Budget And Monthly Expense
                else if (ls_ReportName.ToLower() == "BudgetAndMonthlyExpense".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptBudgetAndMonthlyExpense.rpt");
                    _ds = new Datasets.dsBudgetAndMonthlyExpense();
                    string ls_Location = "", ls_Calendar = "";
                    int li_AllCalendar = 0;

                    ls_Location = SCMS.Reports.ReportParameters.Location;
                    li_AllCalendar = SCMS.Reports.ReportParameters.AllCalendar;
                    ls_Calendar = SCMS.Reports.ReportParameters.sCalendar;

                    if (_ds.Tables.Contains("Logo"))
                    {
                        _ds.Tables.Remove("Logo");
                    }

                    if (_ds.Tables.Contains("BudgetAndMonthlyExpense"))
                    {
                        _ds.Tables.Remove("BudgetAndMonthlyExpense");
                    }

                    _ds = _dalReports.BudgetAndMonthlyExpense(ls_Location, li_AllCalendar, ls_Calendar);
                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_ds);
                    _ReportDocument.SummaryInfo.ReportTitle = "Budget And Monthly Expenditure";
                }
                #endregion

                _ReportDocument.SetParameterValue("pm_CompanyName", ls_Company);
                _ReportDocument.SummaryInfo.ReportComments = ls_ApplicationName;
                _ReportDocument.SummaryInfo.ReportAuthor = ls_User;
                crvReports.ReportSource = _ReportDocument;
                crvReports.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.InnerHtml = ex.Message.ToString();
            }
        }
        #endregion

        #region Events
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            //IBLDocument _blDocument = CorespondenceBLFactory.GetDocument() as IBLDocument;
            //DocumentSchema _schema = new DocumentSchema();
            //IEnvelope _envelope = new Envelope();
            //int _DocId = 0, _ReturnValue = 0;

            //try
            //{
            //    _DocId = Convert.ToInt32(lblId.Text);

            //    if (_DocId > 0)
            //    {
            //        _schema.ID = _DocId;
            //        if (txtReports.Content != null && txtReports.Content.ToString() != "")
            //        {
            //            _schema.Letter = txtReports.Content.ToString();
            //        }
            //        _envelope.SetMaster(_schema);
            //        _ReturnValue = _blDocument.Edit(_envelope);

            //        if (_ReturnValue > 0)
            //        {
            //            imgsuccess.Visible = true;
            //            imgerror.Visible = false;
            //            SHMA.HR.HRIS.CORE.Utilities.SetMessage(lblMessage, "Record Saved Successfully", MessageType.Information);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    imgsuccess.Visible = false;
            //    imgerror.Visible = true;
            //    if (ex.Message.Trim().ToString().Length >= 60)
            //    {
            //        Utilities.SetMessage(lblMessage, ex.Message.Substring(0, 60), MessageType.Error);
            //    }
            //    else
            //    {
            //        Utilities.SetMessage(lblMessage, ex.Message.ToString(), MessageType.Error);
            //    }
            //}
        }
        #endregion
    }
}
