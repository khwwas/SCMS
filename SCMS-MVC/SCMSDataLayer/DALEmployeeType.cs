using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
   public class DALEmployeeType
   {
       public int Save(SETUP_EmployeeType newSetupEmployeeType)
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               SETUP_EmployeeType existingSetupmployeeType = dbSCMS.SETUP_EmployeeTypes.Where(c => c.EmpTyp_Id.Equals(newSetupEmployeeType.EmpTyp_Id)).SingleOrDefault();
               if (existingSetupmployeeType != null)
               {
                   existingSetupmployeeType.EmpTyp_Title = newSetupEmployeeType.EmpTyp_Title;
                   existingSetupmployeeType.EmpTyp_Abbreviation = newSetupEmployeeType.EmpTyp_Abbreviation;
                   existingSetupmployeeType.EmpTyp_Active = newSetupEmployeeType.EmpTyp_Active;
                   existingSetupmployeeType.EmpTyp_SortOrder = newSetupEmployeeType.EmpTyp_SortOrder;
               }
               else
               {
                   dbSCMS.SETUP_EmployeeTypes.InsertOnSubmit(newSetupEmployeeType);
               }
               dbSCMS.SubmitChanges();
               return Convert.ToInt32(newSetupEmployeeType.EmpTyp_Id);
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
               int result = dbSCMS.ExecuteCommand("Delete From SETUP_EmployeeType where EmpTyp_Id='" + Id + "'");
               return 1;
           }
           catch
           {
               return 0;
           }
       }


       public List<SETUP_EmployeeType> GetAllData()
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               return dbSCMS.SETUP_EmployeeTypes.Where(c => c.EmpTyp_Active == 1).OrderBy(c => c.EmpTyp_Id).ToList();
           }
           catch (Exception ex)
           {
               return null;
           }
       }
   }
}
