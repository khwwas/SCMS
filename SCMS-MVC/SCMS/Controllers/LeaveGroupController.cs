using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class LeaveGroupController : Controller
    {
        //
        // GET: /LeaveGroup/
        DALLeaveGroup objDalLeaveGroup = new DALLeaveGroup();
        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            return View("LeaveGroup");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Location, String Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_LeaveGroup lrow_LeaveGroup = new SETUP_LeaveGroup();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_LeaveGroup") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_LeaveGroup");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_LeaveGroup.LevGrp_Id = ps_Code;
                    lrow_LeaveGroup.LevGrp_Code = ps_Code;
                    lrow_LeaveGroup.LevGrp_Title = Title;
                    lrow_LeaveGroup.Loc_Id = Location;
                    lrow_LeaveGroup.LevGrp_Active = 1;

                    li_ReturnValue = objDalLeaveGroup.SaveRecord(lrow_LeaveGroup);
                    ViewData["SaveResult"] = li_ReturnValue;
                }

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteRecord(String _pId)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                li_ReturnValue = objDalLeaveGroup.DeleteRecordById(_pId);
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
