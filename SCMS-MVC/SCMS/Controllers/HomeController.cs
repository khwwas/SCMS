using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCMS.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(int? ModId, string ModDesc, string ModAbbr)
        {
            if (ModId != null)
            {
                SystemParameters.ModuleId = Convert.ToInt32(ModId);
                SystemParameters.ModuleDesc = ModDesc;
                SystemParameters.ModuleAbbr = ModAbbr;
            }
            return View();
        }

    }
}
