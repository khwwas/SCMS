using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

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
            ViewData["ddl_AccCodeFrom"] = new SelectList(new DALChartOfAccount().GetChartOfAccountForDropDown(), "ChrtAcc_Id", "ChrtAcc_Title");
            ViewData["ddl_AccCodeTo"] = new SelectList(new DALChartOfAccount().GetChartOfAccountForDropDown(), "ChrtAcc_Id", "ChrtAcc_Title");
            ViewData["ReportName"] = ps_ReportName;

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
        public void SetParam_Setups(String ps_ReportName)
        {
            Reports.ReportParameters.ReportName = ps_ReportName;
        }

        public void SetParam_ChartOfAccount(String ps_ReportName, int pi_Level)
        {
            Reports.ReportParameters.ReportName = ps_ReportName;
            Reports.ReportParameters.Level = pi_Level;
        }
        
        #endregion

        public void SetParameter(String ps_ReportName, String ps_Location)
        {
            Reports.ReportParameters.ReportName = ps_ReportName;

            if (ps_ReportName == "ChartOfAccount")
            {
                Reports.ReportParameters.Location = ps_Location;
            }
            else if (ps_ReportName == "LedgerDetail")
            {
                Reports.ReportParameters.Location = ps_Location;
            }
        }

    }
}
