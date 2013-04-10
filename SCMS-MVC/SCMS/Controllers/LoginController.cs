using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View("Login");
        }

        #region Functions
        public int ValidateUser(string ps_UserName, string ps_Password)
        {
            DALLogin objDal = new DALLogin();

            try
            {
                SECURITY_User user = objDal.ValidateUser(ps_UserName, ps_Password);

                if (user != null)
                {
                    Session["user"] = user;
                    return 1;
                }
                else
                {
                    Int32 li_Id = 0;
                    return li_Id = Convert.ToInt32(user.User_Id);
                }
            }
            catch
            {
                return 0;
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("user");
            Session.Abandon();
            return RedirectToAction("../");
        }
        #endregion
    }
}
