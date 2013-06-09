using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCMSDataLayer.DB;
using SCMSDataLayer;
using System.IO;

namespace SCMS.Controllers
{
    public class EmployeeController : Controller
    {
        DALEmployee objDalEmployee = new DALEmployee();
        DALEmpPlacement objDalEmpPlacement = new DALEmpPlacement();

        public ActionResult Index()
        {
          return View("");
        }

        public ActionResult EmployeeEntry(string id)
        {

            SETUP_Employee SETUP_Emp = null;
            sp_GetEmployeePlacementsResult SETUP_EmpPlc = null; 
            if (!String.IsNullOrEmpty(id))
            {
                SETUP_Emp = new DALEmployee().GetEmployeeById(id);
                ViewData["Employee"] = SETUP_Emp;
                ViewData["EmpId"] = id;
                SETUP_EmpPlc = new SCMSDataLayer.DALEmpPlacement().GetLastPlacement(id);
                ViewData["LastPlacement"] = SETUP_EmpPlc;
            }

            ViewData["ddl_Gender"] = new SelectList(new DALGender().GetAllData(), "Gndr_Id", "Gndr_Title", SETUP_Emp!=null?SETUP_Emp.Gndr_Id:"ddl_Gender");
            ViewData["ddl_Religion"] = new SelectList(new DALReligion().GetAllData(), "Rlgn_Id", "Rlgn_Title", SETUP_Emp != null ? SETUP_Emp.Rlgn_Id : "ddl_Religion");
            ViewData["ddl_MaritalStatus"] = new SelectList(new DALMaritalStatus().GetAllData(), "MS_Id", "MS_Title", SETUP_Emp != null ? SETUP_Emp.MS_Id : "ddl_MaritalStatus");
            ViewData["ddl_Nationality"] = new SelectList(new DALNationality().GetAllData(), "Natn_Id", "Natn_Title", SETUP_Emp != null ? SETUP_Emp.Natn_Id : "ddl_Nationality");


            ViewData["ddl_Department"] = new SelectList(new DALDepartment().GetAllRecords(), "Dpt_Id", "Dpt_Title", SETUP_EmpPlc!=null?SETUP_EmpPlc.Dpt_Id:"ddl_Department");
            ViewData["ddl_EmployeeType"] = new SelectList(new DALEmployeeType().GetAllData(), "EmpTyp_Id", "EmpTyp_Title", SETUP_EmpPlc != null ? SETUP_EmpPlc.EmpTyp_Id:"ddl_EmployeeType");
            ViewData["ddl_JobTitle"] = new SelectList(new DALJobTitle().GetAllRecords(), "JT_Id", "JT_Title", SETUP_EmpPlc != null ? SETUP_EmpPlc.JT_Id:"ddl_JobTitle");
            ViewData["ddl_LeaveGroup"] = new SelectList(new DALLeaveGroup().GetAllRecords(), "LevGrp_Id", "LevGrp_Title", SETUP_EmpPlc != null ? SETUP_EmpPlc.LevGrp_Id:"ddl_LeaveGroup");
            ViewData["ddl_LeaveType"] = new SelectList(new DALLeaveType().GetAllData(), "LevTyp_Id", "LevTyp_Title", SETUP_EmpPlc != null ? SETUP_EmpPlc.LevTyp_Id:"ddl_LeaveType");
            ViewData["ddl_Location"] = new SelectList(new DALLocation().GetAllData(), "Loc_Id", "Loc_Title", SETUP_EmpPlc != null ? SETUP_EmpPlc.Loc_Id:"ddl_Location");
            ViewData["ddl_Shift"] = new SelectList(new DALShift().GetAllData(), "Shft_Id", "Shft_Title", SETUP_EmpPlc != null ? SETUP_EmpPlc.Shft_Id:"ddl_Shift");

            return View("Employee");
        }

        public ActionResult SaveEmployee(string Code, string Title, DateTime DoB, string CNIC, string Gender, string MeritalStaus, string Nationality, string Religion, string Address, string Phone, string Mobile, string Email, DateTime AptmentDate, DateTime JoiningDate, DateTime ConfirmDate, string Probation, string NoticePerd, HttpPostedFileBase ProfileImg)
        {
            Int32 li_ReturnValue = 0; string ImageName = "";
            string[] rList = new string[3];
            try
            {
                SETUP_Employee row_Employee = new SETUP_Employee();
                String PevCode = Code;
                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_Employee") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_Employee");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    row_Employee.Emp_Id = Code;
                    row_Employee.Emp_Code = Code;
                    row_Employee.Emp_Title = Title;

                    
                    row_Employee.Emp_DoB = DoB;
                    row_Employee.Emp_Address = Address;
                    row_Employee.Emp_AptmentDate = AptmentDate;
                    row_Employee.Emp_CNIC = CNIC;
                    row_Employee.Emp_ConfirmDate = ConfirmDate;
                    row_Employee.Emp_Email = Email;
                    row_Employee.Emp_JoiningDate = JoiningDate;
                    row_Employee.Emp_Mobile = Mobile;
                    row_Employee.Emp_Months_NoticePerd = NoticePerd;
                    row_Employee.Emp_Months_Probation = Probation;
                    row_Employee.Emp_Phone = Phone;
                    row_Employee.Gndr_Id = Gender;
                    row_Employee.MS_Id = MeritalStaus;
                    row_Employee.Natn_Id = Nationality;
                    row_Employee.Rlgn_Id = Religion;

                    row_Employee.Emp_Active = 1;
                    row_Employee.Emp_SortOrder = 1;
                    
                    li_ReturnValue = objDalEmployee.Save(row_Employee);

                    ImageName=ProfileImage(Code, ProfileImg);
                 
                }
            }
            catch {
               
            }
            rList[0] = li_ReturnValue.ToString();
            rList[1] = Code.ToString();
            rList[2] = ImageName.ToString();
            System.Web.Script.Serialization.JavaScriptSerializer se = new System.Web.Script.Serialization.JavaScriptSerializer();
            string result = se.Serialize(rList);
            Response.Write(result);
            return null;
        }

        public ActionResult DeleteEmployee(string EmpId)
        {
            Int32 li_ReturnValue = 0;

            try
            {

                SETUP_Employee emp = objDalEmployee.GetEmployeeById(EmpId);
                if (emp != null && !string.IsNullOrEmpty(emp.Emp_ImagePath)){
                    DeleteUploadedImage(emp.Emp_ImagePath);
                }
                
                li_ReturnValue = objDalEmployee.DeleteById(EmpId);
                ViewData["SaveResult"] = li_ReturnValue;

                return PartialView("GridData");
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult EmployeeConsole()
        {
            return View("EmployeeConsole");
        }

        public ActionResult ChangeProfileImg(String EmployeeId, HttpPostedFileBase profileImage)
        {
            Response.Write(ProfileImage(EmployeeId, profileImage));
            return null;
        }

        public String ProfileImage(String EmployeeId, HttpPostedFileBase profileImage)
        {

            string li_ReturnValue = "0";

            if (!string.IsNullOrEmpty(EmployeeId))
            {
                if (profileImage != null)
                {
                    String savedFileName = UploadNewImage(profileImage, EmployeeId);
                    try
                    {
                        SETUP_Employee emp = objDalEmployee.GetEmployeeById(EmployeeId);

                        if (emp != null && !string.IsNullOrEmpty(emp.Emp_ImagePath))
                        {
                            DeleteUploadedImage(emp.Emp_ImagePath);
                        }

                        if (savedFileName != null && !savedFileName.Trim().Equals(String.Empty))
                        {
                            emp.Emp_ImagePath = savedFileName;
                            objDalEmployee.ChangeImage(emp);
                        }

                        li_ReturnValue = savedFileName;
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            return li_ReturnValue;
        
        }

        public String UploadNewImage(HttpPostedFileBase file,string EmpId)
        {
            String UPLOADED_IMAGES = "/UploadedDocuments/";
            try
            {
                String fileName = "";
                String retFileName = "";
                if (file != null)
                {
                    fileName = EmpId + "_" + DateTime.Now.ToString();
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace("/", "_");
                    fileName = fileName.Replace(":", "_");
                    fileName = fileName + Path.GetFileName(file.FileName);
                    retFileName = fileName;
                    fileName = UPLOADED_IMAGES + fileName;
                    String filePath = Server.MapPath("../" + fileName);
                    file.SaveAs(filePath);
                }
                return retFileName;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public void DeleteUploadedImage(String imagePath)
        {
            try{
                String UPLOADED_IMAGES = "/UploadedDocuments/";
                String filePathName = UPLOADED_IMAGES + imagePath;
                FileInfo TheFile = new FileInfo(Server.MapPath("../" + filePathName));
                if (TheFile.Exists){
                    System.IO.File.Delete(Server.MapPath("../" + filePathName));
                }
            }
            catch (Exception ex){
            }
        }

        //Save Placement for Employee

        public ActionResult SavePlacement(string Code, string EmpId, string DptId, string EmpTypId, string JTId, string LevGrpId, string LevTypId, string LocId, string ShftId)
        {
            Int32 li_ReturnValue = 0;

            try
            {
                SETUP_EmpPlacement row_EmpPlacement = new SETUP_EmpPlacement();
                ViewData["EmpId"] = EmpId;
                if (String.IsNullOrEmpty(Code))
                {
                    if (DALCommon.AutoCodeGeneration("SETUP_EmpPlacement") == 1)
                    {
                        Code = DALCommon.GetMaximumCode("SETUP_EmpPlacement");
                    }
                }

                if (!String.IsNullOrEmpty(Code))
                {
                    row_EmpPlacement.Plcmt_Id = Code;
                    row_EmpPlacement.Plcmt_Code = Code;

                    row_EmpPlacement.Emp_Id = EmpId;
                    row_EmpPlacement.Dpt_Id = DptId;
                    row_EmpPlacement.EmpTyp_Id = EmpTypId;
                    row_EmpPlacement.JT_Id = JTId;
                    row_EmpPlacement.LevGrp_Id = LevGrpId;
                    row_EmpPlacement.LevTyp_Id = LevTypId;
                    row_EmpPlacement.Loc_Id = LocId;
                    row_EmpPlacement.Shft_Id = ShftId;


                    li_ReturnValue = objDalEmpPlacement.Save(row_EmpPlacement);
                    Response.Write(Code);
                    return null;
                }
            }
            catch
            {
                Response.Write("" + li_ReturnValue);
            }

            Response.Write("" + li_ReturnValue);
            return null;
        }

    }
}
