using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer.DB;
using SCMSDataLayer;

namespace SCMS.Controllers
{
    public class ReligionController : Controller
    {
        DALReligion objDalReligion = new DALReligion();

        public ActionResult Index()
        {
            return View("Religion");
        }

        public ActionResult SaveRecord(string Code, string Title, string Abbreviation)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_Religion row_Religion = new SETUP_Religion();

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Religion") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_Religion");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    row_Religion.Rlgn_Id = Code;
                    row_Religion.Rlgn_Code = Code;
                    row_Religion.Rlgn_Title = Title;
                    row_Religion.Rlgn_Abbreviation = Abbreviation;
                    row_Religion.Rlgn_Active = 1;
                    row_Religion.Rlgn_SortOrder = 1;

                    li_ReturnValue = objDalReligion.Save(row_Religion);
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
                li_ReturnValue = objDalReligion.DeleteById(Id);
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
