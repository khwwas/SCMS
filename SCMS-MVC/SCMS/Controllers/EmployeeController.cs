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

            ViewData["ddl_Gender"] = new SelectList(new DALGender().GetAllData(), "Gndr_Id", "Gndr_Title", "ddl_Gender");
            ViewData["ddl_Religion"] = new SelectList(new DALReligion().GetAllData(), "Rlgn_Id", "Rlgn_Title", "ddl_Religion");
            ViewData["ddl_MaritalStatus"] = new SelectList(new DALMaritalStatus().GetAllData(), "MS_Id", "MS_Title", "ddl_MaritalStatus");
            ViewData["ddl_Nationality"] = new SelectList(new DALNationality().GetAllData(), "Natn_Id", "Natn_Title", "ddl_Nationality");
            return View("Employee");
        }

        public ActionResult Edit(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ViewData["Employee"] = new DALEmployee().GetEmployeeById(id);;
                ViewData["EmpId"] = id;
            }

            ViewData["ddl_Gender"] = new SelectList(new DALGender().GetAllData(), "Gndr_Id", "Gndr_Title", "ddl_Gender");
            ViewData["ddl_Religion"] = new SelectList(new DALReligion().GetAllData(), "Rlgn_Id", "Rlgn_Title", "ddl_Religion");
            ViewData["ddl_MaritalStatus"] = new SelectList(new DALMaritalStatus().GetAllData(), "MS_Id", "MS_Title", "ddl_MaritalStatus");
            ViewData["ddl_Nationality"] = new SelectList(new DALNationality().GetAllData(), "Natn_Id", "Natn_Title", "ddl_Nationality");


            ViewData["ddl_Department"] = new SelectList(new DALDepartment().GetAllRecords(), "Dpt_Id", "Dpt_Title", "ddl_Department");
            ViewData["ddl_EmployeeType"] = new SelectList(new DALEmployeeType().GetAllData(), "EmpTyp_Id", "EmpTyp_Title", "ddl_EmployeeType");
            ViewData["ddl_JobTitle"] = new SelectList(new DALJobTitle().GetAllRecords(), "JT_Id", "JT_Title", "ddl_JobTitle");
            ViewData["ddl_LeaveGroup"] = new SelectList(new DALLeaveGroup().GetAllRecords(), "LevGrp_Id", "LevGrp_Title", "ddl_LeaveGroup");
            ViewData["ddl_LeaveType"] = new SelectList(new DALLeaveType().GetAllData(), "LevTyp_Id", "LevTyp_Title", "ddl_LeaveType");
            ViewData["ddl_Location"] = new SelectList(new DALLocation().GetAllData(), "Loc_Id", "Loc_Title", "ddl_Location");
            ViewData["ddl_Shift"] = new SelectList(new DALShift().GetAllData(), "Shft_Id", "Shft_Title", "ddl_Shift");

            
            return View("EditEmployee");
        }

        public ActionResult SaveRecord(string Code, string Title, DateTime DoB, string CNIC, string Gender, string MeritalStaus, string Nationality, string Religion, string Address, string Phone, string Mobile, string Email, DateTime AptmentDate, DateTime JoiningDate, DateTime ConfirmDate, string Probation, string NoticePerd)
        {
            Int32 li_ReturnValue = 0;

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
                    ViewData["SaveResult"] = li_ReturnValue;
                }

                if (String.IsNullOrEmpty(PevCode))
                {
                    return PartialView("GridData");
                }
                else
                {

                    Response.Write("" + li_ReturnValue);
                    return null;
                } 
            }
            catch
            {
                return PartialView("GridData");
            }
        }

        public ActionResult DeleteRecord(string EmpId)
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
                
        public ActionResult ChangePhoto(String EmployeeId, HttpPostedFileBase profileImage)
        {
            if (!string.IsNullOrEmpty(EmployeeId))
            {
                if (profileImage != null)
                {
                    String savedFileName = UploadImage(profileImage, EmployeeId);
                    try
                    {
                        SETUP_Employee emp = objDalEmployee.GetEmployeeById(EmployeeId);

                        if (emp!=null && !string.IsNullOrEmpty(emp.Emp_ImagePath))
                        {
                            DeleteUploadedImage(emp.Emp_ImagePath);
                        }

                        if (savedFileName != null && !savedFileName.Trim().Equals(String.Empty))
                        {
                            emp.Emp_ImagePath = savedFileName;
                            objDalEmployee.ChangeImage(emp);
                        }

                    }
                    catch (Exception)
                    { }
                }
               
                return RedirectToAction("Edit", "Employee", new { id = EmployeeId });
            }
            return null;
        }

        public String UploadImage(HttpPostedFileBase file,string EmpId)
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
                    ViewData["SaveResult"] = li_ReturnValue;
                }

                return PartialView("PlacementGridData");
            }
            catch
            {
                return PartialView("PlacementGridData");
            }
        }

        // Delete Placement 
        public ActionResult DeletePlacement(string EmpId,string Id)
        {
            Int32 li_ReturnValue = 0;
            ViewData["EmpId"] = EmpId;
            try
            {
                li_ReturnValue = objDalEmpPlacement.DeleteById(Id);
                ViewData["SaveResult"] = li_ReturnValue;

                return PartialView("PlacementGridData");
            }
            catch
            {
                return PartialView("PlacementGridData");
            }
        }

    }
}
