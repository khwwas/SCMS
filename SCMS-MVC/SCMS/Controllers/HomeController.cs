﻿using System;
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

        public ActionResult Index(int? ModId)
        {
            if (ModId != null)
            {
                SCMSDataLayer.DALCommon.ModuleId = Convert.ToInt32(ModId);
            }
            return View();
        }

    }
}
