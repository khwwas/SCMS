using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;
using System.Data;
using System.Data.SqlClient;

namespace SCMSDataLayer
{
    public class DALCalendar
    {
        public int SaveRecord(SETUP_Calendar lrow_Calendar)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                SETUP_Calendar lRow_ExistingData = dbSCMS.SETUP_Calendars.Where(b => b.Cldr_Id.Equals(lrow_Calendar.Cldr_Id)).SingleOrDefault();

                if (lRow_ExistingData != null)
                {
                    lRow_ExistingData.Loc_Id = lrow_Calendar.Loc_Id;
                    lRow_ExistingData.Cmp_Id = lrow_Calendar.Cmp_Id;
                    lRow_ExistingData.Cldr_Title = lrow_Calendar.Cldr_Title;
                    lRow_ExistingData.Cldr_Prefix = lrow_Calendar.Cldr_Prefix;
                    lRow_ExistingData.Cldr_DateStart = lrow_Calendar.Cldr_DateStart;
                    lRow_ExistingData.Cldr_DateEnd = lrow_Calendar.Cldr_DateEnd;
                    lRow_ExistingData.CldrType_Id = lrow_Calendar.CldrType_Id;
                }
                else
                {
                    dbSCMS.SETUP_Calendars.InsertOnSubmit(lrow_Calendar);
                }
                dbSCMS.SubmitChanges();

                li_ReturnValue = Convert.ToInt32(lrow_Calendar.Cldr_Id);
            }
            catch
            {
                return 0;
            }

            return li_ReturnValue;
        }

        public List<SETUP_Calendar> GetAllRecords()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.SETUP_Calendars.Where(c => c.Cldr_Active == 1).OrderBy(c => c.Cldr_Code).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int DeleteRecordById(String ps_Id)
        {
            int li_ReturnValue = 0;

            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                li_ReturnValue = dbSCMS.ExecuteCommand("Delete From SETUP_Calendar where Cldr_Id='" + ps_Id + "'");
            }
            catch
            {
                li_ReturnValue = 0;
            }

            return li_ReturnValue;
        }

        public string GetCalendarPrefix_ByCurrentDate(DateTime pdt_CurrentDate)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand _cmd = new SqlCommand();
            DataSet _ds = new DataSet();
            string ls_Sql = "", ls_ReturnValue = "";

            try
            {
                con = Connection.ReportConnection("Open");
                if (con.State != System.Data.ConnectionState.Open)
                {
                    return null;
                }

                ls_Sql += " Select Cldr_Prefix ";
                ls_Sql += "   From SETUP_Calendar ";
                ls_Sql += "  Where Convert( DateTime, '" + pdt_CurrentDate.ToString("dd/MM/yyyy") + "', 103 ) Between ";
                ls_Sql += "        Convert( DateTime, Convert( Char, Cldr_DateStart, 103 ), 103 ) And ";
                ls_Sql += "        Convert( DateTime, Convert( Char, Cldr_DateEnd, 103 ), 103 ) ";

                SqlDataAdapter da = new SqlDataAdapter(ls_Sql, con);
                da.Fill(_ds, "Calendar");

                if (_ds != null && _ds.Tables != null && _ds.Tables[0].Rows.Count > 0 &&
                    _ds.Tables[0].Rows[0]["Cldr_Prefix"] != null &&
                    _ds.Tables[0].Rows[0]["Cldr_Prefix"].ToString() != "")
                {
                    ls_ReturnValue = _ds.Tables[0].Rows[0]["Cldr_Prefix"].ToString();
                }

                Connection.ReportConnection("Close");
            }
            catch
            {
                ls_ReturnValue = "";
            }

            return ls_ReturnValue;
        }
    }
}
