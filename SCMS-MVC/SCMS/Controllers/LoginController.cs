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
            List<SETUP_Company> CompaniesList = new SCMSDataLayer.DALCompany().GetCmpByCode("00001");

            if( CompaniesList != null && 
                CompaniesList.Count > 0 &&
                CompaniesList[0].Cmp_Title != null &&
                CompaniesList[0].Cmp_Title.ToString() != "")
            {
                SCMS.SystemParameters.CurrentCmpName = CompaniesList[0].Cmp_Title;
                ViewData["CmpDesc"] = CompaniesList[0].Cmp_Title;
            }
            
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
                    DALCommon.UserLoginId = user.User_Id;
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
