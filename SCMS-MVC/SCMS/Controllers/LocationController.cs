using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class LocationController : Controller
    {
        //
        // GET: /Location/
        DALLocation objDalLocation = new DALLocation();

        public ActionResult Index()
        {
            ViewData["ddl_Company"] = new SelectList(new DALCompany().PopulateData(), "Cmp_Id", "Cmp_Title", "ddl_Company");
            return View("Location");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title, String ps_CmpId)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Location lrow_Location = new SETUP_Location();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Location") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_Location");
                    }
                }

                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_Location.Loc_Id = ps_Code;
                    lrow_Location.Loc_Code = ps_Code;
                    lrow_Location.Loc_Title = ps_Title;
                    lrow_Location.Cmp_Id = ps_CmpId;
                    lrow_Location.Loc_Active = 1;

                    li_ReturnValue = objDalLocation.SaveRecord(lrow_Location);
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
                li_ReturnValue = objDalLocation.DeleteRecordById(_pId);
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
