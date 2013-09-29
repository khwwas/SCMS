using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALAuditLog
    {
        ///// <summary>
        ///// This Function will be deleted after change in all setup and transaction files
        ///// </summary>
        ///// <param name="lrow_AuditTrail"></param>
        ///// <returns></returns>
        //public int SaveRecord(SYSTEM_AuditTrail lrow_AuditTrail)
        //{
        //    int li_ReturnValue = 0;

        //    try
        //    {
        //        SCMSDataContext dbSCMS = Connection.Create();
        //        dbSCMS.SYSTEM_AuditTrails.InsertOnSubmit(lrow_AuditTrail);
        //        dbSCMS.SubmitChanges();
        //        li_ReturnValue = Convert.ToInt32(lrow_AuditTrail.AdtTrl_Id);
        //    }
        //    catch
        //    {
        //        return 0;
        //    }

        //    return li_ReturnValue;
        //}

        public int SaveRecord(Int32 pi_ScreenId, String ps_UserId, String ps_Action, String[] ps_Label, String[] ps_Data)
        {
            SYSTEM_AuditTrail systemAuditTrail = new SYSTEM_AuditTrail();

            try
            {
                systemAuditTrail.Scr_Id = pi_ScreenId;
                systemAuditTrail.User_Id = ps_UserId;
                systemAuditTrail.AdtTrl_Action = ps_Action;
                systemAuditTrail.AdtTrl_EntryId = ps_Data[0];
                systemAuditTrail.AdtTrl_DataDump = GenerateXML(ps_Label, ps_Data);
                systemAuditTrail.AdtTrl_Date = DateTime.Now;

                SCMSDataContext dbSCMS = Connection.Create();
                dbSCMS.SYSTEM_AuditTrails.InsertOnSubmit(systemAuditTrail);
                dbSCMS.SubmitChanges();
            }
            catch
            {
                return -1;
            }

            return 1;
        }

        string GenerateXML(string[] ps_Label, string[] ps_Data)
        {
            string ReturnValue = "";
            Int32 _Index;

            try
            {
                ReturnValue += " <html> <body> ";
                for (_Index = 0; _Index <= ps_Label.Length - 1; _Index++)
                {
                    ReturnValue += "<div style=clear:both><b> " + ps_Label[_Index] + ": </b> " + ps_Data[_Index] + " &nbsp;</div> ";
                }
                ReturnValue += " </body> </html>";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return ReturnValue;
        }
    }
}
