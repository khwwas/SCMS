using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class JobPositionController : Controller
    {
        DALJobPosition objDALJobPosition = new DALJobPosition();

        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            ViewData["ddl_departement"] = null; //new SelectList(null, "Dpt_Id", "Dpt_Title", "ddl_departement");
            ViewData["ddl_functionalarea"] = new SelectList(new DALFunctionalArea().PopulateData(), "FA_Id", "FA_Title", "ddl_functionalarea");
            ViewData["ddl_jobtitle"] = null; //new SelectList(null, "JT_Id", "JT_Title", "ddl_jobtitle");

            return View("JobPosition");
        }


        public ActionResult SaveJobPosition(string Code, string Title, string Remarks,string location, string Job, string Department,string functionalarea)
        {
            Int32 JobPositionId = 0;

            try
            {
                SETUP_JobPosition setupJobPositionRow = new SETUP_JobPosition();

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_JobPosition") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_JobPosition");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    setupJobPositionRow.JP_Id = Code;
                    setupJobPositionRow.JP_Code = Code;
                    setupJobPositionRow.JP_Title = Title;
                    setupJobPositionRow.JP_SortOrder = 1;
                    setupJobPositionRow.JP_Active = 1;
                    setupJobPositionRow.JP_Remarks = Remarks;
                    // setupJobPositionRow.JT_Id = Job;
                   // setupJobPositionRow.Dpt_Id = Department;
                    setupJobPositionRow.FA_Id = functionalarea;
                    setupJobPositionRow.Loc_Id = location;

                    JobPositionId = objDALJobPosition.SaveJobPosition(setupJobPositionRow);
                    ViewData["SaveResult"] = JobPositionId;
                }
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }


        public ActionResult DeleteJobPosition(String JobPosition)
        {
            Int32 li_ReturnValue = 0;
            try
            {
                li_ReturnValue = objDALJobPosition.DeleteJobPositionById(JobPosition);
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
