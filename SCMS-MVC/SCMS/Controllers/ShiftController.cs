using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer.DB;
using SCMSDataLayer;

namespace SCMS.Controllers
{
    public class ShiftController : Controller
    {
        DALShift objDalShift = new DALShift();
        
        public ActionResult Index()
        {
            return View("Shift");
        }

        public ActionResult SaveRecord(String Code, String Title, String Abbreviation, DateTime StartTime, DateTime EndTime,DateTime BreakStartTime,DateTime BreakEndTime,DateTime BreakDuration,String GraceIn,String GraceEarly)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Shift row_Shift = new SETUP_Shift();

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Shift") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_Shift");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    row_Shift.Shft_Id = Code;
                    row_Shift.Shft_Code = Code;
                    row_Shift.Shft_Title = Title;
                    row_Shift.Shft_Abbreviation = Abbreviation;
                    row_Shift.Shft_Active=1;
                    row_Shift.Shft_SortOrder=1;
                    row_Shift.Shft_StartTime = StartTime;
                    row_Shift.Shft_EndTime = EndTime;
                    row_Shift.Shift_BreakStartTime = BreakStartTime;
                    row_Shift.Shift_BreakEndTime = BreakEndTime;
                    row_Shift.Shift_BreakDuration = BreakDuration;
                    row_Shift.Shift_GraceIn = GraceIn;
                    row_Shift.Shift_GraceEarly = GraceEarly;
                    li_ReturnValue = objDalShift.SaveShift(row_Shift);
                    ViewData["SaveResult"] = li_ReturnValue;
                }

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteRecord(string ShifId)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                li_ReturnValue = objDalShift.DeleteShiftById(ShifId);
                ViewData["SaveResult"] = li_ReturnValue;

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }
    }
}
