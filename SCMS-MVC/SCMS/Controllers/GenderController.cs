using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class GenderController : Controller
    {
        DALGender objDalGender = new DALGender();

        public ActionResult Index()
        {
            return View("Gender");
        }

        public ActionResult SaveRecord(string Code, string Title, string Abbreviation)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Gender row_Gender = new SETUP_Gender();

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Gender") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_Gender");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    row_Gender.Gndr_Id = Code;
                    row_Gender.Gndr_Code = Code;
                    row_Gender.Gndr_Title = Title;
                    row_Gender.Gndr_Abbreviation = Abbreviation;
                    row_Gender.Gndr_Active = 1;
                    row_Gender.Gndr_SortOrder = 1;

                    li_ReturnValue = objDalGender.Save(row_Gender);
                    ViewData["SaveResult"] = li_ReturnValue;
                }

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteRecord(string GId)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                li_ReturnValue = objDalGender.DeleteById(GId);
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
