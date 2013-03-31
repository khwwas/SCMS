using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class FunctionalAreaController : Controller
    {

        DALFunctionalArea objDALFunctionalArea = new DALFunctionalArea();

        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            return View("FunctionalArea");
        }

        
        public ActionResult SaveFunctionalArea(string Code, string Title, string Location)
        {
            Int32 FunctionalAreaId = 0;

            try
            {
                SETUP_FunctionalArea setupFunctionalAreaRow = new SETUP_FunctionalArea();

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_FunctionalArea") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_FunctionalArea");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    setupFunctionalAreaRow.FA_Id = Code;
                    setupFunctionalAreaRow.FA_Code = Code;
                    setupFunctionalAreaRow.FA_Title = Title;
                    setupFunctionalAreaRow.Loc_Id = Location;
                    setupFunctionalAreaRow.FA_SortOrder = 1;
                    setupFunctionalAreaRow.FA_Active = 1;

                    FunctionalAreaId = objDALFunctionalArea.SaveFunctionalArea(setupFunctionalAreaRow);
                    ViewData["SaveResult"] = FunctionalAreaId;
                }
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }


        public ActionResult DeleteFunctionalArea(String FunctionalAreaId)
        {
            Int32 li_ReturnValue = 0;
            try
            {
                li_ReturnValue = objDALFunctionalArea.DeleteFunctionalAreaById(FunctionalAreaId);
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
