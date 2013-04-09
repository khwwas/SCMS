using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer.DB;
using SCMSDataLayer;

namespace SCMS.Controllers
{
    public class EmployeeTypeController : Controller
    {
        DALEmployeeType objDalEmployeeType = new DALEmployeeType();

        public ActionResult Index()
        {
            return View("EmployeeType");
        }

        public ActionResult SaveRecord(string Code, string Title, string Abbreviation)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_EmployeeType row_EmployeeType = new SETUP_EmployeeType();

                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_EmployeeType") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_EmployeeType");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    row_EmployeeType.EmpTyp_Id = Code;
                    row_EmployeeType.EmpTyp_Code = Code;
                    row_EmployeeType.EmpTyp_Title = Title;
                    row_EmployeeType.EmpTyp_Abbreviation = Abbreviation;
                    row_EmployeeType.EmpTyp_Active = 1;
                    row_EmployeeType.EmpTyp_SortOrder = 1;
                    
                    li_ReturnValue = objDalEmployeeType.Save(row_EmployeeType);
                    ViewData["SaveResult"] = li_ReturnValue;
                }

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteRecord(string EmpTypId)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                li_ReturnValue = objDalEmployeeType.DeleteById(EmpTypId);
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
