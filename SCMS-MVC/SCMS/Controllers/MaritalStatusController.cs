using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class MaritalStatusController : Controller
    {
        DALMaritalStatus objDalMaritalStatus = new DALMaritalStatus();

        public ActionResult Index()
        {
            return View("MaritalStatus");
        }

        public ActionResult SaveRecord(string Code, string Title, string Abbreviation)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_MaritalStatus row_MaritalStatus = new SETUP_MaritalStatus();

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_MaritalStatus") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_MaritalStatus");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    row_MaritalStatus.MS_Id = Code;
                    row_MaritalStatus.MS_Code = Code;
                    row_MaritalStatus.MS_Title = Title;
                    row_MaritalStatus.MS_Abbreviation = Abbreviation;
                    row_MaritalStatus.MS_Active = 1;
                    row_MaritalStatus.MS_SortOrder = 1;

                    li_ReturnValue = objDalMaritalStatus.Save(row_MaritalStatus);
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
                li_ReturnValue = objDalMaritalStatus.DeleteById(Id);
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
