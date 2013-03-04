using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class VoucherTypeNarrationController : Controller
    {
        //
        // GET: /VoucherTypeNarration/

        public ActionResult Index()
        {
            ViewData["ddl_VoucherType"] = new SelectList(new DALVoucherType().GetAllData().Where(c => c.VchrType_Active == 1).OrderBy(c => c.VchrType_Title), "VchrType_Id", "VchrType_Title", "ddl_VoucherType");
            return View("VoucherTypeNarration");
        }

        public ActionResult SaveRecord(String ps_Code, String ps_Title, String ps_CmpId)
        {
            DALVoucherTypeNarration objDal = new DALVoucherTypeNarration();
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_VoucherTypeNarration lrow_Data = new SETUP_VoucherTypeNarration();

                if (String.IsNullOrEmpty(ps_Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_VoucherTypeNarration") == 1)
                    {
                        ps_Code = DALCommon.GetMaximumCode("SETUP_VoucherTypeNarration");
                    }
                }

                if (!String.IsNullOrEmpty(ps_Code))
                {
                    lrow_Data.VchrTypeNarr_Id = ps_Code;
                    lrow_Data.VchrTypeNarr_Code = ps_Code;
                    lrow_Data.VchrTypeNarr_Title = ps_Title;
                    lrow_Data.VchrType_Id = ps_CmpId;
                    lrow_Data.VchrTypeNarr_Active = 1;

                    li_ReturnValue = objDal.SaveRecord(lrow_Data);
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
            DALVoucherTypeNarration objDal = new DALVoucherTypeNarration();
            Int32 li_ReturnValue = 0;

            try
            {
                li_ReturnValue = objDal.DeleteRecordById(_pId);
                ViewData["DeleteResult"] = li_ReturnValue;

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

    }
}
