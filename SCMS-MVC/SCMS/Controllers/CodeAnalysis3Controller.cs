﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class CodeAnalysis3Controller : Controller
    {
        //
        // GET: /CodeAnalysis3/
        DALCodeAnalysis3 objDALCodeAnalysis3 = new DALCodeAnalysis3();
        public ActionResult Index()
        {
            ViewData["ddl_location"] = new SelectList(new DALLocation().PopulateData(), "Loc_Id", "Loc_Title", "ddl_Location");
            return View("CodeAnalysis3");
        }

        // Insertion
        public ActionResult SaveRecord(String ps_Code, String Location, String Title)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                    SETUP_CodeAnalysis3 lrow_CodeAnalysis3 = new SETUP_CodeAnalysis3();

                    if (String.IsNullOrEmpty(ps_Code))
                    {
                        if (DALCommon.AutoCodeGeneration("SETUP_CodeAnalysis3") == 1)
                        {
                            ps_Code = DALCommon.GetMaximumCode("SETUP_CodeAnalysis3");
                        }
                    }


                    if (!String.IsNullOrEmpty(ps_Code))
                    {
                        lrow_CodeAnalysis3.CA_Id = ps_Code;
                        lrow_CodeAnalysis3.CA_Code = ps_Code;
                        lrow_CodeAnalysis3.CA_Title = Title;
                        lrow_CodeAnalysis3.Loc_Id = Location;
                        lrow_CodeAnalysis3.CA_Active = 1;

                        li_ReturnValue = objDALCodeAnalysis3.SaveRecord(lrow_CodeAnalysis3);
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
                li_ReturnValue = objDALCodeAnalysis3.DeleteRecordById(_pId);
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
