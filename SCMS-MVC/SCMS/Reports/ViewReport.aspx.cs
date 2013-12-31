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
                    Datasets.dsCompany _dsCompany = new Datasets.dsCompany();

                    if (_dsCompany.Tables.Contains("Logo"))
                    {
                        _dsCompany.Tables.Remove("Logo");
                    }

                    if (_dsCompany.Tables.Contains("Company"))
                    {
                        _dsCompany.Tables.Remove("Company");
                    }

                    _ds = _dalReports.ListOfCompanies();
                    _dsCompany.Tables.Add(_ds.Tables["Company"].Copy());
                    _dsCompany.Tables.Add(_dtImage.Copy());

                    if (_dsCompany == null || _dsCompany.Tables == null || _dsCompany.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsCompany);
                    _ReportDocument.SummaryInfo.ReportTitle = "List Of Companies";
                }
                #endregion

                #region Location
                if (ls_ReportName.ToLower() == "Location".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptLocation.rpt");
                    Datasets.dsLocation _dsLocation = new Datasets.dsLocation();

                    if (_dsLocation.Tables.Contains("Logo"))
                    {
                        _dsLocation.Tables.Remove("Logo");
                    }

                    if (_dsLocation.Tables.Contains("Location"))
                    {
                        _dsLocation.Tables.Remove("Location");
                    }

                    _ds = _dalReports.ListOfLocations();
                    _dsLocation.Tables.Add(_ds.Tables["Location"].Copy());
                    _dsLocation.Tables.Add(_dtImage.Copy());

                    if (_dsLocation == null || _dsLocation.Tables == null || _dsLocation.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsLocation);
                    _ReportDocument.SummaryInfo.ReportTitle = "List Of Locations";
                }
                #endregion

                #region City
                if (ls_ReportName.ToLower() == "City".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptCity.rpt");
                    Datasets.dsCity _dsCity = new Datasets.dsCity();

                    if (_dsCity.Tables.Contains("Logo"))
                    {
                        _dsCity.Tables.Remove("Logo");
                    }

                    if (_dsCity.Tables.Contains("City"))
                    {
                        _dsCity.Tables.Remove("City");
                    }

                    _ds = _dalReports.ListOfCities();
                    _dsCity.Tables.Add(_ds.Tables["City"].Copy());
                    _dsCity.Tables.Add(_dtImage.Copy());

                    if (_dsCity == null || _dsCity.Tables == null || _dsCity.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsCity);
                    _ReportDocument.SummaryInfo.ReportTitle = "List Of Cities";
                }
                #endregion

                #region Voucher Types
                if (ls_ReportName.ToLower() == "VoucherTypes".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptVoucherTypes.rpt");
                    Datasets.dsVoucherTypes _dsVoucherTypes = new Datasets.dsVoucherTypes();

                    if (_dsVoucherTypes.Tables.Contains("Logo"))
                    {
                        _dsVoucherTypes.Tables.Remove("Logo");
                    }

                    if (_dsVoucherTypes.Tables.Contains("VoucherTypes"))
                    {
                        _dsVoucherTypes.Tables.Remove("VoucherTypes");
                    }

                    _ds = _dalReports.ListOfVoucherTypes();
                    _dsVoucherTypes.Tables.Add(_ds.Tables["VoucherTypes"].Copy());
                    _dsVoucherTypes.Tables.Add(_dtImage.Copy());

                    if (_dsVoucherTypes == null || _dsVoucherTypes.Tables == null || _dsVoucherTypes.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsVoucherTypes);
                    _ReportDocument.SummaryInfo.ReportTitle = "List Of Voucher Types";
                }
                #endregion

                #region Chart Of Account
                if (ls_ReportName.ToLower() == "ChartOfAccount".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptChartOfAccount.rpt");
                    Datasets.dsChartOfAccount _dsChartOfAccount = new Datasets.dsChartOfAccount();

                    if (_dsChartOfAccount.Tables.Contains("Logo"))
                    {
                        _dsChartOfAccount.Tables.Remove("Logo");
                    }

                    if (_dsChartOfAccount.Tables.Contains("ChartOfAccount"))
                    {
                        _dsChartOfAccount.Tables.Remove("ChartOfAccount");
                    }

                    _ds = _dalReports.ListOfChartOfAccounts(ReportParameters.Level);
                    _dsChartOfAccount.Tables.Add(_ds.Tables["ChartOfAccount"].Copy());
                    _dsChartOfAccount.Tables.Add(_dtImage.Copy());

                    if (_dsChartOfAccount == null || _dsChartOfAccount.Tables == null || _dsChartOfAccount.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsChartOfAccount);
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
                    Datasets.dsVoucherDocument _dsVoucherDocument = new Datasets.dsVoucherDocument();
                    string ls_Location = "", ls_VoucherTypes = "", ls_DocFrom = "", ls_DocTo = "", ls_VchrStatus= "All";
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

                    if (_dsVoucherDocument.Tables.Contains("Logo"))
                    {
                        _dsVoucherDocument.Tables.Remove("Logo");
                    }

                    if (_dsVoucherDocument.Tables.Contains("VoucherDocument"))
                    {
                        _dsVoucherDocument.Tables.Remove("VoucherDocument");
                    }

                    _ds = _dalReports.VoucherDocument(ls_Location, ls_VoucherTypes, li_AllDoc, ls_DocFrom, ls_DocTo, 
                                                      li_AllVchrStatus, ls_VchrStatus, li_AllDate, ldt_DateFrom, ldt_Dateto);
                    _dsVoucherDocument.Tables.Add(_ds.Tables[0].Copy());
                    _dsVoucherDocument.Tables[0].TableName = "VoucherDocument";

                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsVoucherDocument);
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
                    Datasets.dsLedger _dsLedger = new Datasets.dsLedger();
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

                    if (_dsLedger.Tables.Contains("Logo"))
                    {
                        _dsLedger.Tables.Remove("Logo");
                    }

                    if (_dsLedger.Tables.Contains("LedgerDetail"))
                    {
                        _dsLedger.Tables.Remove("LedgerDetail");
                    }

                    _ds = _dalReports.LedgerDetail_LocWise(ls_Location, li_AllAccCode, ls_AccCodeFrom, ls_AccCodeTo, li_AllDate, ldt_DateFrom, ldt_Dateto);
                    _dsLedger.Tables.Add(_ds.Tables[0].Copy());
                    _dsLedger.Tables[0].TableName = "LedgerDetail";

                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsLedger);
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
                    Datasets.dsLedger _dsLedger = new Datasets.dsLedger();
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

                    if (_dsLedger.Tables.Contains("Logo"))
                    {
                        _dsLedger.Tables.Remove("Logo");
                    }

                    if (_dsLedger.Tables.Contains("LedgerDetail"))
                    {
                        _dsLedger.Tables.Remove("LedgerDetail");
                    }

                    _ds = _dalReports.LedgerDetail_AccWise(ls_Location, li_AllAccCode, ls_AccCodeFrom, ls_AccCodeTo, li_AllDate, ldt_DateFrom, ldt_Dateto);
                    _dsLedger.Tables.Add(_ds.Tables[0].Copy());
                    _dsLedger.Tables[0].TableName = "LedgerDetail";

                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsLedger);
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
                    Datasets.dsTrialBalance _dsTrialBalance = new Datasets.dsTrialBalance();

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

                    if (_dsTrialBalance.Tables.Contains("Logo"))
                    {
                        _dsTrialBalance.Tables.Remove("Logo");
                    }

                    if (_dsTrialBalance.Tables.Contains("TrialBalance"))
                    {
                        _dsTrialBalance.Tables.Remove("TrialBalance");
                    }

                    _ds = _dalReports.TrialBalance(ls_Location, li_AllAccCode, ls_AccCodeFrom, ls_AccCodeTo, li_AllDate, ldt_DateFrom,
                                                   ldt_Dateto, li_ChrtOfAccLevel, SCMS.Reports.ReportParameters.TrialActivity);
                    _dsTrialBalance.Tables.Add(_ds.Tables[0].Copy());
                    _dsTrialBalance.Tables[0].TableName = "TrialBalance";

                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsTrialBalance);
                    _ReportDocument.SetParameterValue("pm_AllDate", li_AllDate);
                    _ReportDocument.SetParameterValue("pm_DateFrom", ldt_DateFrom);
                    _ReportDocument.SetParameterValue("pm_DateTo", ldt_Dateto);
                }
                #endregion

                #region Income Statement
                else if (ls_ReportName.ToLower() == "IncomeStatement".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptIncomeStatement.rpt");
                    Datasets.dsIncomeStatement _dsIncomeStatement = new Datasets.dsIncomeStatement();
                    string ls_Location = "";
                    int li_Level = 0, li_Year = 0;
                    //DateTime ldt_DateFrom, ldt_Dateto;

                    ls_Location = SCMS.Reports.ReportParameters.Location;
                    li_Level = SCMS.Reports.ReportParameters.Level;
                    li_Year = SCMS.Reports.ReportParameters.Year;

                    if (_dsIncomeStatement.Tables.Contains("Logo"))
                    {
                        _dsIncomeStatement.Tables.Remove("Logo");
                    }

                    if (_dsIncomeStatement.Tables.Contains("IncomeStatement"))
                    {
                        _dsIncomeStatement.Tables.Remove("IncomeStatement");
                    }

                    _ds = _dalReports.IncomeStatement(ls_Location, li_Level, li_Year);
                    _dsIncomeStatement.Tables.Add(_ds.Tables[0].Copy());
                    _dsIncomeStatement.Tables[0].TableName = "IncomeStatement";

                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsIncomeStatement);
                    _ReportDocument.SummaryInfo.ReportTitle = "Income Statement";
                    _ReportDocument.SetParameterValue("pm_CurrentYear", li_Year);
                    _ReportDocument.SetParameterValue("pm_PreviousYear", li_Year - 1);
                }
                #endregion

                #region Balance Sheet
                else if (ls_ReportName.ToLower() == "BalanceSheet".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptBalanceSheet.rpt");
                    Datasets.dsBalanceSheet _dsBalanceSheet = new Datasets.dsBalanceSheet();
                    string ls_Location = "";
                    int li_Level = 0, li_Year = 0;

                    ls_Location = SCMS.Reports.ReportParameters.Location;
                    li_Level = SCMS.Reports.ReportParameters.Level;
                    li_Year = SCMS.Reports.ReportParameters.Year;

                    if (_dsBalanceSheet.Tables.Contains("Logo"))
                    {
                        _dsBalanceSheet.Tables.Remove("Logo");
                    }

                    if (_dsBalanceSheet.Tables.Contains("Assets"))
                    {
                        _dsBalanceSheet.Tables.Remove("Assets");
                    }

                    if (_dsBalanceSheet.Tables.Contains("Equity"))
                    {
                        _dsBalanceSheet.Tables.Remove("Equity");
                    }

                    _ds = _dalReports.BalanceSheet(ls_Location, li_Level, li_Year);
                    _dsBalanceSheet.Tables.Add(_ds.Tables["Assets"].Copy());
                    _dsBalanceSheet.Tables.Add(_ds.Tables["Equity"].Copy());
                    //_dsBalanceSheet.Tables[0].TableName = "BalanceSheet";

                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsBalanceSheet);
                    _ReportDocument.SummaryInfo.ReportTitle = "Balance Sheet";
                    _ReportDocument.SetParameterValue("pm_CurrentYear", li_Year);
                    _ReportDocument.SetParameterValue("pm_PreviousYear", li_Year - 1);
                }
                #endregion

                #region Audit Trial
                else if (ls_ReportName.ToLower() == "AuditTrail".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptAuditTrail.rpt");
                    Datasets.dsAuditTrail _dsAuditTrail = new Datasets.dsAuditTrail();
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

                    if (_dsAuditTrail.Tables.Contains("Logo"))
                    {
                        _dsAuditTrail.Tables.Remove("Logo");
                    }

                    if (_dsAuditTrail.Tables.Contains("AuditTrail"))
                    {
                        _dsAuditTrail.Tables.Remove("AuditTrail");
                    }

                    _ds = _dalReports.AuditTrail(li_AllDate, ldt_DateFrom, ldt_Dateto);
                    _dsAuditTrail.Tables.Add(_ds.Tables[0].Copy());
                    _dsAuditTrail.Tables[0].TableName = "AuditTrail";

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

                    _ReportDocument.SetDataSource(_dsAuditTrail);
                    _ReportDocument.SummaryInfo.ReportTitle = "Audit Log";
                    //_ReportDocument.SetParameterValue("pm_CurrentYear", li_Year);
                    //_ReportDocument.SetParameterValue("pm_PreviousYear", li_Year - 1);
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
