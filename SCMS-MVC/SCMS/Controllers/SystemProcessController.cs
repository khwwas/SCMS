using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
namespace SCMS.Controllers
{
    public class SystemProcessController : Controller
    {
        //
        // GET: /SystemProcess/
        //DALCity objDalCity = new DALCity();

        public ActionResult Index()
        {
            return View("SystemProcess");
        }

        public ActionResult SaveRecord()
        {
            DALSystemProcess objDALSystemProcess = new DALSystemProcess();
            Int32 li_ReturnValue = 0;

            try
            {
                var lst_Data = objDALSystemProcess.GetAllMasterRecords();

                foreach (GL_VchrMaster lstRow in lst_Data)
                {

                    li_ReturnValue = objDALSystemProcess.SaveRecord(lstRow);
                }
            //    if (String.IsNullOrEmpty(ps_Code))
            //    {
            //        if (DALCommon.AutoCodeGeneration("SETUP_City") == 1)
            //        {
            //            ps_Code = DALCommon.GetMaximumCode("SETUP_City");
            //            ls_Action = "Add";
            //        }
            //    }

            //    if (!String.IsNullOrEmpty(ps_Code))
            //    {
            //        lrow_City.City_Id = ps_Code;
            //        lrow_City.City_Code = ps_Code;
            //        lrow_City.City_Title = ps_Title;
            //        lrow_City.Cnty_Id = "00001";
            //        lrow_City.City_Active = 1;

               
                ViewData["SaveResult"] = li_ReturnValue;
           
            
            }
            catch
            {
                return PartialView("GridData");
            }

            return PartialView("");
        }

    }
}
