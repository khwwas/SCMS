using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class CodeAnalysis1Controller : Controller
    {
        //
        // GET: /CodeAnalysis1/
        DALCodeAnalysis1 objDALCodeAnalysis1 = new DALCodeAnalysis1();
        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            return View("CodeAnalysis1");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Location, String Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {

                SETUP_CodeAnalysis1 lrow_CodeAnalysis1 = new SETUP_CodeAnalysis1();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_CodeAnalysis1") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_CodeAnalysis1");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_CodeAnalysis1.CA_Id = ps_Code;
                    lrow_CodeAnalysis1.CA_Code = ps_Code;
                    lrow_CodeAnalysis1.CA_Title = Title;
                    lrow_CodeAnalysis1.Loc_Id = Location;
                    lrow_CodeAnalysis1.CA_Active = 1;

                    li_ReturnValue = objDALCodeAnalysis1.SaveRecord(lrow_CodeAnalysis1);
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
                li_ReturnValue = objDALCodeAnalysis1.DeleteRecordById(_pId);
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
