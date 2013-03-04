using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
namespace SCMS.Controllers
{
    public class CityController : Controller
    {
        //
        // GET: /Company/
        DALCity objDalCity = new DALCity();
       
        public ActionResult Index()
        {
            return View("City");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title)
        {
            Int32  li_ReturnValue = 0;

            try
            {
                SETUP_City lrow_City = new SETUP_City();
                
                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_City") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_City");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_City.City_Id = ps_Code;
                    lrow_City.City_Code = ps_Code;
                    lrow_City.City_Title = ps_Title;
                    lrow_City.Cnty_Id = "00001";
                    lrow_City.City_Active = 1;

                    li_ReturnValue = objDalCity.SaveRecord(lrow_City);
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
                li_ReturnValue = objDalCity.DeleteRecordById(_pId);
                ViewData["DeleteResult"] = li_ReturnValue;
                
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

    }
}
