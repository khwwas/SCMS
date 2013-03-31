using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class JobTitleController : Controller
    {
        //
        // GET: /JobTitle/
        DALJobTitle objDalJobTitle = new DALJobTitle();
        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            return View("JobTitle");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Location, String Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_JobTitle lrow_JobTitle = new SETUP_JobTitle();
                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_JobTitle") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_JobTitle");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_JobTitle.JT_Id = ps_Code;
                    lrow_JobTitle.JT_Code= ps_Code;
                    lrow_JobTitle.JT_Title = Title;
                    lrow_JobTitle.Loc_Id = Location;
                    lrow_JobTitle.JT_Active = 1;

                    li_ReturnValue = objDalJobTitle.SaveRecord(lrow_JobTitle);
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
                li_ReturnValue = objDalJobTitle.DeleteRecordById(_pId);
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
