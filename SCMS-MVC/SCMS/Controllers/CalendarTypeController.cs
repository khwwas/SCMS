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
        public ActionResult SaveRecord(String ps_Code, String Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_CalendarType lrow_CalendarType = new SETUP_CalendarType();

                String Action = "Add";
                if (!string.IsNullOrEmpty(ps_Code))
                {
                    Action = "Edit";
                }

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
                    //lrow_CalendarType.Cmp_Id = Comapany;
                    //lrow_CalendarType.Loc_Id = Location;
                    //lrow_CalendarType.CldrType_Level = Level;
                    lrow_CalendarType.CldrType_Active = 1;

                    li_ReturnValue = objDalCalendarType.SaveRecord(lrow_CalendarType);
                    ViewData["SaveResult"] = li_ReturnValue;

                    // Audit Trail Entry Section
                    if (li_ReturnValue > 0)
                    {
                        string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                        if (IsAuditTrail == "1")
                        {
                            SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                            DALAuditTrail objAuditTrail = new DALAuditTrail();
                            systemAuditTrail.Scr_Id = 13;
                            systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                            systemAuditTrail.Loc_Id = lrow_CalendarType.Loc_Id;
                            systemAuditTrail.AdtTrl_Action = Action;
                            systemAuditTrail.AdtTrl_EntryId = ps_Code;
                            systemAuditTrail.AdtTrl_DataDump = "CldrType_Id = " + lrow_CalendarType.CldrType_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "CldrType_Code = " + lrow_CalendarType.CldrType_Code + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + lrow_CalendarType.Cmp_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + lrow_CalendarType.Loc_Id + ";";
                            systemAuditTrail.AdtTrl_DataDump += "CldrType_Title = " + lrow_CalendarType.CldrType_Title + ";";
                            systemAuditTrail.AdtTrl_DataDump += "CldrType_Active = " + lrow_CalendarType.CldrType_Active + ";";
                            systemAuditTrail.AdtTrl_DataDump += "CldrType_SortOrder = " + lrow_CalendarType.CldrType_SortOrder + ";";
                            systemAuditTrail.AdtTrl_DataDump += "CldrType_Level = " + lrow_CalendarType.CldrType_Level + ";";
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

        //public ActionResult SaveRecord(String ps_Code, String Comapany, String Location, String Title, int Level)
        //{
        //    Int32 li_ReturnValue = 0;

        //    try
        //    {
        //        SETUP_CalendarType lrow_CalendarType = new SETUP_CalendarType();

        //        if (String.IsNullOrEmpty(ps_Code))
        //        {
        //            if (DALCommon.AutoCodeGeneration("SETUP_CalendarType") == 1)
        //            {
        //                ps_Code = DALCommon.GetMaximumCode("SETUP_CalendarType");
        //            }
        //        }


        //        if (!String.IsNullOrEmpty(ps_Code))
        //        {
        //            lrow_CalendarType.CldrType_Id = ps_Code;
        //            lrow_CalendarType.CldrType_Code = ps_Code;
        //            lrow_CalendarType.CldrType_Title = Title;
        //            lrow_CalendarType.Cmp_Id = Comapany;
        //            lrow_CalendarType.Loc_Id = Location;
        //            lrow_CalendarType.CldrType_Level = Level;
        //            lrow_CalendarType.CldrType_Active = 1;

        //            li_ReturnValue = objDalCalendarType.SaveRecord(lrow_CalendarType);
        //            ViewData["SaveResult"] = li_ReturnValue;
        //        }

        //        return PartialView("GridData");
        //    }
        //    catch
        //    {
        //        return PartialView("GridData");
        //    }
        //}

        public ActionResult DeleteRecord(String _pId)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_CalendarType CalendarTypeRow = objDalCalendarType.GetAllRecords().Where(c => c.CldrType_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalCalendarType.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 13;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = CalendarTypeRow.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = _pId;
                        systemAuditTrail.AdtTrl_DataDump = "CldrType_Id = " + CalendarTypeRow.CldrType_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "CldrType_Code = " + CalendarTypeRow.CldrType_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + CalendarTypeRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + CalendarTypeRow.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "CldrType_Title = " + CalendarTypeRow.CldrType_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "CldrType_Active = " + CalendarTypeRow.CldrType_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "CldrType_SortOrder = " + CalendarTypeRow.CldrType_SortOrder + ";";
                        systemAuditTrail.AdtTrl_DataDump += "CldrType_Level = " + CalendarTypeRow.CldrType_Level + ";";
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
