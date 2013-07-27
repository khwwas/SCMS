using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer;
using SCMSDataLayer.DB;
using CrystalDecisions.Shared.Json;
using System.Web.Script.Serialization;

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
            var Locations = new DALLocation().PopulateData();
            var Voucher = new DALVoucherEntry().GetAllMasterRecords();

            ViewData["ddl_VoucherType"] = new SelectList(VoucherTypes, "VchrType_Id", "VchrType_Title", Session["VoucherTypeForVoucherEntry"]);
            var ChartOfAccounts = new DALChartOfAccount().GetChartOfAccountForDropDown();
            string ChartOfAccountCodes = "";
            foreach (SETUP_ChartOfAccount COA in ChartOfAccounts)
            {
                if (ChartOfAccountCodes.Length > 0)
                {
                    ChartOfAccountCodes += "," + COA.ChrtAcc_Id + ":" + COA.ChrtAcc_Title;
                }
                {
                    ChartOfAccountCodes += COA.ChrtAcc_Id + ":" + COA.ChrtAcc_Title;
                }
            }
            ViewData["ChartOfAccountCodesWithTitles"] = ChartOfAccountCodes;
            SETUP_ChartOfAccount SelectChartOfAccount = new SETUP_ChartOfAccount();
            SelectChartOfAccount.ChrtAcc_Id = "0";
            SelectChartOfAccount.ChrtAcc_Code = "";
            ChartOfAccounts.Insert(0, SelectChartOfAccount);
            ViewData["ChartOfAccounts"] = ChartOfAccounts;
            ViewData["ddl_Account"] = new SelectList(ChartOfAccounts, "ChrtAcc_Id", "ChrtAcc_Title", "");
            var NarrationList = new DALVoucherTypeNarration().GetAllData().ToList();
            string[] nList = new string[NarrationList.Count];
            if (NarrationList != null && NarrationList.Count > 0)
            {
                for (int index = 0; index < NarrationList.Count; index++)
                {
                    nList[index] = NarrationList[index].VchrTypeNarr_Title;
                }
                System.Web.Script.Serialization.JavaScriptSerializer se = new System.Web.Script.Serialization.JavaScriptSerializer();
                ViewData["Narrations"] = se.Serialize(nList);
            }
            ViewData["ddl_Location"] = new SelectList(Locations, "Loc_Id", "Loc_Title", Session["LocationIdForVoucherEntry"]);
            if (!String.IsNullOrEmpty(VoucherId))
            {
                SetVoucherEntryToEdit(VoucherId);
            }
            else
            {
                ViewData["VoucherCode"] = "[Auto]";
                ViewData["CurrentDate"] = DateTime.Now.ToString("MM/dd/yyyy");
            }
            Session.Remove("VoucherTypeForVoucherEntry");
            Session.Remove("LocationIdForVoucherEntry");
            return View();
        }

        public String NewVoucherDetailEntryRow(int RowNo, String AccountCode, String Narration, String Debit, String Credit)
        {
            if (AccountCode == null || AccountCode == "undefined")
            {
                AccountCode = "0";
            }
            if (Narration == null || Narration == "undefined")
            {
                Narration = "";
            }
            if (Debit == null || Debit == "undefined" || Debit == "0.000000")
            {
                Debit = "";
            }
            else
            {
                Debit = Math.Round(Convert.ToDecimal(Debit), 2).ToString();
            }
            if (Credit == null || Credit == "undefined" || Credit == "0.000000")
            {
                Credit = "";
            }
            else
            {
                Credit = Math.Round(Convert.ToDecimal(Credit), 2).ToString();
            }
            var ChartOfAccounts = new DALChartOfAccount().GetChartOfAccountForDropDown();
            String Response = "";
            // Response += "<div id='DetailRow" + RowNo.ToString() + "' style='float: left; width: auto;'>";
            Response += "<div class='CustomCell' style='width: 250px; height: 30px;'>";
            Response += "<select id='ddl_Account" + RowNo.ToString() + "' name='ddl_Account" + RowNo.ToString() + "' style='width:250px;'><option value='0'></option>";
            foreach (SETUP_ChartOfAccount row in ChartOfAccounts)
            {
                if (row.ChrtAcc_Id == AccountCode)
                {
                    Response += "<option value='" + row.ChrtAcc_Id + "' selected='selected'>" + row.ChrtAcc_Title + "</option>";
                }
                else
                {
                    Response += "<option value='" + row.ChrtAcc_Id + "'>" + row.ChrtAcc_Title + "</option>";
                }
            }
            Response += "</select>";
            Response += "</div>";
            Response += "<div class='CustomCell' style='width: 565px; height: 30px;'>";
            Response += "<input type='text' class='CustomText' style='width: 545px;' id='txt_Details" + RowNo.ToString() + "' name='txt_Details'";
            Response += "maxlength='200' value='" + Narration + "' />";
            Response += "</div>";
            Response += "<div class='CustomCell' style='width: 118px; height: 30px;'>";
            Response += "<input type='text' class='CustomText' style='width: 100px;' id='txt_Debit" + RowNo.ToString() + "' name='txt_Debit'";
            if (Credit != "")
            {
                Response += "maxlength='50' onblur='SetTotals(this.id)' disabled='disabled' />";
            }
            else
            {
                Response += "maxlength='50' value='" + Debit + "' onblur='SetTotals(this.id)' />";
            }
            Response += "</div>";
            Response += "<div class='CustomCell' style='width: 118px; height: 30px;'>";
            Response += "<input type='text' class='CustomText' style='width: 100px;' id='txt_Credit" + RowNo.ToString() + "' name='txt_Credit'";
            if (Debit != "")
            {
                Response += "maxlength='50'  onblur='SetTotals(this.id)' disabled='disabled' />";
            }
            else
            {
                Response += "maxlength='50' value='" + Credit + "' onblur='SetTotals(this.id)' />";
            }
            Response += "</div>";
            // Response += "</div>";
            return Response;
        }

        public String GetLastRecordByVoucherTypeId(string LocationId, string VoucherTypeId)
        {
            var Voucher = new DALVoucherEntry().GetLastRecordByVchrType(LocationId, VoucherTypeId);
            String LastVoucher = "";
            if (Voucher != null && Voucher.Count > 0)
            {
                LastVoucher += "<div class='CustomCell' style='width: 800px; height: 30px; font-family: Tahoma;'>";
                LastVoucher += "<b>Voucher # :</b>";
                LastVoucher += Voucher.Last().VchMas_Code;
                LastVoucher += "<b>, Date : </b>";
                LastVoucher += Voucher.Last().VchMas_Date != null ? Convert.ToDateTime(Voucher.Last().VchMas_Date).ToShortDateString() : "";
                LastVoucher += "<b>, Status : </b>";
                LastVoucher += Voucher.Last().VchMas_Status;
                LastVoucher += "</div>";
                LastVoucher += "<div class='Clear' style='border-bottom: 1px solid #ccc; margin-bottom: 5px;'>";
                LastVoucher += "</div>";
            }
            return LastVoucher;
        }

        //public ActionResult SaveVoucher(String VoucherMasterCode, DateTime VoucherDate, string Status, String VoucherType, String LocationId, String Remarks, String[] VoucherDetailRows)
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public string SaveVoucher(IEnumerable<string> VoucherDetailRows)
        {
            String[] MasterRow = VoucherDetailRows.Last().Split('║');
            String VoucherMasterId = MasterRow[0];
            String VoucherMasterCode = MasterRow[6].Replace("[Auto]", null);
            DateTime VoucherDate = MasterRow[1] != null ? Convert.ToDateTime(MasterRow[1]) : DateTime.Now;
            string Status = MasterRow[2];
            String VoucherType = MasterRow[3];
            String LocationId = MasterRow[4];
            String Remarks = MasterRow[5];
            string ls_VchrTypId = "";

            Session["VoucherTypeForVoucherEntry"] = VoucherType;
            Session["LocationIdForVoucherEntry"] = LocationId;
            DALVoucherEntry objDalVoucherEntry = new DALVoucherEntry();
            Int32 li_ReturnValue = 0;
            int flag = 0;

            try
            {
                GL_VchrMaster GL_Master = new GL_VchrMaster();
                GL_VchrDetail GL_Detail = new GL_VchrDetail();
                String Prefix = new DALVoucherType().GetAllData().Where(c => c.VchrType_Id.Equals(VoucherType)).SingleOrDefault().VchrType_Prefix;
                ls_VchrTypId = Convert.ToString(Convert.ToInt32(VoucherType));

                if (String.IsNullOrEmpty(VoucherMasterId))
                {
                    if (DALCommon.AutoCodeGeneration("GL_VchrMaster") == 1)
                    {
                        //VoucherMasterCode = DALCommon.GetMaximumCode("GL_VchrMaster");
                        VoucherMasterId = DALCommon.GetMaxVoucherId(VoucherDate.Year.ToString().Substring(2, 2));
                        VoucherMasterCode = DALCommon.GetMaxVoucherCode("GL_VchrMaster", VoucherType, Prefix, LocationId);
                    }
                }
                else
                {
                    flag = 1;
                }

                if (!String.IsNullOrEmpty(VoucherMasterId) && !String.IsNullOrEmpty(VoucherMasterCode))
                {
                    GL_Master.VchMas_Id = VoucherMasterId;
                    ViewData["VoucherId"] = VoucherMasterId;
                    GL_Master.VchMas_Code = VoucherMasterCode;
                    ViewData["VoucherCode"] = VoucherMasterCode;
                    GL_Master.VchMas_Date = VoucherDate;
                    GL_Master.Loc_Id = LocationId;
                    GL_Master.VchrType_Id = VoucherType;
                    GL_Master.VchMas_Remarks = Remarks;
                    GL_Master.VchMas_Status = Status;
                    GL_Master.VchMas_EnteredDate = DateTime.Now;
                    li_ReturnValue = objDalVoucherEntry.SaveVoucherMaster(GL_Master);
                    if (li_ReturnValue != null && li_ReturnValue > 0)
                    {
                        if (flag == 1)
                        {
                            objDalVoucherEntry.DeleteDetailRecordByMasterId(VoucherMasterId);
                        }

                        for (int index = 0; index < VoucherDetailRows.ToList().Count - 1; index++)
                        {
                            string Row = VoucherDetailRows.ToList()[index];
                            String[] Columns = Row.Split('║');
                            String VoucherDetailCode = "";

                            if (String.IsNullOrEmpty(VoucherDetailCode))
                            {
                                if (DALCommon.AutoCodeGeneration("GL_VchrDetail") == 1)
                                {
                                    VoucherDetailCode = DALCommon.GetMaximumCode("GL_VchrDetail");
                                    //VoucherDetailCode = VoucherMasterCode;
                                }
                            }

                            if (!String.IsNullOrEmpty(VoucherDetailCode) && Columns[0] != null && Columns[0] != "" && Columns[0] != "0" && ((Columns[1] != null && Columns[1] != "") || (Columns[2] != null && Columns[2] != "")))
                            {
                                GL_Detail = new GL_VchrDetail();
                                GL_Detail.VchDet_Id = VoucherDetailCode;
                                GL_Detail.VchMas_Id = VoucherMasterId;
                                GL_Detail.ChrtAcc_Id = Columns[0].ToString(); //Columns[0] has AccountId from Account Title drop down;
                                GL_Detail.VchMas_DrAmount = (Columns[1] != null && Columns[1] != "") ? Convert.ToDecimal(Columns[1].Replace(",", "")) : 0; // Columns[1] has Debit Amount
                                GL_Detail.VchMas_CrAmount = (Columns[2] != null && Columns[2] != "") ? Convert.ToDecimal(Columns[2].Replace(",", "")) : 0; // Columns[2] has Debit Amount
                                GL_Detail.VchDet_Remarks = (Columns[3] != null && Columns[3] != "") ? Columns[3].ToString() : ""; // Columns[3] has Remarks
                                objDalVoucherEntry.SaveVoucherDetail(GL_Detail);
                            }
                        }
                    }
                }
                ViewData["SaveResult"] = li_ReturnValue;
                //return PartialView("GridData");
                //string result = ViewData["VoucherId"].ToString(); //+ "|" + ViewData["VoucherCode"].ToString() + "|" + ViewData["SaveResult"].ToString();
                string[] rList = new string[3];
                rList[0] = ViewData["VoucherId"].ToString();
                rList[1] = ViewData["VoucherCode"].ToString();
                rList[2] = ViewData["SaveResult"].ToString();
                System.Web.Script.Serialization.JavaScriptSerializer se = new System.Web.Script.Serialization.JavaScriptSerializer();
                string result = se.Serialize(rList);
                return result;
            }
            catch
            {
                //ViewData["SaveResult"] = 0;
                //return PartialView("GridData");
                return "0";
            }
        }

        public void SetVoucherEntryToEdit(String VoucherId)
        {
            var VoucherEntryRow = new DALVoucherEntry().GetAllMasterRecords().Where(c => c.VchMas_Id.Equals(VoucherId)).SingleOrDefault();
            if (VoucherEntryRow != null)
            {
                ViewData["VoucherCode"] = VoucherEntryRow.VchMas_Code;
                ViewData["VoucherId"] = VoucherEntryRow.VchMas_Id;
                ViewData["CurrentDate"] = Convert.ToDateTime(VoucherEntryRow.VchMas_Date).ToString("MM/dd/yyyy");
                ViewData["Status"] = VoucherEntryRow.VchMas_Status;
                ViewData["VoucherType"] = VoucherEntryRow.VchrType_Id;
                ViewData["LocationId"] = VoucherEntryRow.Loc_Id;
                ViewData["Remarks"] = VoucherEntryRow.VchMas_Remarks;
                var VoucherDetailRows = new DALVoucherEntry().GetAllDetailRecords().Where(c => c.VchMas_Id.Equals(VoucherId)).ToList().OrderBy(c => c.VchDet_Id).ToList();
                ViewData["DetailRecords"] = VoucherDetailRows;
                ViewData["Edit"] = "OK";
            }
        }

    }
}
