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
            Int32 li_ReturnValue = 0;

            // Get Company name show in list
            List<SCMSDataLayer.DB.SETUP_CalendarType> ListCalendarType = new List<SCMSDataLayer.DB.SETUP_CalendarType>();
            ListCalendarType = new SCMSDataLayer.DALCalendarType().GetAllRecords().ToList();

            SCMSDataLayer.DB.SETUP_CalendarType CalendarTypeRow = ListCalendarType.Where(c => c.CldrType_Id.Equals(CalenderType)).SingleOrDefault();

            try
            {
                SETUP_Calendar lrow_Calendar = new SETUP_Calendar();

                String Action = "Add";
                if (!string.IsNullOrEmpty(ps_Code))
                {
                    Action = "Edit";
                }

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
                    //lrow_Calendar.Cmp_Id = Comapany;
                    //lrow_Calendar.Loc_Id = Location;
                    lrow_Calendar.CldrType_Id = CalenderType;
                    lrow_Calendar.Cldr_Prefix = Prefix;
                    lrow_Calendar.Cldr_Title = Title;
                    lrow_Calendar.Cldr_DateStart = SratrtDate;
                    lrow_Calendar.Cldr_DateEnd = EndDate;
                    lrow_Calendar.CldrType_Level = CalendarTypeRow.CldrType_Level;
                    lrow_Calendar.Cldr_Active = 1;

                    li_ReturnValue = objDalCalendar.SaveRecord(lrow_Calendar);
                    ViewData["SaveResult"] = li_ReturnValue;

                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0)
                    {
                        string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                        if (IsAuditTrail == "1")
                        {
                            SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                            DALAuditTrail objAuditTrail = new DALAuditTrail();
                            systemAuditTrail.Scr_Id = 14;
                            systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                            systemAuditTrail.Loc_Id = lrow_Calendar.Loc_Id;
                            systemAuditTrail.AdtTrl_Action = Action;
                            systemAuditTrail.AdtTrl_EntryId = ps_Code;
                            systemAuditTrail.AdtTrl_DataDump = "Cldr_Id = " + lrow_Calendar.Cldr_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cldr_ParentRefId = " + lrow_Calendar.Cldr_ParentRefId + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cldr_Code = " + lrow_Calendar.Cldr_Code + ";";
                            //systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + lrow_Calendar.Cmp_Id + ";";
                            //systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + lrow_Calendar.Loc_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "CldrType_Id = " + lrow_Calendar.CldrType_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cldr_Title = " + lrow_Calendar.Cldr_Title + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cldr_DateStart = " + lrow_Calendar.Cldr_DateStart + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cldr_DateEnd = " + lrow_Calendar.Cldr_DateEnd + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cldr_Active = " + lrow_Calendar.Cldr_Active + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cldr_SortOrder = " + lrow_Calendar.Cldr_SortOrder + ";";
                            systemAuditTrail.AdtTrl_DataDump += "CldrType_Level = " + lrow_Calendar.CldrType_Level + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cldr_Prefix = " + lrow_Calendar.Cldr_Prefix + ";";
                            systemAuditTrail.AdtTrl_Date = DateTime.Now;
                            objAuditTrail.SaveRecord(systemAuditTrail);
                        }
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
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Calendar CalendarRow = objDalCalendar.GetAllRecords().Where(c => c.Cldr_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalCalendar.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 14;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = CalendarRow.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = _pId;
                        systemAuditTrail.AdtTrl_DataDump = "Cldr_Id = " + CalendarRow.Cldr_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cldr_ParentRefId = " + CalendarRow.Cldr_ParentRefId + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cldr_Code = " + CalendarRow.Cldr_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + CalendarRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + CalendarRow.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "CldrType_Id = " + CalendarRow.CldrType_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cldr_Title = " + CalendarRow.Cldr_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cldr_DateStart = " + CalendarRow.Cldr_DateStart + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cldr_DateEnd = " + CalendarRow.Cldr_DateEnd + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cldr_Active = " + CalendarRow.Cldr_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cldr_SortOrder = " + CalendarRow.Cldr_SortOrder + ";";
                        systemAuditTrail.AdtTrl_DataDump += "CldrType_Level = " + CalendarRow.CldrType_Level + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cldr_Prefix = " + CalendarRow.Cldr_Prefix + ";";
                        systemAuditTrail.AdtTrl_Date = DateTime.Now;
                        objAuditTrail.SaveRecord(systemAuditTrail);
                    }
                }
                // Audit Trail Section End

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

    }
}
