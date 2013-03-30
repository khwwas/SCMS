using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class CalendarController : Controller
    {
        //
        // GET: /Calendar/
        DALCalendar objDalCalendar = new DALCalendar();
        
        public ActionResult Index()
        {
            ViewData["ddl_Company"] = new SelectList(new DALCompany().PopulateData(), "Cmp_Id", "Cmp_Title", "ddl_Company");
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            ViewData["ddl_CalenderType"] = new SelectList(new DALCalendarType().GetAllRecords(), "CldrType_Id", "CldrType_Title", "ddl_CalenderType");
            ViewData["CurrentDate"] = DateTime.Now.ToString("MM/dd/yyyy");
            return View("Calendar");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Comapany, String Location, String CalenderType, String Prefix, String Title, DateTime SratrtDate, DateTime EndDate)
        {
            Int32 li_ReturnValue = 0;
            
            // Get Company name show in list
            List<SCMSDataLayer.DB.SETUP_CalendarType> ListCalendarType = new List<SCMSDataLayer.DB.SETUP_CalendarType>();
            ListCalendarType = new SCMSDataLayer.DALCalendarType().GetAllRecords().ToList();

            SCMSDataLayer.DB.SETUP_CalendarType CalendarTypeRow = ListCalendarType.Where(c => c.CldrType_Id.Equals(CalenderType)).SingleOrDefault();

            try
            {
                SETUP_Calendar lrow_Calendar = new SETUP_Calendar();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Calendar") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_Calendar");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_Calendar.Cldr_Id = ps_Code;
                    lrow_Calendar.Cldr_Code = ps_Code;
                    lrow_Calendar.Cmp_Id = Comapany;
                    lrow_Calendar.Loc_Id = Location;
                    lrow_Calendar.CldrType_Id = CalenderType;
                    lrow_Calendar.Cldr_Prefix = Prefix;
                    lrow_Calendar.Cldr_Title = Title;
                    lrow_Calendar.Cldr_DateStart = SratrtDate;
                    lrow_Calendar.Cldr_DateEnd = EndDate;
                    lrow_Calendar.CldrType_Level = CalendarTypeRow.CldrType_Level;
                    lrow_Calendar.Cldr_Active = 1;
                    
                    li_ReturnValue = objDalCalendar.SaveRecord(lrow_Calendar);
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
                li_ReturnValue = objDalCalendar.DeleteRecordById(_pId);
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
