﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;

namespace SCMS.Controllers
{
    public class VoucherController : Controller
    {
        //
        // GET: /Voucher/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult VoucherEntry(String VoucherId)
        {
            DALVoucherEntry objDalVoucherEntry = new DALVoucherEntry();

            var VoucherTypes = new DALVoucherType().GetAllData();
            sp_GetVoucherTypesListResult Select = new sp_GetVoucherTypesListResult();
            Select.VchrType_Id = "0";
            Select.VchrType_Title = "";
            VoucherTypes.Insert(0, Select);

            ViewData["ddl_VoucherType"] = new SelectList(VoucherTypes, "VchrType_Id", "VchrType_Title", "");
            var ChartOfAccounts = new DALChartOfAccount().GetChartOfAccountForDropDown();
            SETUP_ChartOfAccount SelectChartOfAccount = new SETUP_ChartOfAccount();
            SelectChartOfAccount.ChrtAcc_Id = "0";
            SelectChartOfAccount.ChrtAcc_Code = "";
            ChartOfAccounts.Insert(0, SelectChartOfAccount);
            ViewData["ChartOfAccounts"] = ChartOfAccounts;
            ViewData["ddl_Account"] = new SelectList(ChartOfAccounts, "ChrtAcc_Id", "ChrtAcc_Title", "");
            if (!String.IsNullOrEmpty(VoucherId))
            {
                ViewData["VoucherCode"] = VoucherId;
            }
            else
            {
                ViewData["VoucherCode"] = "[Auto]";
            }
            ViewData["CurrentDate"] = DateTime.Now.ToString("MM/dd/yyyy");
            return View();
        }

        public String NewVoucherDetailEntryRow(int RowNo)
        {
            var ChartOfAccounts = new DALChartOfAccount().GetChartOfAccountForDropDown();
            String Response = "";
            // Response += "<div id='DetailRow" + RowNo.ToString() + "' style='float: left; width: auto;'>";
            Response += "<div class='CustomCell' style='width: 250px; height: 30px;'>";
            Response += "<select id='ddl_Account" + RowNo.ToString() + "' name='ddl_Account" + RowNo.ToString() + "' style='width:250px;'><option value='0'></option>";
            foreach (SETUP_ChartOfAccount row in ChartOfAccounts)
            {
                Response += "<option value='" + row.ChrtAcc_Id + "'>" + row.ChrtAcc_Title + "</option>";
            }
            Response += "</select>";
            Response += "</div>";
            Response += "<div class='CustomCell' style='width: 118px; height: 30px;'>";
            Response += "<input type='text' class='CustomText' style='width: 100px;' id='txt_Debit" + RowNo.ToString() + "' name='txt_Debit'";
            Response += "maxlength='50' onblur='SetTotals(this.id)' />";
            Response += "</div>";
            Response += "<div class='CustomCell' style='width: 118px; height: 30px;'>";
            Response += "<input type='text' class='CustomText' style='width: 100px;' id='txt_Credit" + RowNo.ToString() + "' name='txt_Credit'";
            Response += "maxlength='50' onblur='SetTotals(this.id)' />";
            Response += "</div>";
            Response += "<div class='CustomCell' style='width: 565px; height: 30px;'>";
            Response += "<input type='text' class='CustomText' style='width: 545px;' id='txt_Details" + RowNo.ToString() + "' name='txt_Details'";
            Response += "maxlength='50' />";
            Response += "</div>";
            // Response += "</div>";
            return Response;
        }

        public ActionResult SaveVoucher(String VoucherMasterCode, DateTime VoucherDate, int Status, String VoucherType, String Remarks, String[] VoucherDetailRows)
        {
            DALVoucherEntry objDalVoucherEntry = new DALVoucherEntry();
            Int32 li_ReturnValue = 0;
            int flag = 0;
            try
            {
                GL_VchrMaster GL_Master = new GL_VchrMaster();
                GL_VchrDetail GL_Detail = new GL_VchrDetail();
                String Prefix = new DALVoucherType().GetAllData().Where(c => c.VchrType_Id.Equals(VoucherType)).SingleOrDefault().VchrType_Prefix;

                if (String.IsNullOrEmpty(VoucherMasterCode))
                {
                    if (DALCommon.AutoCodeGeneration("GL_VchrMaster") == 1)
                    {
                        VoucherMasterCode = DALCommon.GetMaximumCode("GL_VchrMaster");
                    }
                }
                else
                {
                    flag = 1;
                }

                if (!String.IsNullOrEmpty(VoucherMasterCode))
                {
                    GL_Master.VchMas_Id = VoucherMasterCode;
                    ViewData["VoucherId"] = VoucherMasterCode;
                    GL_Master.VchMas_Code = Prefix + VoucherMasterCode;
                    ViewData["VoucherCode"] = Prefix + VoucherMasterCode;
                    GL_Master.VchMas_Date = VoucherDate;
                    GL_Master.Loc_Id = "00001";
                    GL_Master.VchrType_Id = VoucherType;
                    GL_Master.VchMas_Remarks = Remarks;
                    GL_Master.VchMas_Status = Status;
                    GL_Master.VchMas_EnteredDate = DateTime.Now;
                    li_ReturnValue = objDalVoucherEntry.SaveVoucherMaster(GL_Master);
                    if (li_ReturnValue != null && li_ReturnValue > 0)
                    {
                        if (flag == 1)
                        {
                            objDalVoucherEntry.DeleteDetailRecordByMasterId(VoucherMasterCode);
                        }
                        String[] DetailRows = VoucherDetailRows[0].Split(',');
                        foreach (String Row in DetailRows)
                        {
                            String[] Columns = Row.Split('~');
                            String VoucherDetailCode = "";

                            if (String.IsNullOrEmpty(VoucherDetailCode))
                            {
                                if (DALCommon.AutoCodeGeneration("GL_VchrDetail") == 1)
                                {
                                    VoucherDetailCode = DALCommon.GetMaximumCode("GL_VchrDetail");
                                }
                            }

                            if (!String.IsNullOrEmpty(VoucherDetailCode) && Columns[0] != null && Columns[0] != "" && Columns[0] != "0" && ((Columns[1] != null && Columns[1] != "") || (Columns[2] != null && Columns[2] != "")))
                            {
                                GL_Detail = new GL_VchrDetail();
                                GL_Detail.VchDet_Id = VoucherDetailCode;
                                GL_Detail.VchMas_Id = VoucherMasterCode;
                                GL_Detail.ChrtAcc_Id = Columns[0].ToString(); //Columns[0] has AccountId from Account Title drop down;
                                GL_Detail.VchMas_DrAmount = (Columns[1] != null && Columns[1] != "") ? Convert.ToDecimal(Columns[1]) : 0; // Columns[1] has Debit Amount
                                GL_Detail.VchMas_CrAmount = (Columns[2] != null && Columns[2] != "") ? Convert.ToDecimal(Columns[2]) : 0; // Columns[2] has Debit Amount
                                GL_Detail.VchDet_Remarks = (Columns[3] != null && Columns[3] != "") ? Columns[3].ToString() : ""; // Columns[3] has Remarks
                                objDalVoucherEntry.SaveVoucherDetail(GL_Detail);
                            }
                        }
                    }
                }
                ViewData["SaveResult"] = li_ReturnValue;
                return PartialView("GridData");
            }
            catch
            {
                ViewData["SaveResult"] = 0;
                return PartialView("GridData");
            }
        }

    }
}