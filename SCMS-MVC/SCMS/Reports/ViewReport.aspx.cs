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
using System.Reflection;

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

                    if (SCMS.SystemParameters.CurrentAppName == null ||
                        SCMS.SystemParameters.CurrentAppName.Trim() == "")
                    {
                        ls_ApplicationName = "Application";
                    }
                    else
                    {
                        ls_ApplicationName = SCMS.SystemParameters.CurrentAppName;
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

                    if (SCMS.SystemParameters.CurrentUserName == null ||
                        SCMS.SystemParameters.CurrentUserName.Trim() == "")
                    {
                        ls_User = "Administrator";
                    }
                    else
                    {
                        ls_User = SCMS.SystemParameters.CurrentUserName;
                    }

                    BindReports();
                }
            }
            catch (Exception ex)
            {

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
                }
                #endregion

                #region Trial Balance
                else if (ls_ReportName.ToLower() == "TrialBalance".ToLower())
                {
                    _ReportDocument.Load(_ServerPath + "\\Reports\\Reps\\rptTrialBalance.rpt");
                    Datasets.dsTrialBalance _dsTrialBalance = new Datasets.dsTrialBalance();
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

                    if (_dsTrialBalance.Tables.Contains("Logo"))
                    {
                        _dsTrialBalance.Tables.Remove("Logo");
                    }

                    if (_dsTrialBalance.Tables.Contains("TrialBalance"))
                    {
                        _dsTrialBalance.Tables.Remove("TrialBalance");
                    }

                    _ds = _dalReports.TrialBalance(ls_Location, li_AllAccCode, ls_AccCodeFrom, ls_AccCodeTo, li_AllDate, ldt_DateFrom, ldt_Dateto);
                    _dsTrialBalance.Tables.Add(_ds.Tables[0].Copy());
                    _dsTrialBalance.Tables[0].TableName = "TrialBalance";

                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsTrialBalance);
                    _ReportDocument.SummaryInfo.ReportTitle = "Trial Balance";
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
