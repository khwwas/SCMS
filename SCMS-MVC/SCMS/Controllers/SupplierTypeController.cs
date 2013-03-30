using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
namespace SCMS.Controllers
{
    public class SupplierTypeController : Controller
    {
        //
        // GET: /Supplier Type/
        DALSupplierType objDalSupplierType = new DALSupplierType();
       
        public ActionResult Index()
        {
            return View("SupplierType");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title)
        {
            Int32  li_ReturnValue = 0;

            try
            {
                SETUP_SupplierType lrow_SupplierType = new SETUP_SupplierType();
                
                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_SupplierType") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_SupplierType");
                    }
                }


                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_SupplierType.SuppType_Id = ps_Code;
                    lrow_SupplierType.SuppType_Code = ps_Code;
                    lrow_SupplierType.SuppType_Title = ps_Title;
                    lrow_SupplierType.SuppType_Active = 1;

                    li_ReturnValue = objDalSupplierType.SaveRecord(lrow_SupplierType);
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
                li_ReturnValue = objDalSupplierType.DeleteRecordById(_pId);
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
