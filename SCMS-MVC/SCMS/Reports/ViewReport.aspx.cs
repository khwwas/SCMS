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

namespace SCMS.Reports
{
    public partial class ViewReport : System.Web.UI.Page
    {
        string ReportName = "", ls_Company = "Daanish Schools", ls_ApplicationName = "Financial Management System - v1.0", ls_User = "Administrator";

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(SCMS.Reports.ReportParameters.ReportName))
                {
                    ReportName = SCMS.Reports.ReportParameters.ReportName;

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
                if (ReportName.ToLower() == "Company".ToLower())
                {
                    _ReportDocument.Load(Server.MapPath("/Reports/Reps/rptCompany.rpt"));
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
                if (ReportName.ToLower() == "Location".ToLower())
                {
                    _ReportDocument.Load(Server.MapPath("/Reports/Reps/rptLocation.rpt"));
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
                if (ReportName.ToLower() == "City".ToLower())
                {
                    _ReportDocument.Load(Server.MapPath("/Reports/Reps/rptCity.rpt"));
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
                if (ReportName.ToLower() == "VoucherTypes".ToLower())
                {
                    _ReportDocument.Load(Server.MapPath("/Reports/Reps/rptVoucherTypes.rpt"));
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
                if (ReportName.ToLower() == "ChartOfAccount".ToLower())
                {
                    _ReportDocument.Load(Server.MapPath("/Reports/Reps/rptChartOfAccount.rpt"));
                    Datasets.dsChartOfAccount _dsChartOfAccount = new Datasets.dsChartOfAccount();

                    if (_dsChartOfAccount.Tables.Contains("Logo"))
                    {
                        _dsChartOfAccount.Tables.Remove("Logo");
                    }

                    if (_dsChartOfAccount.Tables.Contains("ChartOfAccount"))
                    {
                        _dsChartOfAccount.Tables.Remove("ChartOfAccount");
                    }

                    _ds = _dalReports.ListOfChartOfAccounts();
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

                #region Ledger Detail
                else if (ReportName.ToLower() == "LedgerDetail".ToLower())
                {
                    _ReportDocument.Load(Server.MapPath("/Reports/Reps/rptLedger.rpt"));
                    Datasets.dsLedger _dsLedger = new Datasets.dsLedger();
                    string LocationCode = "";

                    LocationCode = SCMS.Reports.ReportParameters.Location;

                    if (_dsLedger.Tables.Contains("Logo"))
                    {
                        _dsLedger.Tables.Remove("Logo");
                    }

                    if (_dsLedger.Tables.Contains("LedgerDetail"))
                    {
                        _dsLedger.Tables.Remove("LedgerDetail");
                    }

                    _ds = _dalReports.LedgerDetail();
                    _dsLedger.Tables.Add(_ds.Tables[0].Copy());
                    _dsLedger.Tables[0].TableName = "LedgerDetail";

                    if (_ds == null || _ds.Tables == null || _ds.Tables.Count <= 0)
                    {
                        MessageBox.InnerHtml = "Report dindn't find anything against the selected criteria";
                        return;
                    }

                    _ReportDocument.SetDataSource(_dsLedger);
                    _ReportDocument.SummaryInfo.ReportTitle = "Ledger Detail";
                }
                #endregion

                #region Trial Balance
                else if (ReportName.ToLower() == "TrialBalance".ToLower())
                {
                    _ReportDocument.Load(Server.MapPath("/Reports/Reps/rptTrialBalance.rpt"));
                    Datasets.dsTrialBalance _dsTrialBalance = new Datasets.dsTrialBalance();
                    string LocationCode = "";

                    LocationCode = SCMS.Reports.ReportParameters.Location;

                    if (_dsTrialBalance.Tables.Contains("Logo"))
                    {
                        _dsTrialBalance.Tables.Remove("Logo");
                    }

                    if (_dsTrialBalance.Tables.Contains("TrialBalance"))
                    {
                        _dsTrialBalance.Tables.Remove("TrialBalance");
                    }

                    _ds = _dalReports.TrialBalance();
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

                #region Application Gantt Chart
                //else if (ReportName.ToLower() == "appganttchart")
                //{
                //    CriteriaSchema _Schema = new CriteriaSchema();
                //    _Schema.CompanyId = SHMA.HR.HRIS.CORE.Utilities.CompanyId;
                //    _Schema.DepartmentId = SHMA.HR.HRIS.CORE.Utilities.DepartmentId;
                //    _Schema.CandidateId = SHMA.HR.HRIS.CORE.Utilities.CandidateId;
                //    _Schema.JobProfileId = SHMA.HR.HRIS.CORE.Utilities.JobProfileId;
                //    _Schema.DateStart = SHMA.HR.HRIS.CORE.Utilities.DateStart;
                //    _Schema.DateEnd = SHMA.HR.HRIS.CORE.Utilities.DateEnd;
                //    IEnvelope _envelope = new Envelope();
                //    _envelope.SetMaster(_Schema);
                //    IBLReports _blAppGanttChart = (IBLReports)SHMA.HR.HRIS.PERFORMANCE.BUSINESSLOGIC.PerformanceBLFactory.GetReports();
                //    SHMA.HR.HRIS.UX.pem.DataSets.dsAppGanttChart _dsAppGanttChart = new SHMA.HR.HRIS.UX.pem.DataSets.dsAppGanttChart();
                //    if (_dsAppGanttChart.Tables.Contains("AppGanttChart"))
                //    {
                //        _dsAppGanttChart.Tables.Remove("AppGanttChart");
                //    }
                //    _dsAppGanttChart.Tables.Add((_blAppGanttChart.GetAssessmentGanttChartReportData(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsAppGanttChart.Tables[0].TableName = "AppGanttChart";
                //    SHMA.HR.HRIS.UX.pem.Reports.rptAppGanttChart _rptAppGanttChart = new SHMA.HR.HRIS.UX.pem.Reports.rptAppGanttChart();

                //    _rptAppGanttChart.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    _rptAppGanttChart.SummaryInfo.ReportComments = CommonMethods.UserContext.UserID;

                //    _rptAppGanttChart.SetDataSource(_dsAppGanttChart);
                //    crvReports.HasToggleGroupTreeButton = false;
                //    crvReports.ReportSource = _rptAppGanttChart;
                //    crvReports.DataBind();
                //}
                //#endregion
                //#region HR Action
                //else if (ReportName.ToLower() == "hraction")
                //{
                //    CriteriaSchema _Schema = new CriteriaSchema();
                //    _Schema.CompanyId = SHMA.HR.HRIS.CORE.Utilities.CompanyId;
                //    _Schema.DepartmentId = SHMA.HR.HRIS.CORE.Utilities.DepartmentId;
                //    _Schema.CandidateId = SHMA.HR.HRIS.CORE.Utilities.CandidateId;
                //    _Schema.JobProfileId = SHMA.HR.HRIS.CORE.Utilities.JobProfileId;
                //    IEnvelope _envelope = new Envelope();
                //    _envelope.SetMaster(_Schema);
                //    IBLReports _blHrAction = (IBLReports)SHMA.HR.HRIS.PERFORMANCE.BUSINESSLOGIC.PerformanceBLFactory.GetReports();
                //    SHMA.HR.HRIS.UX.pem.DataSets.dshraction _dsHrAction = new SHMA.HR.HRIS.UX.pem.DataSets.dshraction();
                //    if (_dsHrAction.Tables.Contains("HrAction"))
                //    {
                //        _dsHrAction.Tables.Remove("HrAction");
                //    }
                //    _dsHrAction.Tables.Add((_blHrAction.GetHrActionReportData(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsHrAction.Tables[0].TableName = "HrAction";
                //    SHMA.HR.HRIS.UX.pem.Reports.rpthraction _rptHrAction = new SHMA.HR.HRIS.UX.pem.Reports.rpthraction();
                //    _rptHrAction.SetDataSource(_dsHrAction);
                //    crvReports.HasToggleGroupTreeButton = false;
                //    crvReports.ReportSource = _rptHrAction;
                //    crvReports.DataBind();
                //    //ReportPrintingObjectAssignment(_rptHrAction);
                //}
                //#endregion
                //#region Capability Review
                //else if (ReportName.ToLower() == "capabilityreview")
                //{
                //    CriteriaSchema _Schema = new CriteriaSchema();
                //    _Schema.CompanyId = SHMA.HR.HRIS.CORE.Utilities.CompanyId;
                //    _Schema.DepartmentId = SHMA.HR.HRIS.CORE.Utilities.DepartmentId;
                //    _Schema.CandidateId = SHMA.HR.HRIS.CORE.Utilities.CandidateId;
                //    _Schema.JobProfileId = SHMA.HR.HRIS.CORE.Utilities.JobProfileId;
                //    IEnvelope _envelope = new Envelope();
                //    _envelope.SetMaster(_Schema);
                //    IBLReports _blEmployeeComparison = (IBLReports)SHMA.HR.HRIS.PERFORMANCE.BUSINESSLOGIC.PerformanceBLFactory.GetReports();
                //    SHMA.HR.HRIS.UX.pem.DataSets.dsEmployeeComparison _dsEmployeeComparison = new SHMA.HR.HRIS.UX.pem.DataSets.dsEmployeeComparison();
                //    if (_dsEmployeeComparison.Tables.Contains("EmployeeComparison"))
                //    {
                //        _dsEmployeeComparison.Tables.Remove("EmployeeComparison");
                //    }
                //    _dsEmployeeComparison.Tables.Add((_blEmployeeComparison.GetCapabilityReviewReportData(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsEmployeeComparison.Tables[0].TableName = "EmployeeComparison";
                //    SHMA.HR.HRIS.UX.pem.Reports.rptEmployeeComparison _rptEmployeeComparison = new SHMA.HR.HRIS.UX.pem.Reports.rptEmployeeComparison();
                //    _rptEmployeeComparison.SetDataSource(_dsEmployeeComparison);
                //    crvReports.HasToggleGroupTreeButton = false;

                //    _rptEmployeeComparison.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    _rptEmployeeComparison.SummaryInfo.ReportComments = CommonMethods.UserContext.UserID;

                //    crvReports.ReportSource = _rptEmployeeComparison;
                //    crvReports.DataBind();
                //}
                //#endregion
                //#region Department Comparison
                //else if (ReportName.ToLower() == "departmentcomparison")
                //{
                //    CriteriaSchema _Schema = new CriteriaSchema();
                //    _Schema.CompanyId = SHMA.HR.HRIS.CORE.Utilities.CompanyId;
                //    _Schema.DepartmentId = SHMA.HR.HRIS.CORE.Utilities.DepartmentId;
                //    _Schema.PeriodId = SHMA.HR.HRIS.CORE.Utilities.PeriodId;
                //    IEnvelope _envelope = new Envelope();
                //    _envelope.SetMaster(_Schema);
                //    IBLReports _blDepartmentComparison = (IBLReports)SHMA.HR.HRIS.PERFORMANCE.BUSINESSLOGIC.PerformanceBLFactory.GetReports();
                //    SHMA.HR.HRIS.UX.pem.DataSets.dsDeptComparison _dsDepartmentComparison = new SHMA.HR.HRIS.UX.pem.DataSets.dsDeptComparison();
                //    if (_dsDepartmentComparison.Tables.Contains("DeptComparison"))
                //    {
                //        _dsDepartmentComparison.Tables.Remove("DeptComparison");
                //    }
                //    _dsDepartmentComparison.Tables.Add((_blDepartmentComparison.GetDepartmentComparisonReportData(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsDepartmentComparison.Tables[0].TableName = "DeptComparison";
                //    SHMA.HR.HRIS.UX.pem.Reports.rptDeptComparison _rptDepartmentComparison = new SHMA.HR.HRIS.UX.pem.Reports.rptDeptComparison();
                //    _rptDepartmentComparison.SetDataSource(_dsDepartmentComparison);
                //    crvReports.HasToggleGroupTreeButton = false;

                //    _rptDepartmentComparison.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    _rptDepartmentComparison.SummaryInfo.ReportComments = CommonMethods.UserContext.UserID;

                //    crvReports.ReportSource = _rptDepartmentComparison;
                //    crvReports.DataBind();

                //}
                //#endregion
                //#region Transactions Status
                //else if (ReportName.ToLower() == "transactionstatus")
                //{
                //    CriteriaSchema _Schema = new CriteriaSchema();
                //    _Schema.CompanyId = SHMA.HR.HRIS.CORE.Utilities.CompanyId;
                //    _Schema.DepartmentId = SHMA.HR.HRIS.CORE.Utilities.DepartmentId;
                //    _Schema.JobProfileId = SHMA.HR.HRIS.CORE.Utilities.JobProfileId;
                //    _Schema.PeriodId = SHMA.HR.HRIS.CORE.Utilities.PeriodId;
                //    IEnvelope _envelope = new Envelope();
                //    _envelope.SetMaster(_Schema);
                //    IBLReports _blTransactionStatus = (IBLReports)SHMA.HR.HRIS.PERFORMANCE.BUSINESSLOGIC.PerformanceBLFactory.GetReports();
                //    SHMA.HR.HRIS.UX.pem.DataSets.dsTransactionStatus _dsTransactionStatus = new SHMA.HR.HRIS.UX.pem.DataSets.dsTransactionStatus();
                //    if (_dsTransactionStatus.Tables.Contains("TransactionStatus"))
                //    {
                //        _dsTransactionStatus.Tables.Remove("TransactionStatus");
                //    }
                //    if (_dsTransactionStatus.Tables.Contains("TransactionWithInBudget"))
                //    {
                //        _dsTransactionStatus.Tables.Remove("TransactionWithInBudget");
                //    }
                //    if (_dsTransactionStatus.Tables.Contains("TransactionOutOfBudget"))
                //    {
                //        _dsTransactionStatus.Tables.Remove("TransactionOutOfBudget");
                //    }
                //    _dsTransactionStatus.Tables.Add((_blTransactionStatus.GetTransactionStatusReportData(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsTransactionStatus.Tables[0].TableName = "TransactionStatus";

                //    _dsTransactionStatus.Tables.Add((_blTransactionStatus.GetTransactionWithinBudget(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsTransactionStatus.Tables[1].TableName = "TransactionWithInBudget";

                //    _dsTransactionStatus.Tables.Add((_blTransactionStatus.GetTransactionOutOfBudget(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsTransactionStatus.Tables[2].TableName = "TransactionOutOfBudget";

                //    SHMA.HR.HRIS.UX.pem.Reports.rptTransactionStatus _rptTransactionStatus = new SHMA.HR.HRIS.UX.pem.Reports.rptTransactionStatus();
                //    _rptTransactionStatus.SetDataSource(_dsTransactionStatus);
                //    crvReports.HasToggleGroupTreeButton = false;

                //    _rptTransactionStatus.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    _rptTransactionStatus.SummaryInfo.ReportComments = CommonMethods.UserContext.UserID;

                //    crvReports.ReportSource = _rptTransactionStatus;
                //    crvReports.DataBind();

                //}
                //#endregion
                //#region Performance Form
                //else if (ReportName.ToLower() == "performanceform")
                //{
                //    CriteriaSchema _Schema = new CriteriaSchema();
                //    _Schema.CompanyId = SHMA.HR.HRIS.CORE.Utilities.CompanyId;
                //    _Schema.DepartmentId = SHMA.HR.HRIS.CORE.Utilities.DepartmentId;
                //    _Schema.CandidateId = SHMA.HR.HRIS.CORE.Utilities.CandidateId;
                //    _Schema.PlanStageId = SHMA.HR.HRIS.CORE.Utilities.PlanStageId;
                //    _Schema.PeriodId = SHMA.HR.HRIS.CORE.Utilities.PeriodId;
                //    IEnvelope _envelope = new Envelope();
                //    _envelope.SetMaster(_Schema);
                //    IBLReports _blPerformanceForm = (IBLReports)SHMA.HR.HRIS.PERFORMANCE.BUSINESSLOGIC.PerformanceBLFactory.GetReports();
                //    SHMA.HR.HRIS.UX.pem.DataSets.dsAsmntMain _dsPerformanceForm = new SHMA.HR.HRIS.UX.pem.DataSets.dsAsmntMain();
                //    if (_dsPerformanceForm.Tables.Contains("HRLH_EmpData"))
                //    {
                //        _dsPerformanceForm.Tables.Remove("HRLH_EmpData");
                //    }
                //    if (_dsPerformanceForm.Tables.Contains("HRLH_PRFMOBJ"))
                //    {
                //        _dsPerformanceForm.Tables.Remove("HRLH_PRFMOBJ");
                //    }
                //    if (_dsPerformanceForm.Tables.Contains("HRLH_AsmntScale"))
                //    {
                //        _dsPerformanceForm.Tables.Remove("HRLH_AsmntScale");
                //    }
                //    if (_dsPerformanceForm.Tables.Contains("HRLH_AsmntSummary"))
                //    {
                //        _dsPerformanceForm.Tables.Remove("HRLH_AsmntSummary");
                //    }
                //    if (_dsPerformanceForm.Tables.Contains("HRLH_TrngReq"))
                //    {
                //        _dsPerformanceForm.Tables.Remove("HRLH_TrngReq");
                //    }
                //    if (_dsPerformanceForm.Tables.Contains("HRLH_AsmntPaperScore"))
                //    {
                //        _dsPerformanceForm.Tables.Remove("HRLH_AsmntPaperScore");
                //    }
                //    if (_dsPerformanceForm.Tables.Contains("HRLH_AsmntSection"))
                //    {
                //        _dsPerformanceForm.Tables.Remove("HRLH_AsmntSection");
                //    }
                //    if (_dsPerformanceForm.Tables.Contains("HRLH_AsmntComments"))
                //    {
                //        _dsPerformanceForm.Tables.Remove("HRLH_AsmntComments");
                //    }
                //    _dsPerformanceForm.Tables.Add((_blPerformanceForm.GetEmployeeData(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsPerformanceForm.Tables[0].TableName = "HRLH_EmpData";
                //    _dsPerformanceForm.Tables.Add((_blPerformanceForm.GetPRFMOBJData(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsPerformanceForm.Tables[1].TableName = "HRLH_PRFMOBJ";
                //    _dsPerformanceForm.Tables.Add((_blPerformanceForm.GetAsmntScalelevels(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsPerformanceForm.Tables[2].TableName = "HRLH_AsmntScale";
                //    _dsPerformanceForm.Tables.Add((_blPerformanceForm.GetAsmntSummaryData(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsPerformanceForm.Tables[3].TableName = "HRLH_AsmntSummary";
                //    //_dsPerformanceForm.Tables.Add((_blPerformanceForm.GetTrngReqData(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    //_dsPerformanceForm.Tables[4].TableName = "HRLH_TrngReq";
                //    _dsPerformanceForm.Tables.Add((_blPerformanceForm.GetAsmntPaperScore(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsPerformanceForm.Tables[4].TableName = "HRLH_AsmntPaperScore";
                //    _dsPerformanceForm.Tables.Add((_blPerformanceForm.GetAsmntSectionsData(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsPerformanceForm.Tables[5].TableName = "HRLH_AsmntSection";
                //    _dsPerformanceForm.Tables.Add((_blPerformanceForm.GetAsmntCommentsData(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsPerformanceForm.Tables[6].TableName = "HRLH_AsmntComments";

                //    //SHMA.HR.HRIS.UX.pem.Reports.rptAsmntMain _rptPerformanceForm = new SHMA.HR.HRIS.UX.pem.Reports.rptAsmntMain();
                //    SHMA.HR.HRIS.UX.pem.Reports.rptAsmntMain_WWF _rptPerformanceForm = new SHMA.HR.HRIS.UX.pem.Reports.rptAsmntMain_WWF();
                //    _rptPerformanceForm.SetDataSource(_dsPerformanceForm);
                //    crvReports.HasToggleGroupTreeButton = false;

                //    _rptPerformanceForm.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    _rptPerformanceForm.SummaryInfo.ReportComments = CommonMethods.UserContext.UserID;

                //    crvReports.ReportSource = _rptPerformanceForm;
                //    crvReports.DataBind();

                //}
                //#endregion
                //#endregion
                //#region Employee Complete Profile
                //else if (ReportName.ToLower() == "EmployeeCompProfile".ToLower())
                //{
                //    IBLReports _blAsmntPlan = (IBLReports)SHMA.HR.HRIS.PERFORMANCE.BUSINESSLOGIC.PerformanceBLFactory.GetReports();
                //    SHMA.HR.HRIS.UX.pem.Reports.rptEmployeeProfile _rptEmpProfile = new SHMA.HR.HRIS.UX.pem.Reports.rptEmployeeProfile();
                //    SHMA.HR.HRIS.UX.pem.DataSets.dsEmployeeProfile _dsEmpProfile = new SHMA.HR.HRIS.UX.pem.DataSets.dsEmployeeProfile();
                //    IEnvelope _envelope = new Envelope();
                //    CriteriaSchema _Schema = new CriteriaSchema();

                //    try
                //    {
                //        _Schema.CompanyId = SHMA.HR.HRIS.CORE.Utilities.CompanyId;
                //        _Schema.DepartmentId = SHMA.HR.HRIS.CORE.Utilities.DepartmentId;
                //        _Schema.JobProfileId = SHMA.HR.HRIS.CORE.Utilities.JobProfileId;
                //        _Schema.EmployeeId = SHMA.HR.HRIS.CORE.Utilities.CandidateId;

                //        _envelope.SetMaster(_Schema);
                //        if (_dsEmpProfile.Tables.Contains("EmployeeData"))
                //        {
                //            _dsEmpProfile.Tables.Remove("EmployeeData");
                //        }
                //        if (_dsEmpProfile.Tables.Contains("Qualification"))
                //        {
                //            _dsEmpProfile.Tables.Remove("Qualification");
                //        }
                //        if (_dsEmpProfile.Tables.Contains("EmploymentHistory"))
                //        {
                //            _dsEmpProfile.Tables.Remove("EmploymentHistory");
                //        }
                //        if (_dsEmpProfile.Tables.Contains("PromotionHistory"))
                //        {
                //            _dsEmpProfile.Tables.Remove("PromotionHistory");
                //        }
                //        if (_dsEmpProfile.Tables.Contains("SalaryHistory"))
                //        {
                //            _dsEmpProfile.Tables.Remove("SalaryHistory");
                //        }
                //        if (_dsEmpProfile.Tables.Contains("Posting History"))
                //        {
                //            _dsEmpProfile.Tables.Remove("Posting History");
                //        }
                //        if (_dsEmpProfile.Tables.Contains("PerformanceHistory"))
                //        {
                //            _dsEmpProfile.Tables.Remove("PerformanceHistory");
                //        }
                //        if (_dsEmpProfile.Tables.Contains("TrainingHistory"))
                //        {
                //            _dsEmpProfile.Tables.Remove("TrainingHistory");
                //        }

                //        _dsEmpProfile.Tables.Add((_blAsmntPlan.EmpProfile_EmpData(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //        _dsEmpProfile.Tables[0].TableName = "EmployeeData";

                //        _dsEmpProfile.Tables.Add((_blAsmntPlan.EmpProfile_Qualification(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //        _dsEmpProfile.Tables[1].TableName = "Qualification";

                //        _dsEmpProfile.Tables.Add((_blAsmntPlan.EmpProfile_EmploymentHistory(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //        _dsEmpProfile.Tables[2].TableName = "EmploymentHistory";

                //        _dsEmpProfile.Tables.Add((_blAsmntPlan.EmpProfile_PromotionHistory(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //        _dsEmpProfile.Tables[3].TableName = "PromotionHistory";

                //        _dsEmpProfile.Tables.Add((_blAsmntPlan.EmpProfile_SalaryRevisionHistory(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //        _dsEmpProfile.Tables[4].TableName = "SalaryHistory";

                //        _rptEmpProfile.SetDataSource(_dsEmpProfile);
                //        crvReports.HasToggleGroupTreeButton = false;
                //        _rptEmpProfile.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //        _rptEmpProfile.SummaryInfo.ReportComments = CommonMethods.UserContext.UserID;
                //        crvReports.ReportSource = _rptEmpProfile;
                //        crvReports.DataBind();
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new Exception(ex.Message.ToString());
                //    }
                //}
                //#endregion
                //#region Job Profile Sheet
                //else if (ReportName.ToLower() == "jdprofilesheet")
                //{
                //    string _strSearch = Request.QueryString["id"];

                //    IBLJobDescription _bl = (IBLJobDescription)SHMA.HR.HRIS.PROFILE.BUSINESSLOGIC.ProfileBLFactory.GetJobDescription();
                //    IEnvelope _envelope = new Envelope();

                //    _envelope.SetMaster(_strSearch.ToString());
                //    DataSet _ds = (DataSet)_bl.GetJobPositionReport(_envelope).GetMaster();
                //    DataSet _dssection = (DataSet)_bl.GetJPSectionReport(_envelope).GetMaster();
                //    DataSet _dsSkills = (DataSet)_bl.GetJPSkillsReport(_envelope).GetMaster();
                //    DataSet _dsQualification = (DataSet)_bl.GetJPQualificationReport(_envelope).GetMaster();
                //    DataSet _dsExpreience = (DataSet)_bl.GetJPExpreienceReport(_envelope).GetMaster();
                //    DataSet _dsLanguage = (DataSet)_bl.GetJPLanguageReport(_envelope).GetMaster();

                //    ps = new SHMA.HR.HRIS.UX.jp.Reports.rptJobPosition();

                //    ps.SetDataSource(_ds.Tables[0]);
                //    ps.Subreports["RptJPSection.rpt"].SetDataSource(_dssection.Tables[0]);
                //    ps.Subreports["rptJPSkills.rpt"].SetDataSource(_dsSkills.Tables[0]);


                //    ps.Subreports["rptJPQualification.rpt"].SetDataSource(_dsQualification.Tables[0]);
                //    //ps.DetailSection3.SectionFormat.EnableKeepTogether = false;

                //    ps.Subreports["rptJPExpreince.rpt"].SetDataSource(_dsExpreience.Tables[0]);
                //    ps.Subreports["rptJPLanguage.rpt"].SetDataSource(_dsLanguage.Tables[0]);

                //    ps.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    ps.SummaryInfo.ReportComments = SHMA.HR.HRIS.CORE.Utilities.CurrentUserName;
                //    crvReports.HasToggleGroupTreeButton = false;
                //    crvReports.ReportSource = ps;
                //    crvReports.DataBind();
                //    ReportPrintingObjectAssignment(ps);
                //}
                //#endregion
                //#region Candidate Comparison
                //else if (ReportName.ToLower() == "candidatecomparison")
                //{
                //    IBLCandidate _bl = (IBLCandidate)SHMA.HR.HRIS.RECRUITMENT.BUSINESSLOGIC.RecruitmentBLFactory.GetCandidate();

                //    IEnvelope _envelope = new Envelope();

                //    string _str = Request.QueryString["id"];
                //    _envelope.SetMaster(_str);

                //    DataSet _ds = (DataSet)_bl.GetCandidateForComparison(_envelope).GetMaster();
                //    SHMA.HR.HRIS.UX.rec.Report.rptCandidateComparison _candidateComparison_source = new SHMA.HR.HRIS.UX.rec.Report.rptCandidateComparison();
                //    _candidateComparison_source.SetDataSource(_ds.Tables[0]);
                //    _candidateComparison_source.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    _candidateComparison_source.SummaryInfo.ReportComments = SHMA.HR.HRIS.CORE.Utilities.CurrentUserName;

                //    crvReports.HasToggleGroupTreeButton = false;
                //    //crvReports.HasToggleParameterPanelButton = false;
                //    crvReports.ReportSource = _candidateComparison_source;
                //    ReportPrintingObjectAssignment(_candidateComparison_source);

                //}
                //#endregion
                //#region Employee Identification
                //else if (ReportName.ToLower() == "employeeidentification")
                //{
                //    IBLEmployee _bl = (IBLEmployee)SHMA.HR.HRIS.SETUP.BUSINESSLOGIC.SetupBLFactory.GetEmployee();

                //    IEnvelope _envelope = new Envelope();

                //    string _str = Request.QueryString["id"];
                //    _envelope.SetMaster(_str);

                //    DataSet _ds = (DataSet)_bl.GetEmployeeByID(_envelope).GetMaster();

                //    SHMA.HR.HRIS.UX.Pers.Report.rptEmployee _empinfo_source = new SHMA.HR.HRIS.UX.Pers.Report.rptEmployee();
                //    _empinfo_source.SetDataSource(_ds.Tables[0]);
                //    _empinfo_source.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    _empinfo_source.SummaryInfo.ReportComments = SHMA.HR.HRIS.CORE.Utilities.CurrentUserName;

                //    crvReports.HasToggleGroupTreeButton = false;
                //    crvReports.ReportSource = _empinfo_source;


                //}
                //#endregion
                //#region Joining Report
                //else if (ReportName.ToLower() == "joiningreport")
                //{
                //    IBLScheduling _bl = (IBLScheduling)SHMA.HR.HRIS.RECRUITMENT.BUSINESSLOGIC.RecruitmentBLFactory.GetScheduling();

                //    IEnvelope _envelope = new Envelope();

                //    string _str = Request.QueryString["id"];
                //    _envelope.SetMaster(_str);

                //    DataSet _ds = (DataSet)_bl.GetCandidateByCandidateID(_envelope).GetMaster();

                //    SHMA.HR.HRIS.UX.rec.Report.rptJoiningReport _joiningReport_source = new SHMA.HR.HRIS.UX.rec.Report.rptJoiningReport();
                //    _joiningReport_source.SetDataSource(_ds.Tables[0]);
                //    _joiningReport_source.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    _joiningReport_source.SummaryInfo.ReportComments = SHMA.HR.HRIS.CORE.Utilities.CurrentUserName;

                //    crvReports.HasToggleGroupTreeButton = false;
                //    crvReports.ReportSource = _joiningReport_source;
                //    ReportPrintingObjectAssignment(_joiningReport_source);
                //}
                //#endregion
                //#region Candidate Listing
                //else if (ReportName.ToLower() == "candidatelisting")
                //{
                //    IBLCandidate _bl = (IBLCandidate)SHMA.HR.HRIS.RECRUITMENT.BUSINESSLOGIC.RecruitmentBLFactory.GetCandidate();

                //    IEnvelope _envelope = new Envelope();
                //    //var url = "../Home/ViewReport.aspx?ReportName=candidatelisting&deptID=" + deptID + "&cityID=" + cityID + "&jdID=" + jdID
                //    //     + "&expectedDate=" + expectedDate + "&requestStatus=" + requestStatus + "&requestNO=" + requestNo;

                //    string deptID = Request.QueryString["deptID"];
                //    string cityID = Request.QueryString["cityID"];
                //    string jdID = Request.QueryString["jdID"];
                //    string expectedDate = Request.QueryString["expectedDate"];
                //    string requestStatus = Request.QueryString["requestStatus"];
                //    int requestNo = Convert.ToInt32(Request.QueryString["requestNO"]);

                //    DataSet _ds = (DataSet)_bl.GetCandidateListing(deptID, cityID, jdID, expectedDate, requestStatus, requestNo).GetMaster();
                //    SHMA.HR.HRIS.UX.rec.Report.rptCandidateListing _candidateListing_source = new SHMA.HR.HRIS.UX.rec.Report.rptCandidateListing();
                //    _candidateListing_source.SetDataSource(_ds.Tables[0]);
                //    _candidateListing_source.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    _candidateListing_source.SummaryInfo.ReportComments = SHMA.HR.HRIS.CORE.Utilities.CurrentUserName;

                //    //start setting parameter values


                //    if (!string.IsNullOrEmpty(deptID))
                //    {
                //        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                //            _candidateListing_source.DataDefinition.FormulaFields["fDepartment"].Text = string.Format("\"{0}\"", _ds.Tables[0].Rows[0]["DPT_TITLE"].ToString());
                //    }
                //    else
                //        _candidateListing_source.DataDefinition.FormulaFields["fDepartment"].Text = string.Format("\"{0}\"", "ALL");

                //    if (!string.IsNullOrEmpty(jdID))
                //    {
                //        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                //            _candidateListing_source.DataDefinition.FormulaFields["fJobPosition"].Text = string.Format("\"{0}\"", _ds.Tables[0].Rows[0]["JBT_TITLE"].ToString());
                //    }
                //    else
                //        _candidateListing_source.DataDefinition.FormulaFields["fJobPosition"].Text = string.Format("\"{0}\"", "ALL");

                //    if (!string.IsNullOrEmpty(cityID))
                //    {
                //        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                //            _candidateListing_source.DataDefinition.FormulaFields["fLocation"].Text = string.Format("\"{0}\"", _ds.Tables[0].Rows[0]["CTY_TITLE"].ToString());
                //    }
                //    else
                //        _candidateListing_source.DataDefinition.FormulaFields["fLocation"].Text = string.Format("\"{0}\"", "ALL");

                //    if (requestNo > 0)
                //    {
                //        _candidateListing_source.DataDefinition.FormulaFields["fRequestNo"].Text = string.Format("\"{0}\"", requestNo.ToString());
                //    }
                //    else
                //        _candidateListing_source.DataDefinition.FormulaFields["fRequestNo"].Text = string.Format("\"{0}\"", "ALL");

                //    _candidateListing_source.DataDefinition.FormulaFields["fProject"].Text = string.Format("\"{0}\"", "ALL");

                //    if (!string.IsNullOrEmpty(expectedDate))
                //    {
                //        _candidateListing_source.DataDefinition.FormulaFields["fExpectedDate"].Text = string.Format("\"{0}\"", Convert.ToDateTime(expectedDate).ToString("dd/MMM/yyyy"));
                //    }
                //    else
                //        _candidateListing_source.DataDefinition.FormulaFields["fExpectedDate"].Text = string.Format("\"{0}\"", "ALL");

                //    if (!string.IsNullOrEmpty(requestStatus))
                //    {
                //        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                //            _candidateListing_source.DataDefinition.FormulaFields["fRequestStatus"].Text = string.Format("\"{0}\"", _ds.Tables[0].Rows[0]["REQ_STATUS"].ToString());
                //    }
                //    else
                //        _candidateListing_source.DataDefinition.FormulaFields["fRequestStatus"].Text = string.Format("\"{0}\"", "ALL");

                //    //end setting parameter values
                //    crvReports.ReportSource = _candidateListing_source;
                //    //this.crvReports.RefreshReport();

                //    //ReportPrintingObjectAssignment(_candidateListing_source);
                //    crvReports.HasToggleGroupTreeButton = false;
                //    crvReports.HasPageNavigationButtons = true;
                //    crvReports.DisplayToolbar = true;
                //    crvReports.HasExportButton = false;
                //    crvReports.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
                //    crvReports.HasSearchButton = false;
                //    crvReports.HasZoomFactorList = true;
                //    //crvReports.HasToggleParameterPanelButton = false;

                //}
                //#endregion
                //#region Job Request Status
                //else if (ReportName.ToLower() == "jobrequeststatus")
                //{
                //    IBLRequest _bl = (IBLRequest)SHMA.HR.HRIS.RECRUITMENT.BUSINESSLOGIC.RecruitmentBLFactory.GetRequest();

                //    IEnvelope _envelope = new Envelope();
                //    string deptID = Request.QueryString["deptID"];
                //    string locID = Request.QueryString["LocID"];
                //    string jdID = Request.QueryString["jdID"];
                //    string frmDate = Request.QueryString["frmDate"];
                //    string toDate = Request.QueryString["toDate"];
                //    string dateCheckedOn = Request.QueryString["dateCheckedOn"];

                //    DataSet _ds = (DataSet)_bl.GetJobRequestStatus(deptID, locID, jdID, frmDate, toDate, dateCheckedOn).GetMaster();
                //    for (int col = 0; col < _ds.Tables[0].Columns.Count; col++)
                //        if (col == 12 || col == 16 || col == 17)
                //        {
                //            for (int row = 0; row < _ds.Tables[0].Rows.Count; row++)
                //                if (string.IsNullOrEmpty(_ds.Tables[0].Rows[row][col].ToString()))
                //                    _ds.Tables[0].Rows[row][col] = "0";
                //        }
                //    //Start setting CV RECEIEVED COUNT
                //    DataSet _dsWJApplication = null;
                //    for (int r = 0; r < _ds.Tables[0].Rows.Count; r++)
                //    {
                //        _dsWJApplication = GetApplicationFromHRMS("WHERE JobPost.JobPost_sHCM_Ref='" + _ds.Tables[0].Rows[r]["REQ_HCM_REF_NO"] + "'", _ds.Tables[0].Rows[r]["REQ_HCM_REF_NO"].ToString());

                //        if (_dsWJApplication.Tables.Count > 1)
                //            _ds.Tables[0].Rows[r]["CV_RECEIVED"] = _dsWJApplication.Tables[1].Rows[0][0].ToString();
                //    }
                //    //End setting CV RECEIEVED COUNT

                //    SHMA.HR.HRIS.UX.rec.Report.rptJobRequestStatus _jobRequestStatus_source = new SHMA.HR.HRIS.UX.rec.Report.rptJobRequestStatus();
                //    _jobRequestStatus_source.SetDataSource(_ds.Tables[0]);
                //    _jobRequestStatus_source.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    _jobRequestStatus_source.SummaryInfo.ReportComments = SHMA.HR.HRIS.CORE.Utilities.CurrentUserName;

                //    crvReports.ReportSource = _jobRequestStatus_source;
                //    crvReports.HasToggleGroupTreeButton = false;
                //    crvReports.HasPageNavigationButtons = true;
                //    crvReports.DisplayToolbar = true;
                //    crvReports.HasExportButton = false;
                //    crvReports.HasPrintButton = false;
                //    crvReports.HasSearchButton = false;
                //    crvReports.HasZoomFactorList = true;
                //    //crvReports.HasToggleParameterPanelButton = false;
                //}
                //#endregion
                //#region Employee Data Collection
                //else if (ReportName.ToLower() == "employeedatacollection")
                //{
                //    IBLEmployee _bl = (IBLEmployee)SHMA.HR.HRIS.SETUP.BUSINESSLOGIC.SetupBLFactory.GetEmployee();
                //    IEnvelope _envelope = new Envelope();
                //    string _str = Request.QueryString["id"];
                //    _envelope.SetMaster(_str);
                //    DataSet _ds = (DataSet)_bl.GetEmployeeDataCollectionByEmpID(_envelope).GetMaster();
                //    SHMA.HR.HRIS.UX.Pers.Report.rptEmployeeDataCollection _rptEDC = new SHMA.HR.HRIS.UX.Pers.Report.rptEmployeeDataCollection();
                //    _rptEDC.SetDataSource(_ds.Tables[0]);
                //    _rptEDC.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    _rptEDC.SummaryInfo.ReportComments = SHMA.HR.HRIS.CORE.Utilities.CurrentUserName;

                //    IBLTRecommendation _blRecommend = (IBLTRecommendation)SHMA.HR.HRIS.TND.BUSINESSLOGIC.TndBLFactory.GetTRecommendation();
                //    IEnvelope _envelope_recommend = new Envelope();
                //    _envelope_recommend.SetMaster(_str);
                //    DataSet _dsRecommend = (DataSet)_blRecommend.GetRecommendationByEmpID(_envelope_recommend).GetMaster();
                //    _rptEDC.Subreports[0].SetDataSource(_dsRecommend.Tables[0]);

                //    crvReports.ReportSource = _rptEDC;
                //    ReportPrintingObjectAssignment(_rptEDC);

                //    //Setting Report Viewer Parameters
                //    crvReports.HasToggleGroupTreeButton = false;
                //    //crvReports.HasToggleParameterPanelButton = false;
                //    crvReports.HasPageNavigationButtons = true;
                //    crvReports.DisplayToolbar = true;
                //    crvReports.HasExportButton = true;
                //    crvReports.HasPrintButton = true;
                //    crvReports.HasSearchButton = true;
                //    crvReports.HasZoomFactorList = true;
                //}
                //#endregion
                //#region training Nomination
                //else if (ReportName.ToLower() == "trainingnomination")
                //{
                //    IBLCourseCalendar _bl = (IBLCourseCalendar)SHMA.HR.HRIS.TND.BUSINESSLOGIC.TndBLFactory.GetCourseCalendar();
                //    IEnvelope _envelope = new Envelope();
                //    string _str = Request.QueryString["id"];
                //    string[] course_Inst_ID = _str.Split('|');

                //    SHMA.HR.HRIS.UX.tnd.Reports.rptTrainingNomination _rptNomination_source = new SHMA.HR.HRIS.UX.tnd.Reports.rptTrainingNomination();

                //    CourseCalendarSchema _schema = new CourseCalendarSchema();
                //    _schema.CourseID = course_Inst_ID[0] + "'";
                //    _schema.InstitutionID = "'" + course_Inst_ID[1];
                //    _envelope.SetMaster(_schema);

                //    // Starts Main Report
                //    SHMA.HR.HRIS.UX.tnd.DS.DS_TrainingNomination _dsMain = new SHMA.HR.HRIS.UX.tnd.DS.DS_TrainingNomination();
                //    DataSet _dsCourseNomination = (DataSet)_bl.GetCourseNominationByCourseID(_envelope).GetMaster();
                //    _rptNomination_source.SetDataSource(_dsCourseNomination);
                //    // Ends Main Report

                //    // start getting Employee Dataset
                //    IBLEmployee _blEmployee = (IBLEmployee)SHMA.HR.HRIS.SETUP.BUSINESSLOGIC.SetupBLFactory.GetEmployee();
                //    DataSet _dsEmployee = (DataSet)_blEmployee.GetEmployeeByCourseID(_schema.CourseID, _schema.InstitutionID).GetMaster();
                //    _rptNomination_source.Subreports[1].SetDataSource(_dsEmployee.Tables[0]);
                //    // End getting Employee Dataset

                //    // start getting Department Dataset
                //    IBLDepartment _blDepartment = (IBLDepartment)SHMA.HR.HRIS.SETUP.BUSINESSLOGIC.SetupBLFactory.GetDepartment();
                //    DataSet _dsDepartment = (DataSet)_blDepartment.GetDepartmentByCourseID(_schema.CourseID, _schema.InstitutionID).GetMaster();
                //    _rptNomination_source.Subreports[0].SetDataSource(_dsDepartment.Tables[0]);
                //    // End getting Department Dataset

                //    crvReports.ReportSource = _rptNomination_source;
                //    _rptNomination_source.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                //    _rptNomination_source.SummaryInfo.ReportComments = SHMA.HR.HRIS.CORE.Utilities.CurrentUserName;


                //    crvReports.HasToggleGroupTreeButton = false;
                //    crvReports.DataBind();
                //}
                //#endregion
                //#region Hiring request
                ////else if (ReportName.ToLower() == "hiringrequest")
                ////{
                ////    UserService.UserService ws = new SHMA.HR.HRIS.UX.UserService.UserService();

                ////    IBLRequest _bl = (IBLRequest)SHMA.HR.HRIS.RECRUITMENT.BUSINESSLOGIC.RecruitmentBLFactory.GetRequest();
                ////    IEnvelope _envelope = new Envelope();
                ////    string _str = Request.QueryString["id"];
                ////    _envelope.SetMaster(_str);
                ////    DataSet _ds = (DataSet)_bl.GetRequestByID(_envelope).GetMaster();

                ////    // Assignment User Full Name In data Set
                ////    DataSet ds_UserFullName = ws.GetUserByUserId((string)_ds.Tables[0].Rows[0]["JOB_CREATED_BY"]);
                ////    string str = (string)ds_UserFullName.Tables[0].Rows[0]["UST_FULLNAME"];
                ////    _ds.Tables[0].Rows[0]["JOB_CREATED_BY"] = str;
                ////    //APPROVED_BY
                ////    if (_ds.Tables[0].Rows[0]["APPROVED_BY"] != DBNull.Value)
                ////    {
                ////        ds_UserFullName = ws.GetUserByUserId((string)_ds.Tables[0].Rows[0]["APPROVED_BY"]);
                ////        str = (string)ds_UserFullName.Tables[0].Rows[0]["UST_FULLNAME"];
                ////    }
                ////    else
                ////    {
                ////        ds_UserFullName = new DataSet(); str = string.Empty;
                ////    }
                ////    _ds.Tables[0].Rows[0]["APPROVED_BY"] = str;
                ////    // End Assignment
                ////    SHMA.HR.HRIS.UX.rec.Report.rptHiringRequest _hr_source = new SHMA.HR.HRIS.UX.rec.Report.rptHiringRequest();
                ////    _hr_source.SetDataSource(_ds.Tables[0]);
                ////    _hr_source.SummaryInfo.ReportTitle = GetCurrentCompanyByCompanyID(SHMA.HR.HRIS.CORE.Utilities.GetCurrentCompanyID());
                ////    _hr_source.SummaryInfo.ReportComments = SHMA.HR.HRIS.CORE.Utilities.CurrentUserName;
                ////    crvReports.HasToggleGroupTreeButton = false;
                ////    crvReports.ReportSource = _hr_source;
                ////    crvReports.DataBind();
                ////}
                //#endregion
                //#region Correspondence Doc Print
                //else if (ReportName.ToLower() == "CorrespondanceDocPrint".ToLower())
                //{
                //    IBLDocument _bl = SHMA.HR.HRIS.CORESPONDENCE.BUSINESSLOGIC.CorespondenceBLFactory.GetDocument() as IBLDocument;
                //    IEnvelope _envelope = new Envelope();
                //    string _Text = "";

                //    _envelope.SetMaster(Session["DocumentNo"].ToString());
                //    SHMA.HR.HRIS.UX.csp.DataSets.dsAllDocuments _dsAllDocuments = new SHMA.HR.HRIS.UX.csp.DataSets.dsAllDocuments();

                //    if (IsPostBack)
                //    {
                //        return;
                //    }

                //    if (_dsAllDocuments.Tables.Contains("AllDocument"))
                //    {
                //        _dsAllDocuments.Tables.Remove("AllDocument");
                //    }
                //    _dsAllDocuments.Tables.Add((_bl.GetAllDocuments(_envelope).GetMaster() as DataSet).Tables[0].Copy());
                //    _dsAllDocuments.Tables[0].TableName = "AllDocument";

                //    if (_dsAllDocuments.Tables.Count > 0 && _dsAllDocuments.Tables[0].Rows.Count > 0)
                //    {
                //        if (_dsAllDocuments.Tables[0].Rows[0]["DOC_LETTER"] == null ||
                //            _dsAllDocuments.Tables[0].Rows[0]["DOC_LETTER"].ToString() == "")
                //        {
                //            _Text = "";
                //            li_Save.Visible = false;
                //            li_SendEmail.Visible = false;
                //            btn_Save.Visible = false;
                //            imgedit.Visible = false;
                //            btn_SendEmail.Visible = false;
                //            imgMail.Visible = false;
                //            txtReports.EditModes = EditModes.Preview;
                //            return;
                //        }
                //        else
                //        {
                //            if (_dsAllDocuments.Tables[0].Rows[0]["DOC_ID"] == null ||
                //                _dsAllDocuments.Tables[0].Rows[0]["DOC_ID"].ToString().Trim() == "")
                //            {
                //                lblId.Text = "";
                //            }
                //            else
                //            {
                //                lblId.Text = _dsAllDocuments.Tables[0].Rows[0]["DOC_ID"].ToString();
                //            }

                //            if (_dsAllDocuments.Tables[0].Rows[0]["Tmplt_Title"] == null ||
                //                _dsAllDocuments.Tables[0].Rows[0]["Tmplt_Title"].ToString().Trim() == "")
                //            {
                //                lbl_Subject.Text = "";
                //            }
                //            else
                //            {
                //                lbl_Subject.Text = _dsAllDocuments.Tables[0].Rows[0]["Tmplt_Title"].ToString();
                //            }


                //            if (_dsAllDocuments.Tables[0].Rows[0]["DOC_SENDERID"] == null ||
                //                _dsAllDocuments.Tables[0].Rows[0]["DOC_SENDERID"].ToString().Trim() == "")
                //            {
                //                lbl_SenderId.Text = "";
                //            }
                //            else
                //            {
                //                lbl_SenderId.Text = _dsAllDocuments.Tables[0].Rows[0]["DOC_SENDERID"].ToString();
                //            }

                //            if (_dsAllDocuments.Tables[0].Rows[0]["DOC_RECIPIENTID"] == null ||
                //                _dsAllDocuments.Tables[0].Rows[0]["DOC_RECIPIENTID"].ToString().Trim() == "")
                //            {
                //                lbl_RecipientId.Text = "";
                //            }
                //            else
                //            {
                //                lbl_RecipientId.Text = _dsAllDocuments.Tables[0].Rows[0]["DOC_RECIPIENTID"].ToString();
                //            }


                //            _Text = _dsAllDocuments.Tables[0].Rows[0]["DOC_LETTER"].ToString();
                //        }
                //    }

                //    if (Request.QueryString["AllowEdit"] != null && Request.QueryString["AllowEdit"] != "" &&
                //        Request.QueryString["AllowEdit"].ToString().ToLower() == "false".ToLower())
                //    {
                //        li_Save.Visible = false;
                //        li_SendEmail.Visible = true;
                //        btn_Save.Visible = false;
                //        imgedit.Visible = false;
                //        btn_SendEmail.Visible = true;
                //        imgMail.Visible = true;
                //        txtReports.Visible = true;
                //        txtReports.Content = _Text;
                //        txtReports.EditModes = EditModes.Preview;
                //    }
                //    else
                //    {
                //        li_Save.Visible = true;
                //        li_SendEmail.Visible = true;
                //        btn_Save.Visible = true;
                //        imgedit.Visible = true;
                //        btn_SendEmail.Visible = true;
                //        imgMail.Visible = true;
                //        txtReports.Visible = true;
                //        txtReports.Content = _Text;
                //    }

                //    //SHMA.HR.HRIS.UX.csp.Reports.rptAllDocument _rptAllDocument = new SHMA.HR.HRIS.UX.csp.Reports.rptAllDocument();
                //    //_rptAllDocument.SetDataSource(_dsAllDocuments);
                //    //crvReports.HasToggleGroupTreeButton = false;
                //    //crvReports.ReportSource = _rptAllDocument;
                //    //crvReports.DataBind();
                //}
                //#endregion
                //#region Job Position
                //else if (ReportName == "rptjobposition")
                //{
                //    if (Request.QueryString["jdid"] != null)
                //    {
                //        JobProfile_Report(Request.QueryString["jdid"].ToString());
                //    }
                //}
                //#endregion
                //#region Candidate Comparison
                //else if (ReportName == "rptcandidatecomparison")
                //{
                //    if (Request.QueryString["canid"] != null)
                //    {
                //        CandidateComparison_Report(Request.QueryString["canid"].ToString());
                //    }
                //}
                //#endregion
                //#region Hiring Request
                //else if (ReportName == "rpthiringrequest")
                //{
                //    if (Request.QueryString["reqid"] != null)
                //    {
                //        HirinRequest_report(Request.QueryString["reqid"].ToString());
                //    }
                //}
                //#endregion
                //#region Joining Report Of Employee
                //else if (ReportName == "rptjoiningreport")
                //{
                //    if (Request.QueryString["empid"] != null)
                //    {
                //        JoiningReport_Report(Request.QueryString["empid"].ToString());
                //    }
                //}
                //#endregion
                //#region Employee Data report
                //else if (ReportName == "rptempdatareport")
                //{
                //    if (Request.QueryString["empid"] != null)
                //    {
                //        EmployeeData_Report(Request.QueryString["empid"].ToString());
                //    }
                //}

                //#endregion
                //#region Employee Identification report
                //else if (ReportName == "rptempdataIdenreport")
                //{
                //    if (Request.QueryString["empid"] != null)
                //    {
                //        EmployeeIdentification_Report(Request.QueryString["empid"].ToString());
                //    }
                //}
                //#endregion
                //#region Candidate Listing report
                //else if (ReportName == "rptcandidatelisting")
                //{
                //    CandidateListing_Report(Request.QueryString["deptID"].ToString(), Request.QueryString["cityID"].ToString(), Request.QueryString["jdID"].ToString(), Request.QueryString["expectedDate"].ToString(), Request.QueryString["requestStatus"].ToString(), int.Parse(Request.QueryString["requestNO"]));
                //}
                //#endregion
                //#region Request Status report
                //else if (ReportName == "rptjobrequeststatus")
                //{
                //    JobRequestStatus_Report(Request.QueryString["deptID"].ToString(), Request.QueryString["LocID"].ToString(), Request.QueryString["jdID"].ToString(), Request.QueryString["frmDate"].ToString(), Request.QueryString["toDate"].ToString(), Request.QueryString["dateCheckedOn"].ToString());
                //}
                //#endregion
                //#region Training Plan
                //else if (ReportName == "rptTrainingPlan")
                //{
                //    TrainingPlan_Report(Request.QueryString["cri"].ToString(), Request.QueryString["crs"].ToString(), Request.QueryString["bothd"].ToString(), Request.QueryString["bfromd"].ToString(), Request.QueryString["btod"].ToString(), Request.QueryString["fromd"].ToString(), Request.QueryString["todate"].ToString());
                //}
                //#endregion
                //#region Department Wise Training Program
                //else if (ReportName == "rptDptTrainingWise")
                //{
                //    DptWiseTrainingProgram_Report(Request.QueryString["cri"].ToString(), Request.QueryString["crs"].ToString(), Request.QueryString["bothd"].ToString(), Request.QueryString["bfromd"].ToString(), Request.QueryString["btod"].ToString(), Request.QueryString["fromd"].ToString(), Request.QueryString["todate"].ToString(),
                //        Request.QueryString["year"].ToString(), Request.QueryString["cal"].ToString(), Request.QueryString["dptfrom"].ToString(), Request.QueryString["dptto"].ToString(), Request.QueryString["locfrom"].ToString(), Request.QueryString["locto"].ToString(), Request.QueryString["bdpt"].ToString(),
                //        Request.QueryString["bdptfrom"].ToString(), Request.QueryString["bdptto"].ToString(), Request.QueryString["bloc"].ToString(), Request.QueryString["blocfrom"].ToString(), Request.QueryString["blocto"].ToString());
                //}
                //#endregion
                //#region Employee Dynamic Report
                //else if (ReportName == "rptEmpDynamic")
                //{
                //    EmployeeDynamicReport();
                //}
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
