using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class NationalityController : Controller
    {
        DALNationality objDalNationality = new DALNationality();

        public ActionResult Index()
        {
            return View("Nationality");
        }

        public ActionResult SaveRecord(string Code, string Title, string Abbreviation)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Nationality row_Nationality = new SETUP_Nationality();

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Nationality") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_Nationality");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    row_Nationality.Natn_Id = Code;
                    row_Nationality.Natn_Code = Code;
                    row_Nationality.Natn_Title = Title;
                    row_Nationality.Natn_Abbreviation = Abbreviation;
                    row_Nationality.Natn_Active = 1;
                    row_Nationality.Natn_SortOrder = 1;

                    li_ReturnValue = objDalNationality.Save(row_Nationality);
                    ViewData["SaveResult"] = li_ReturnValue;
                }

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteRecord(string Id)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                li_ReturnValue = objDalNationality.DeleteById(Id);
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
