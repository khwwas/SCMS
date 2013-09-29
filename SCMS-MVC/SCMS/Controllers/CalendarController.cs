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
            //ViewData["ddl_Company"] = new SelectList(new DALCompany().PopulateData(), "Cmp_Id", "Cmp_Title", "ddl_Company");
            //ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            ViewData["ddl_CalenderType"] = new SelectList(new DALCalendarType().GetAllRecords(), "CldrType_Id", "CldrType_Title", "ddl_CalenderType");
            ViewData["CurrentDate"] = DateTime.Now.ToString("MM/dd/yyyy");
            return View("Calendar");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String CalenderType, String Prefix, String Title, DateTime SratrtDate, DateTime EndDate)
        {
            SETUP_Calendar lrow_Calendar = new SETUP_Calendar();
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[6], ls_Data = new String[6];
            Int32 li_ReturnValue = 0;

            // Get Company name show in list
            List<SCMSDataLayer.DB.SETUP_CalendarType> ListCalendarType = new List<SCMSDataLayer.DB.SETUP_CalendarType>();
            ListCalendarType = new SCMSDataLayer.DALCalendarType().GetAllRecords().ToList();
            SCMSDataLayer.DB.SETUP_CalendarType CalendarTypeRow = ListCalendarType.Where(c => c.CldrType_Id.Equals(CalenderType)).SingleOrDefault();

            try
            {
                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Calendar") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_Calendar");
                        ls_Action = "Add";
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_Calendar.Cldr_Id = ps_Code;
                    lrow_Calendar.Cldr_Code = ps_Code;
                    lrow_Calendar.CldrType_Id = CalenderType;
                    lrow_Calendar.Cldr_Prefix = Prefix;
                    lrow_Calendar.Cldr_Title = Title;
                    lrow_Calendar.Cldr_DateStart = SratrtDate;
                    lrow_Calendar.Cldr_DateEnd = EndDate;
                    lrow_Calendar.CldrType_Level = CalendarTypeRow.CldrType_Level;
                    lrow_Calendar.Cldr_Active = 1;

                    li_ReturnValue = objDalCalendar.SaveRecord(lrow_Calendar);
                    ViewData["SaveResult"] = li_ReturnValue;

                    IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0 && IsAuditTrail == "1")
                    {
                        DALAuditLog objAuditLog = new DALAuditLog();

                        ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                        ls_Lable[0] = "Code";
                        ls_Lable[1] = "Title";
                        ls_Lable[2] = "Prefix";
                        ls_Lable[3] = "Calendar Type";
                        ls_Lable[4] = "Start Date";
                        ls_Lable[5] = "End Date";

                        ls_Data[0] = ps_Code;
                        ls_Data[1] = Title;
                        ls_Data[2] = Prefix;
                        ls_Data[3] = CalenderType;
                        ls_Data[4] = SratrtDate.ToString("dd/MM/yyyy");
                        ls_Data[5] = EndDate.ToString("dd/MM/yyyy");

                        objAuditLog.SaveRecord(14, ls_UserId, ls_Action, ls_Lable, ls_Data);
                    }
                    // Audit Trail Section End
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
            String ls_Action = "Delete", IsAuditTrail = "", ls_UserId = "";
            String[] ls_Lable = new String[6], ls_Data = new String[6];
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Calendar CalendarRow = objDalCalendar.GetAllRecords().Where(c => c.Cldr_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalCalendar.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues("IsAuditTrail")[0];

                // Delete Audit Log
                if (li_ReturnValue > 0 && IsAuditTrail == "1")
                {
                    DALAuditLog objAuditLog = new DALAuditLog();

                    ls_UserId = ((SECURITY_User)Session["user"]).User_Id;
                    ls_Lable[0] = "Code";
                    ls_Lable[1] = "Title";
                    ls_Lable[2] = "Prefix";
                    ls_Lable[3] = "Calendar Type";
                    ls_Lable[4] = "Start Date";
                    ls_Lable[5] = "End Date";

                    ls_Data[0] = CalendarRow.Cldr_Code;
                    ls_Data[1] = CalendarRow.Cldr_Title;
                    ls_Data[2] = CalendarRow.Cldr_Prefix;
                    ls_Data[3] = CalendarRow.CldrType_Id;
                    ls_Data[4] = Convert.ToString(CalendarRow.Cldr_DateStart);
                    ls_Data[5] = Convert.ToString(CalendarRow.Cldr_DateEnd);

                    objAuditLog.SaveRecord(14, ls_UserId, ls_Action, ls_Lable, ls_Data);
                }
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

    }
}
