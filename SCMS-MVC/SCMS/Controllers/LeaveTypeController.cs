using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class LeaveTypeController : Controller
    {

        DALLeaveType objDALLeaveType = new DALLeaveType();
        
        public ActionResult Index()
        {
            
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            return View("LeaveType");
        }

        public ActionResult SaveRecord(string Code, string Location, string Title, string Abbreviation, string Count)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_LeaveType row_LeaveType = new SETUP_LeaveType();

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_LeaveType") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_LeaveType");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    row_LeaveType.LevTyp_Id = Code;
                    row_LeaveType.LevTyp_Code = Code;
                    row_LeaveType.LevTyp_Title = Title;
                    row_LeaveType.LevTyp_Active = 1;
                    row_LeaveType.LevTyp_SortOrder = 1;
                    row_LeaveType.LevTyp_Count = Count;
                    row_LeaveType.LevTyp_Abbreviation = Abbreviation;
                    row_LeaveType.Loc_Id = Location;

                    li_ReturnValue = objDALLeaveType.SaveRecord(row_LeaveType);
                    ViewData["SaveResult"] = li_ReturnValue;
                }

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteRecord(string leaveTypeId)
        {
            Int32 li_ReturnValue = 0;
            try
            {
                li_ReturnValue = objDALLeaveType.DeleteRecordById(leaveTypeId);
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
