using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Text;

namespace SCMS.Controllers
{
    public class ChartOfAccountModController : Controller
    {
        //
        // GET: /Company/

        public ActionResult Index()
        {
            return View("ChartOfAccountMod");
        }

        public ActionResult SaveRecord(string ps_OldCode, string ps_NewCode)
        {
            DALChartOfAccount objDalChartOfAccount = new DALChartOfAccount();
            Int32 li_ReturnValue = 0;

            try
            {
                li_ReturnValue = objDalChartOfAccount.ReplaceOldCode_WithNewCode(ps_OldCode, ps_NewCode);
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
