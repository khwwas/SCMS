using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
   public class DALEmployee
   {
       public int Save(SETUP_Employee newSetupEmployee)
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               SETUP_Employee existingSetupmployee = dbSCMS.SETUP_Employees.Where(c => c.Emp_Id.Equals(newSetupEmployee.Emp_Id)).SingleOrDefault();
               if (existingSetupmployee != null)
               {
                   existingSetupmployee.Emp_Title = newSetupEmployee.Emp_Title;
                   existingSetupmployee.Emp_DoB = newSetupEmployee.Emp_DoB;
                   existingSetupmployee.Emp_Address = newSetupEmployee.Emp_Address;
                   existingSetupmployee.Emp_AptmentDate = newSetupEmployee.Emp_AptmentDate;
                   existingSetupmployee.Emp_CNIC = newSetupEmployee.Emp_CNIC;
                   existingSetupmployee.Emp_ConfirmDate = newSetupEmployee.Emp_ConfirmDate;
                   existingSetupmployee.Emp_Email = newSetupEmployee.Emp_Email;
                   existingSetupmployee.Emp_JoiningDate = newSetupEmployee.Emp_JoiningDate;
                   existingSetupmployee.Emp_Mobile = newSetupEmployee.Emp_Mobile;
                   existingSetupmployee.Emp_Months_NoticePerd = newSetupEmployee.Emp_Months_NoticePerd;
                   existingSetupmployee.Emp_Months_Probation = newSetupEmployee.Emp_Months_Probation;
                   existingSetupmployee.Emp_Phone = newSetupEmployee.Emp_Phone;
                   existingSetupmployee.Gndr_Id = newSetupEmployee.Gndr_Id;
                   existingSetupmployee.MS_Id = newSetupEmployee.MS_Id;
                   existingSetupmployee.Natn_Id = newSetupEmployee.Natn_Id;
                   existingSetupmployee.Rlgn_Id = newSetupEmployee.Rlgn_Id;
                   

                   existingSetupmployee.Emp_Active = newSetupEmployee.Emp_Active;
                   existingSetupmployee.Emp_SortOrder = newSetupEmployee.Emp_SortOrder;
               }
               else
               {
                   dbSCMS.SETUP_Employees.InsertOnSubmit(newSetupEmployee);
               }
               dbSCMS.SubmitChanges();
               return Convert.ToInt32(newSetupEmployee.Emp_Id);
           }
           catch
           {
               return 0;
           }
       }

       public int DeleteById(string Id)
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               dbSCMS.ExecuteCommand("Delete From SETUP_EmpPlacement where Emp_Id='" + Id + "'");
               int result = dbSCMS.ExecuteCommand("Delete From SETUP_Employee where Emp_Id='" + Id + "'");
               return 1;
           }
           catch
           {
               return 0;
           }
       }

       public SETUP_Employee GetEmployeeById(string Id)
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               return dbSCMS.SETUP_Employees.Where(c => c.Emp_Active == 1 && c.Emp_Id.Equals(Id)).Take(1).SingleOrDefault();
           }
           catch (Exception ex)
           {
               return null;
           }
       }

       public int ChangeImage(SETUP_Employee emp)
       {
           try{
               SCMSDataContext dbSCMS = Connection.Create();
               SETUP_Employee existingSetupmployee = dbSCMS.SETUP_Employees.Where(c => c.Emp_Id.Equals(emp.Emp_Id)).SingleOrDefault();
               
               if (existingSetupmployee != null){
                   existingSetupmployee.Emp_ImagePath=emp.Emp_ImagePath;
                   dbSCMS.SubmitChanges();
               }
               return Convert.ToInt32(emp.Emp_Id);
           }
           catch{
               return 0;
           }
       }


       public List<SETUP_Employee> GetAllData()
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               return dbSCMS.SETUP_Employees.Where(c => c.Emp_Active == 1).OrderBy(c => c.Emp_Id).ToList();
           }
           catch (Exception ex)
           {
               return null;
           }
       }
   }
}
