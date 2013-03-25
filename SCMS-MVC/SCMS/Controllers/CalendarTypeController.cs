using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class CalendarTypeController : Controller
    {
        //
        // GET: /CalendarType/
        DALCalendarType objDalCalendarType = new DALCalendarType();
        public ActionResult Index()
        {
            ViewData["ddl_Company"] = new SelectList(new DALCompany().PopulateData(), "Cmp_Id", "Cmp_Title", "ddl_Company");
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            return View("CalendarType");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Comapany, String Location, String Title, int Level)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_CalendarType lrow_CalendarType = new SETUP_CalendarType();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_CalendarType") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_CalendarType");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_CalendarType.CldrType_Id = ps_Code;
                    lrow_CalendarType.CldrType_Code = ps_Code;
                    lrow_CalendarType.CldrType_Title = Title;
                    lrow_CalendarType.Cmp_Id = Comapany;
                    lrow_CalendarType.Loc_Id = Location;
                    lrow_CalendarType.CldrType_Level = Level;
                    lrow_CalendarType.CldrType_Active = 1;

                    li_ReturnValue = objDalCalendarType.SaveRecord(lrow_CalendarType);
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
                li_ReturnValue = objDalCalendarType.DeleteRecordById(_pId);
                ViewData["DeleteResult"] = li_ReturnValue;

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

    }
}
