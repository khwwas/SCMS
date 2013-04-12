using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALLeaveType
    {

        public int SaveRecord(SETUP_LeaveType lrow_LeaveType)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_LeaveType lRow_ExistingData = dbSCMS.SETUP_LeaveTypes.Where(c => c.LevTyp_Id.Equals(lrow_LeaveType.LevTyp_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.LevTyp_Title = lrow_LeaveType.LevTyp_Title;
                    lRow_ExistingData.Loc_Id = lrow_LeaveType.Loc_Id;
                    lRow_ExistingData.LevTyp_Count = lrow_LeaveType.LevTyp_Count;
                    lRow_ExistingData.LevTyp_Abbreviation = lrow_LeaveType.LevTyp_Abbreviation;
                }
                else
                {
                    dbSCMS.SETUP_LeaveTypes.InsertOnSubmit(lrow_LeaveType);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(lrow_LeaveType.LevTyp_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public List<sp_GetLeaveTypesListResult> GetAllData()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_GetLeaveTypesList().ToList();
            }
            catch
            {
                return null;
            }
        }

        public int DeleteRecordById(String _Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SETUP_LeaveType where LevTyp_Id='" + _Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

    }
}
