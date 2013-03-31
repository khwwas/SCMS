using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
        DALDepartment objdalDeparment = new DALDepartment();
        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            return View("Department");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Location, String Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Department lrow_Department = new SETUP_Department();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Department") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_Department");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_Department.Dpt_Id = ps_Code;
                    lrow_Department.Dpt_Code = ps_Code;
                    lrow_Department.Dpt_Title = Title;
                    lrow_Department.Loc_Id = Location;
                    lrow_Department.Dpt_Active = 1;

                    li_ReturnValue = objdalDeparment.SaveRecord(lrow_Department);
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
                li_ReturnValue = objdalDeparment.DeleteRecordById(_pId);
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
