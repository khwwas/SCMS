using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SCMSDataLayer.DB;

namespace SCMSDataLayer
{
    public class DALReports
    {
        #region Setups
        public DataSet ListOfCompanies()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand _cmd = new SqlCommand();
            DataSet _ds = new DataSet();
            //string _Sql = "";

            try
            {
                con = Connection.ReportConnection("Open");
                if (con.State != System.Data.ConnectionState.Open)
                {
                    return null;
                }

                _cmd.Connection = con;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "sp_GetCompanyList";
                _cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(_cmd);
                da.Fill(_ds, "Company");

                Connection.ReportConnection("Close");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _ds;
        }
        public DataSet ListOfLocations()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand _cmd = new SqlCommand();
            DataSet _ds = new DataSet();
            //string _Sql = "";

            try
            {
                con = Connection.ReportConnection("Open");
                if (con.State != System.Data.ConnectionState.Open)
                {
                    return null;
                }

                _cmd.Connection = con;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "sp_GetLocationList";
                _cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(_cmd);
                da.Fill(_ds, "Location");

                Connection.ReportConnection("Close");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _ds;
        }
        public DataSet ListOfCities()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand _cmd = new SqlCommand();
            DataSet _ds = new DataSet();
            //string _Sql = "";

            try
            {
                con = Connection.ReportConnection("Open");
                if (con.State != System.Data.ConnectionState.Open)
                {
                    return null;
                }

                _cmd.Connection = con;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "sp_GetCityList";
                _cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(_cmd);
                da.Fill(_ds, "City");

                Connection.ReportConnection("Close");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _ds;
        }
        public DataSet ListOfVoucherTypes()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand _cmd = new SqlCommand();
            DataSet _ds = new DataSet();
            //string _Sql = "";

            try
            {
                con = Connection.ReportConnection("Open");
                if (con.State != System.Data.ConnectionState.Open)
                {
                    return null;
                }

                _cmd.Connection = con;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "sp_GetVoucherTypesList";
                _cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(_cmd);
                da.Fill(_ds, "VoucherTypes");

                Connection.ReportConnection("Close");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _ds;
        }
        public DataSet ListOfChartOfAccounts()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand _cmd = new SqlCommand();
            DataSet _ds = new DataSet();
            //string _Sql = "";

            try
            {
                con = Connection.ReportConnection("Open");
                if (con.State != System.Data.ConnectionState.Open)
                {
                    return null;
                }

                _cmd.Connection = con;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "sp_ReportChartOfAccount";
                _cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(_cmd);
                da.Fill(_ds, "ChartOfAccount");

                Connection.ReportConnection("Close");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _ds;
        }
        #endregion

        public List<sp_ReportLedgerDetailResult> LedgerDetail()
        {
            try
            {
                SCMSDataContext dbSCMS = Connection.Create();
                return dbSCMS.sp_ReportLedgerDetail().ToList();

                //.Where(c => c.AccNatr_Active == 1).OrderBy(c => c.AccNatr_Code).ToList();
            }
            catch
            {
                return null;
            }
        }

 
    }
}
