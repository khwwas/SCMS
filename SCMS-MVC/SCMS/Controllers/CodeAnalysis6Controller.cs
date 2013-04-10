using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class CodeAnalysis6Controller : Controller
    {
        //
        // GET: /CodeAnalysis6/
        DALCodeAnalysis6 objDALCodeAnalysis6 = new DALCodeAnalysis6();
        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            return View("CodeAnalysis6");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Location, String Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                    SETUP_CodeAnalysis6 lrow_CodeAnalysis6 = new SETUP_CodeAnalysis6();

                    if (String.IsNullOrEmpty(ps_Code))
                    {
                        if (DALCommon.AutoCodeGeneration("SETUP_CodeAnalysis6") == 1)
                        {
                            ps_Code = DALCommon.GetMaximumCode("SETUP_CodeAnalysis6");
                        }
                    }


                    if (!String.IsNullOrEmpty(ps_Code))
                    {
                        lrow_CodeAnalysis6.CA_Id = ps_Code;
                        lrow_CodeAnalysis6.CA_Code = ps_Code;
                        lrow_CodeAnalysis6.CA_Title = Title;
                        lrow_CodeAnalysis6.Loc_Id = Location;
                        lrow_CodeAnalysis6.CA_Active = 1;

                        li_ReturnValue = objDALCodeAnalysis6.SaveRecord(lrow_CodeAnalysis6);
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
                li_ReturnValue = objDALCodeAnalysis6.DeleteRecordById(_pId);
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
