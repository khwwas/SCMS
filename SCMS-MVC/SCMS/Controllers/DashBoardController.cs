using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCMS.Controllers
{
    public class DashBoardController : Controller
    {
        //
        // GET: /DashBoard/

        public ActionResult Index()
        {
            var user = (SCMSDataLayer.DB.SECURITY_User)Session["user"];

            SCMS.SystemParameters.CurrentUserName = user.User_Title.Trim().ToString();

            ViewData["Modules"] = new SCMSDataLayer.DALModules().GetUserModules(user.UsrGrp_Id);
            return View();
        }

    }
}
