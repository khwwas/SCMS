using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class BudgetConsoleController : Controller
    {
        //
        // GET: /Budget/
        public ActionResult Index(string p_LocationId)
        {
            if (p_LocationId != null && p_LocationId.Trim() != "")
            {
                ViewData["ddl_Location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", p_LocationId);
            }
            else
            {
                ViewData["ddl_Location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title");
            }

            //if (p_BudgetTypeId != null && p_BudgetTypeId != "")
            //{
            //    ViewData["ddl_BudgetType"] = new SelectList(new DALBudgetType().PopulateData(), "VchrType_Id", "VchrType_Title", p_BudgetTypeId);
            //}
            //else
            //{
            //    ViewData["ddl_BudgetType"] = new SelectList(new DALBudgetType().PopulateData(), "VchrType_Id", "VchrType_Title");
            //}

            if (p_LocationId == null || p_LocationId.Trim() == "")
            {
                ViewData["AllLoc"] = 1;
                ViewData["LocationId"] = "";
            }
            else
            {
                ViewData["AllLoc"] = 0;
                ViewData["LocationId"] = p_LocationId;
            }


            //if (p_BudgetTypeId == null || p_BudgetTypeId.Trim() == "")
            //{
            //    ViewData["AllVchrType"] = 1;
            //    ViewData["BudgetTypeId"] = "";
            //}
            //else
            //{
            //    ViewData["AllVchrType"] = 0;
            //    ViewData["BudgetTypeId"] = p_BudgetTypeId;
            //}

            return View("BudgetConsole");
        }

        //public ActionResult Search(string ps_AllLoc, string ps_Location)
        //{
        //    DALBudgetEntry objDal = new DALBudgetEntry();
        //    int li_AllLocation = -1;//, li_AllBudgetType = -1, li_AllDate = -1;

        //    try
        //    {
        //        if (ps_Location == null || ps_Location == "" || ps_Location == "0")
        //        {
        //            li_AllLocation = 1;
        //        }
        //        else
        //        {
        //            li_AllLocation = 0;
        //        }

        //        //if (ps_BudgetType == null || ps_BudgetType == "" || ps_BudgetType == "0")
        //        //{
        //        //    li_AllBudgetType = 1;
        //        //}
        //        //else
        //        //{
        //        //    li_AllBudgetType = 0;
        //        //}

        //        //if (ps_DateFrom == null || ps_DateFrom == "" || ps_DateTo == null || ps_DateTo == "")
        //        //{
        //        //    li_AllDate = 1;
        //        //}
        //        //else
        //        //{
        //        //    li_AllDate = 0;
        //        //}

        //        //objDal.GetBudgetEntryConsoleData(li_AllLocation, ps_Location, li_AllBudgetType, ps_BudgetType, li_AllDate, ps_DateFrom, ps_DateTo, false);
        //        objDal.GetBudgetEntryConsoleData(li_AllLocation, ps_Location, 1, "", 1, "", "", true);

        //        return PartialView("GridData");
        //    }
        //    catch
        //    {
        //        return PartialView("GridData");
        //    }
        //}

        public ActionResult DeleteBudget_ByBgdtMasId(String ps_BgdtMasId)
        {
            DALBudgetEntry objDal = new DALBudgetEntry();
            String[] ls_Lable = new String[7], ls_Data = new String[7];
            Int32 li_ReturnValue = 0;

            try
            {
                //GL_BgdtMaster BudgetMasterRow = objDal.GetAllMasterRecords().Where(c => c.BgdtMas_Id.Equals(ps_Id)).SingleOrDefault();
                //List<GL_VchrDetail> BudgetDetailList = objDal.GetAllDetailRecords().Where(c => c.BgdtMas_Id.Equals(ps_Id)).ToList();

                li_ReturnValue = objDal.DeleteBudgetDetail_ByBgdtMasId(ps_BgdtMasId);
                li_ReturnValue = objDal.DeleteBudgetMaster_ByBgdtMasId(ps_BgdtMasId);

                ViewData["result"] = li_ReturnValue;

                //IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                //// Delete Audit Log
                //if (li_ReturnValue > 0 && IsAuditTrail == "1")
                //{
                //    DALAuditLog objAuditLog = new DALAuditLog();

                //    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                //    ls_Lable[0] = "Code";
                //    ls_Lable[1] = "Date";
                //    ls_Lable[2] = "Location";
                //    ls_Lable[3] = "Budget Type";
                //    ls_Lable[4] = "Remarks";
                //    ls_Lable[5] = "Status";
                //    ls_Lable[6] = "Entered By";

                //    ls_Data[0] = BudgetMasterRow.BgdtMas_Code;
                //    ls_Data[1] = Convert.ToString(BudgetMasterRow.BgdtMas_Date);
                //    ls_Data[2] = BudgetMasterRow.Loc_Id;
                //    ls_Data[3] = BudgetMasterRow.VchrType_Id;
                //    ls_Data[4] = BudgetMasterRow.BgdtMas_Remarks;
                //    ls_Data[5] = BudgetMasterRow.BgdtMas_Status;
                //    ls_Data[6] = BudgetMasterRow.BgdtMas_EnteredBy;

                //    objAuditLog.SaveRecord(16, ls_UserId, ls_Action, ls_Lable, ls_Data);
                //}
                //// Audit Trail Section End
                ViewData["ddl_Location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title");
                //ViewData["ddl_BudgetType"] = new SelectList(new DALBudgetType().PopulateData(), "VchrType_Id", "VchrType_Title");
                ViewData["AllLoc"] = 1;
                ViewData["LocationId"] = "";
                //ViewData["AllVchrType"] = 1;
                //ViewData["BudgetTypeId"] = "";

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult CancelBudget_ByBgdtMasId(String ps_BgdtMasId)
        {
            DALBudgetEntry objDal = new DALBudgetEntry();
            String[] ls_Lable = new String[7], ls_Data = new String[7];
            Int32 li_ReturnValue = 0;

            try
            {
                li_ReturnValue = objDal.CancelBudget_ByBgdtMasId(ps_BgdtMasId);
                ViewData["result"] = li_ReturnValue;
                ViewData["ddl_Location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title");
                ViewData["AllLoc"] = 1;
                ViewData["LocationId"] = "";

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }
    }
}
