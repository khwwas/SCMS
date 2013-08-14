using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

using System.IO;
using System.Web.Script.Serialization;

namespace SCMS.Controllers
{
    public class SupplierTypeController : Controller
    {
        //
        // GET: /Supplier Type/
        DALSupplierType objDalSupplierType = new DALSupplierType();

        public ActionResult Index()
        {
            return View("SupplierType");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_SupplierType lrow_SupplierType = new SETUP_SupplierType();

                String Action = "Add";
                if (!string.IsNullOrEmpty(ps_Code))
                {
                    Action = "Edit";
                }

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_SupplierType") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_SupplierType");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_SupplierType.SuppType_Id = ps_Code;
                    lrow_SupplierType.SuppType_Code = ps_Code;
                    lrow_SupplierType.SuppType_Title = ps_Title;
                    lrow_SupplierType.SuppType_Active = 1;

                    li_ReturnValue = objDalSupplierType.SaveRecord(lrow_SupplierType);
                    ViewData["SaveResult"] = li_ReturnValue;
                }

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 4;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = lrow_SupplierType.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = Action;
                        systemAuditTrail.AdtTrl_EntryId = ps_Code;
                        systemAuditTrail.AdtTrl_DataDump = "SuppType_Id = " + lrow_SupplierType.SuppType_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "SuppType_Code = " + lrow_SupplierType.SuppType_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + lrow_SupplierType.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + lrow_SupplierType.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "SuppType_Title = " + lrow_SupplierType.SuppType_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "SuppType_Active = " + lrow_SupplierType.SuppType_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "SuppType_SortOrder = " + lrow_SupplierType.SuppType_SortOrder + ";";
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

        //public ActionResult SaveRecord(HttpContext context)
        //{
        //    var data = context.Request;
        //    var sr = new StreamReader(data.InputStream);
        //    var stream = sr.ReadToEnd();
        //    var javaScriptSerializer = new JavaScriptSerializer();
        //    var arrayOfStrings = javaScriptSerializer.Deserialize<string[]>(stream);
        //    Int32 li_ReturnValue = 0;

        //    //string json = context.Current.Request.Forms["json"];
        //    //JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    //var urObj = serializer.Deserialize<Type>(json);

        //    try
        //    {
        //        //SETUP_SupplierType lrow_SupplierType = new SETUP_SupplierType();

        //        //if (String.IsNullOrEmpty(ps_Code))
        //        //{
        //        //    if (DALCommon.AutoCodeGeneration("SETUP_SupplierType") == 1)
        //        //    {
        //        //        ps_Code = DALCommon.GetMaximumCode("SETUP_SupplierType");
        //        //    }
        //        //}


        //        //if (!String.IsNullOrEmpty(ps_Code))
        //        //{
        //        //    lrow_SupplierType.SuppType_Id = ps_Code;
        //        //    lrow_SupplierType.SuppType_Code = ps_Code;
        //        //    lrow_SupplierType.SuppType_Title = ps_Title;
        //        //    lrow_SupplierType.SuppType_Active = 1;

        //        //    li_ReturnValue = objDalSupplierType.SaveRecord(lrow_SupplierType);
        //        //    ViewData["SaveResult"] = li_ReturnValue;
        //        //}

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
                SETUP_SupplierType SupplierTypeRow = objDalSupplierType.GetAllSupplierType().Where(c => c.SuppType_Id.Equals(_pId)).SingleOrDefault();

                li_ReturnValue = objDalSupplierType.DeleteRecordById(_pId);
                ViewData["SaveResult"] = li_ReturnValue;

                // Audit Trail Entry Section
                if (li_ReturnValue > 0)
                {
                    string IsAuditTrail = System.Configuration.ConfigurationManager.AppSettings.GetValues(3)[0];
                    if (IsAuditTrail == "1")
                    {
                        SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();
                        DALAuditTrail objAuditTrail = new DALAuditTrail();
                        systemAuditTrail.Scr_Id = 4;
                        systemAuditTrail.User_Id = ((SECURITY_User)Session["user"]).User_Id;
                        systemAuditTrail.Loc_Id = SupplierTypeRow.Loc_Id;
                        systemAuditTrail.AdtTrl_Action = "Delete";
                        systemAuditTrail.AdtTrl_EntryId = _pId;
                        systemAuditTrail.AdtTrl_DataDump = "SuppType_Id = " + SupplierTypeRow.SuppType_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "SuppType_Code = " + SupplierTypeRow.SuppType_Code + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Cmp_Id = " + SupplierTypeRow.Cmp_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "Loc_Id = " + SupplierTypeRow.Loc_Id + ";";
                        systemAuditTrail.AdtTrl_DataDump += "SuppType_Title = " + SupplierTypeRow.SuppType_Title + ";";
                        systemAuditTrail.AdtTrl_DataDump += "SuppType_Active = " + SupplierTypeRow.SuppType_Active + ";";
                        systemAuditTrail.AdtTrl_DataDump += "SuppType_SortOrder = " + SupplierTypeRow.SuppType_SortOrder + ";";
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
