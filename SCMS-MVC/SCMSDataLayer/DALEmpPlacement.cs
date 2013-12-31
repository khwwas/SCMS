using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALEmpPlacement
   {
       public int Save(SETUP_EmpPlacement newSetupEmpPlacement)
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               SETUP_EmpPlacement existingEmpPlacement = dbSCMS.SETUP_EmpPlacements.Where(c => c.Plcmt_Id.Equals(newSetupEmpPlacement.Plcmt_Id)).SingleOrDefault();
               if (existingEmpPlacement != null)
               {
                   
                   existingEmpPlacement.Dpt_Id = newSetupEmpPlacement.Dpt_Id;
                   existingEmpPlacement.EmpTyp_Id = newSetupEmpPlacement.EmpTyp_Id;
                   existingEmpPlacement.JT_Id = newSetupEmpPlacement.JT_Id;
                   existingEmpPlacement.LevGrp_Id = newSetupEmpPlacement.LevGrp_Id;
                   existingEmpPlacement.LevTyp_Id = newSetupEmpPlacement.LevTyp_Id;
                   existingEmpPlacement.Loc_Id = newSetupEmpPlacement.Loc_Id;
                   existingEmpPlacement.Shft_Id = newSetupEmpPlacement.Shft_Id;
                  
               }
               else
               {
                   dbSCMS.SETUP_EmpPlacements.InsertOnSubmit(newSetupEmpPlacement);
               }
               dbSCMS.SubmitChanges();
               return Convert.ToInt32(newSetupEmpPlacement.Plcmt_Id);
           }
           catch
           {
               return 0;
           }
       }

       public sp_GetEmployeePlacementsResult GetLastPlacement(string EmpId)
       {
           try
           {
               SCMSDataContext dbSCMS = Connection.Create();
               return dbSCMS.sp_GetEmployeePlacements(EmpId).LastOrDefault();
           }
           catch
           {
               return null;
           }
       }
   }
}
