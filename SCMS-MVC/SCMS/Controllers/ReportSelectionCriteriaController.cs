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

        public ActionResult Index(string ps_ReportName)
        {
            ViewData["ddl_Location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title");
            //ViewData["ddl_Year"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title");
            ViewData["ddl_VoucherTypes"] = new SelectList(new DALVoucherType().PopulateData(), "VchrType_Id", "VchrType_Title");
            ViewData["ddl_AccCodeFrom"] = new SelectList(new DALChartOfAccount().GetChartOfAccountForDropDown(), "ChrtAcc_Id", "ChrtAcc_Title");
            ViewData["ddl_AccCodeTo"] = new SelectList(new DALChartOfAccount().GetChartOfAccountForDropDown(), "ChrtAcc_Id", "ChrtAcc_Title");
            ViewData["ddl_VchrDocFrom"] = new SelectList(new DALVoucherEntry().GetAllMasterRecords(), "VchMas_Id", "VchMas_Code");
            ViewData["ddl_VchrDocTo"] = new SelectList(new DALVoucherEntry().GetAllMasterRecords(), "VchMas_Id", "VchMas_Code");

            ViewData["ReportName"] = ps_ReportName;
            ViewData["CurrentDate"] = DateTime.Now.ToString("MM/dd/yyyy");

            return View("ReportSelectionCriteria");
        }

        public ActionResult ViewReport(string ps_ReportName)
        {
            Response.Redirect("../ViewReport.aspx?ps_ReportName='" + ps_ReportName + "'", false);
            return null;
        }

        string UserLoginId = "";

        #region Setups
        public string SetParam_Setups(String ps_ReportName)
        {
            Reports.ReportParameters.ReportName = ps_ReportName;
            return "OK";
        }

        public string SetParam_ChartOfAccount(String ps_ReportName, int pi_Level)
        {
            ResetParameters();
            Reports.ReportParameters.ReportName = ps_ReportName;
            Reports.ReportParameters.Level = pi_Level;
            return "OK";
        }
        #endregion

        #region VoucherDocument
        public string SetParam_VoucherDocument(string ps_ReportName, string pi_AllLoc, string ps_Location, string pi_AllVchrType, string ps_VoucherTypes,
                                               string pi_AllDoc, string ps_DocFrom, string ps_DocTo,
                                               string pi_AllDate, DateTime pdt_DateFrom, DateTime pdt_DateTo, string ps_VoucherPrint)
        {
            string ls_Location = "", ls_VoucherTypes="";
            
            ResetParameters();
            Reports.ReportParameters.ReportName = ps_ReportName;
            Reports.ReportParameters.VoucherPrint = ps_VoucherPrint;

            if (pi_AllLoc == "1")
            {
                SECURITY_User user = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
                UserLoginId = user.User_Id;

                List<sp_GetUserLocationsByUserIdResult> UserLocations = new DALUserMenuRights().GetUserLocationsByUserId(UserLoginId).ToList();
                if (UserLocations != null && UserLocations.Count > 0)
                {
                    UserLocations = UserLocations.Where(c => c.SelectedLocation != "0").ToList();
                }

                ls_Location = ConvertStringArrayToString(UserLocations.Select(c => c.Loc_Id).ToArray());
                if (ls_Location == null || ls_Location.Trim() == "")
                {
                    ls_Location = "''";
                }

                Reports.ReportParameters.Location = ls_Location;
            }
            else
            {
                Reports.ReportParameters.Location = ps_Location;
            }

            if (pi_AllVchrType == "1")
            {
                SECURITY_User user = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
                UserLoginId = user.User_Id;

                List<sp_GetUserVoucherTypesByUserIdResult> UserVoucherTypes = new DALUserMenuRights().GetUserVoucherTypesByUserId(UserLoginId).ToList();
                if (UserVoucherTypes != null && UserVoucherTypes.Count > 0)
                {
                    UserVoucherTypes = UserVoucherTypes.Where(c => c.SelectedVoucherType != "0").ToList();
                }

                ls_VoucherTypes =ConvertStringArrayToString(UserVoucherTypes.Select(c => c.VchrType_Id).ToArray());
                if (ls_VoucherTypes == null || ls_VoucherTypes.Trim() == "")
                {
                    ls_VoucherTypes = "''";
                }

                Reports.ReportParameters.VoucherTypes = ls_VoucherTypes;
            }
            else
            {
                Reports.ReportParameters.VoucherTypes = ps_VoucherTypes;
            }

            if (pi_AllDoc == "1")
            {
                Reports.ReportParameters.AllDoc = 1;
                Reports.ReportParameters.DocFrom = "";
                Reports.ReportParameters.DocTo = "";
            }
            else
            {
                Reports.ReportParameters.AllDoc = 0;
                Reports.ReportParameters.DocFrom = ps_DocFrom;
                Reports.ReportParameters.DocTo = ps_DocTo;
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

        #region Ledger Datail
        public string SetParam_LedgerDetail(String ps_ReportName, string pi_AllLoc, String ps_Location, string pi_AllAccCode, string ps_AccCodeFrom, string ps_AccCodeTo,
                                              string pi_AllDate, DateTime pdt_DateFrom, DateTime pdt_DateTo)
        {
            string ls_Location = "";

            ResetParameters();
            Reports.ReportParameters.ReportName = ps_ReportName;

            if (pi_AllLoc == "1")
            {
                SECURITY_User user = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
                UserLoginId = user.User_Id;

                List<sp_GetUserLocationsByUserIdResult> UserLocations = new DALUserMenuRights().GetUserLocationsByUserId(UserLoginId).ToList();
                if (UserLocations != null && UserLocations.Count > 0)
                {
                    UserLocations = UserLocations.Where(c => c.SelectedLocation != "0").ToList();
                }

                ls_Location = ConvertStringArrayToString(UserLocations.Select(c => c.Loc_Id).ToArray());
                if (ls_Location == null || ls_Location.Trim() == "")
                {
                    ls_Location = "''";
                }

                Reports.ReportParameters.Location = ls_Location;
            }
            else
            {
                Reports.ReportParameters.Location = ps_Location;
            }

            if (pi_AllAccCode == "1")
            {

                Reports.ReportParameters.AllAccCode = 1;
                Reports.ReportParameters.AccCodeFrom = "";
                Reports.ReportParameters.AccCodeTo = "";
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

        #region Trial Balance
        public string SetParam_TrialBalance(String ps_ReportName, string pi_AllLoc, String ps_Location, string pi_AllAccCode, string ps_AccCodeFrom, string ps_AccCodeTo,
                                              string pi_AllDate, DateTime pdt_DateFrom, DateTime pdt_DateTo, string ps_Level, string ps_TrialActivity)
        {
            string ls_Location = "";

            ResetParameters();
            Reports.ReportParameters.ReportName = ps_ReportName;
            Reports.ReportParameters.Level = Convert.ToInt32(ps_Level);
            Reports.ReportParameters.TrialActivity = ps_TrialActivity;

            if (pi_AllLoc == "1")
            {
                SECURITY_User user = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
                UserLoginId = user.User_Id;

                List<sp_GetUserLocationsByUserIdResult> UserLocations = new DALUserMenuRights().GetUserLocationsByUserId(UserLoginId).ToList();
                if (UserLocations != null && UserLocations.Count > 0)
                {
                    UserLocations = UserLocations.Where(c => c.SelectedLocation != "0").ToList();
                }

                ls_Location = ConvertStringArrayToString(UserLocations.Select(c => c.Loc_Id).ToArray());
                if (ls_Location == null || ls_Location.Trim() == "")
                {
                    ls_Location = "''";
                }

                Reports.ReportParameters.Location = ls_Location;
            }
            else
            {
                Reports.ReportParameters.Location = ps_Location;
            }

            if (pi_AllAccCode == "1")
            {

                Reports.ReportParameters.AllAccCode = 1;
                Reports.ReportParameters.AccCodeFrom = "";
                Reports.ReportParameters.AccCodeTo = "";
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
                Reports.ReportParameters.DateFrom = pdt_DateFrom;
                Reports.ReportParameters.DateTo = pdt_DateTo;
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

        #region Income Statement
        public string SetParam_IncomeStatement(string ps_ReportName, string pi_AllLoc, string ps_Location, Int32 pi_Level, Int32 pi_Year)
        {
            string ls_Location = "";

            ResetParameters();
            Reports.ReportParameters.ReportName = ps_ReportName;
            Reports.ReportParameters.Level = pi_Level;
            Reports.ReportParameters.Year = pi_Year;

            if (pi_AllLoc == "1")
            {
                SECURITY_User user = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
                UserLoginId = user.User_Id;

                List<sp_GetUserLocationsByUserIdResult> UserLocations = new DALUserMenuRights().GetUserLocationsByUserId(UserLoginId).ToList();
                if (UserLocations != null && UserLocations.Count > 0)
                {
                    UserLocations = UserLocations.Where(c => c.SelectedLocation != "0").ToList();
                }

                ls_Location = ConvertStringArrayToString(UserLocations.Select(c => c.Loc_Id).ToArray());
                if (ls_Location == null || ls_Location.Trim() == "")
                {
                    ls_Location = "''";
                }

                Reports.ReportParameters.Location = ls_Location;
            }
            else
            {
                Reports.ReportParameters.Location = ps_Location;
            }

            return "OK";
        }
        #endregion

        #region BalanceSheet
        public string SetParam_BalanceSheet(string ps_ReportName, string pi_AllLoc, string ps_Location, Int32 pi_Level, Int32 pi_Year)
        {
            string ls_Location = "";

            ResetParameters();
            Reports.ReportParameters.ReportName = ps_ReportName;
            Reports.ReportParameters.Level = pi_Level;
            Reports.ReportParameters.Year = pi_Year;

            if (pi_AllLoc == "1")
            {
                SECURITY_User user = (SECURITY_User)System.Web.HttpContext.Current.Session["user"];
                UserLoginId = user.User_Id;

                List<sp_GetUserLocationsByUserIdResult> UserLocations = new DALUserMenuRights().GetUserLocationsByUserId(UserLoginId).ToList();
                if (UserLocations != null && UserLocations.Count > 0)
                {
                    UserLocations = UserLocations.Where(c => c.SelectedLocation != "0").ToList();
                }

                ls_Location = ConvertStringArrayToString(UserLocations.Select(c => c.Loc_Id).ToArray());
                if (ls_Location == null || ls_Location.Trim() == "")
                {
                    ls_Location = "''";
                }

                Reports.ReportParameters.Location = ls_Location;
            }
            else
            {
                Reports.ReportParameters.Location = ps_Location;
            }

            return "OK";
        }
        #endregion

        #region Functions
        void ResetParameters()
        {
            Reports.ReportParameters.Location = null;
            Reports.ReportParameters.VoucherTypes = null;
            Reports.ReportParameters.Level = 0;
            Reports.ReportParameters.AllAccCode = 1;
            Reports.ReportParameters.AccCodeFrom = "";
            Reports.ReportParameters.AccCodeTo = "";
            Reports.ReportParameters.AllDate = 1;
            Reports.ReportParameters.DateFrom = Convert.ToDateTime("01/01/1900");
            Reports.ReportParameters.DateTo = Convert.ToDateTime("01/01/1900");


        }
        static string ConvertStringArrayToString(string[] ps_Array)
        {
            string ls_Array = "";
            int li_Index = 0;

            try
            {
                for (li_Index = 0; li_Index <= ps_Array.Length - 1; li_Index++)
                {
                    if (ls_Array == null || ls_Array.Trim() == "")
                    {
                        ls_Array += "'" + ps_Array[li_Index] + "'";
                    }
                    else
                    {
                        ls_Array += ", '" + ps_Array[li_Index] + "'";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return ls_Array;
        }

        #endregion

    }
}
