using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
using System.Globalization;

namespace SCMS.Controllers
{
    public class ReportSelectionCriteriaController : Controller
    {
        //
        // GET: /Report Selection Criteria/
        //DALChartOfAccount objDalChartOfAccount = new DALChartOfAccount();

        public ActionResult Index(string ps_ReportName)
        {
            ViewData["ddl_Location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title");
            ViewData["ddl_VoucherTypes"] = new SelectList(new DALVoucherType().PopulateData(), "VchrType_Id", "VchrType_Title");
            ViewData["ddl_AccCodeFrom"] = new SelectList(new DALChartOfAccount().GetChartOfAccountForDropDown(), "ChrtAcc_Id", "ChrtAcc_Title");
            ViewData["ddl_AccCodeTo"] = new SelectList(new DALChartOfAccount().GetChartOfAccountForDropDown(), "ChrtAcc_Id", "ChrtAcc_Title");
            ViewData["ddl_VchrDocFrom"] = new SelectList(new DALVoucherEntry().GetAllMasterRecords(), "VchMas_Id", "VchMas_Code");
            ViewData["ddl_VchrDocTo"] = new SelectList(new DALVoucherEntry().GetAllMasterRecords(), "VchMas_Id", "VchMas_Code");

            ViewData["ReportName"] = ps_ReportName;
            ViewData["CurrentDate"] = DateTime.Now.ToString("MM/dd/yyyy");

            //ViewData["ddl_AccCodeFrom"] = new SelectList(new DALChartOfAccount().PopulateData(), "ChrtAcc_Id", "ChrtAcc_Title", "ddl_AccCodeFrom");
            //ViewData["ddl_AccCodeTo"] = new SelectList(new DALChartOfAccount().PopulateData(), "ChrtAcc_Id", "ChrtAcc_Title", "ddl_AccCodeTo");
            //ViewData["ddl_AccNature"] = new SelectList(new DALAccountNature().PopulateData(), "AccNatr_Id", "AccNatr_Title", "ddl_AccNature");
            return View("ReportSelectionCriteria");
        }


        //public RedirectResult ViewReport(string ps_ReportName)
        //{
        //    //return Redirect("../ViewReport.aspx?ps_ReportName='" + ps_ReportName + "'");
        //}

        public ActionResult ViewReport(string ps_ReportName)
        {
            Response.Redirect("../ViewReport.aspx?ps_ReportName='" + ps_ReportName + "'", false);
            return null;
        }

        #region Setups
        public string SetParam_Setups(String ps_ReportName)
        {
            Reports.ReportParameters.ReportName = ps_ReportName;
            return "OK";
        }

        public string SetParam_ChartOfAccount(String ps_ReportName, int pi_Level)
        {
            Reports.ReportParameters.ReportName = ps_ReportName;
            Reports.ReportParameters.Level = pi_Level;
            return "OK";
        }
        #endregion

        #region Ledger Datail
        public string SetParam_LedgerDetail(String ps_ReportName, String ps_Location, string pi_AllAccCode, string ps_AccCodeFrom, string ps_AccCodeTo,
                                              string pi_AllDate, DateTime pdt_DateFrom, DateTime pdt_DateTo)
        {
            Reports.ReportParameters.ReportName = ps_ReportName;
            Reports.ReportParameters.Location = ps_Location;

            if (pi_AllAccCode == "1")
            {

                Reports.ReportParameters.AllAccCode = 1;
                Reports.ReportParameters.AccCodeFrom = "''";
                Reports.ReportParameters.AccCodeTo = "''";
            }
            else
            {
                Reports.ReportParameters.AllAccCode = 0;
                Reports.ReportParameters.AccCodeFrom = ps_AccCodeFrom;
                Reports.ReportParameters.AccCodeTo = ps_AccCodeTo;
            }

            if (pi_AllDate == "1")
            {
                Reports.ReportParameters.AllDate = 1;
                Reports.ReportParameters.DateFrom = Convert.ToDateTime("01/01/1900");
                Reports.ReportParameters.DateTo = Convert.ToDateTime("01/01/1900");
            }
            else
            {
                Reports.ReportParameters.AllDate = 0;
                Reports.ReportParameters.DateFrom = pdt_DateFrom;
                Reports.ReportParameters.DateTo = pdt_DateTo;
            }

            return "OK";
        }
        #endregion

        //public void SetParameter(String ps_ReportName, String ps_Location)
        //{
        //    Reports.ReportParameters.ReportName = ps_ReportName;

        //    if (ps_ReportName == "ChartOfAccount")
        //    {
        //        Reports.ReportParameters.Location = ps_Location;
        //    }
        //    else if (ps_ReportName == "LedgerDetail")
        //    {
        //        Reports.ReportParameters.Location = ps_Location;
        //    }
        //}

    }
}
