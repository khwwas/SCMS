using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
namespace SCMS.Controllers
{
    public class CustomerTypeController : Controller
    {
        //
        // GET: /Supplier Type/
        DALCustomerType objDalCustomerType = new DALCustomerType();
       
        public ActionResult Index()
        {
            return View("CustomerType");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title)
        {
            Int32  li_ReturnValue = 0;

            try
            {
                SETUP_CustomerType lrow_CustomerType = new SETUP_CustomerType();
                
                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_CustomerType") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_CustomerType");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_CustomerType.CustType_Id = ps_Code;
                    lrow_CustomerType.CustType_Code = ps_Code;
                    lrow_CustomerType.CustType_Title = ps_Title;
                    lrow_CustomerType.CustType_Active = 1;

                    li_ReturnValue = objDalCustomerType.SaveRecord(lrow_CustomerType);
                    ViewData["SaveResult"] = li_ReturnValue;
                }
               
                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteRecord(String _pId)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                li_ReturnValue = objDalCustomerType.DeleteRecordById(_pId);
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
