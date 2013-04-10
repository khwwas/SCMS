using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class CodeAnalysis2Controller : Controller
    {
        //
        // GET: /CodeAnalysis2/
        DALCodeAnalysis2 objDALCodeAnalysis2 = new DALCodeAnalysis2();
        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            return View("CodeAnalysis2");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Location, String Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                    SETUP_CodeAnalysis2 lrow_CodeAnalysis2 = new SETUP_CodeAnalysis2();

                    if (String.IsNullOrEmpty(ps_Code))
                    {
                        if (DALCommon.AutoCodeGeneration("SETUP_CodeAnalysis2") == 1)
                        {
                            ps_Code = DALCommon.GetMaximumCode("SETUP_CodeAnalysis2");
                        }
                    }


                    if (!String.IsNullOrEmpty(ps_Code))
                    {
                        lrow_CodeAnalysis2.CA_Id = ps_Code;
                        lrow_CodeAnalysis2.CA_Code = ps_Code;
                        lrow_CodeAnalysis2.CA_Title = Title;
                        lrow_CodeAnalysis2.Loc_Id = Location;
                        lrow_CodeAnalysis2.CA_Active = 1;

                        li_ReturnValue = objDALCodeAnalysis2.SaveRecord(lrow_CodeAnalysis2);
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
                li_ReturnValue = objDALCodeAnalysis2.DeleteRecordById(_pId);
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
