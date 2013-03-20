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
        public DataSet ListOfChartOfAccounts(int pi_Level)
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
                _cmd.Parameters.Add(new SqlParameter("@Level", SqlDbType.Int)).Value = pi_Level;
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

        #region Ledger Detail
        public DataSet LedgerDetail(string ps_Location, int pi_AllAccCode, string ps_AccCodeFrom, string ps_AccCodeTo,
                                    int pi_AllDate, DateTime pdt_DateFrom, DateTime pdt_DateTo)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand _cmd = new SqlCommand();
            DataSet _ds = new DataSet();

            try
            {
                con = Connection.ReportConnection("Open");
                if (con.State != System.Data.ConnectionState.Open)
                {
                    return null;
                }

                _cmd.Connection = con;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "sp_ReportLedgerDetail";
                _cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar)).Value = ps_Location;
                _cmd.Parameters.Add(new SqlParameter("@AllAccCode", SqlDbType.Int)).Value = pi_AllAccCode;
                _cmd.Parameters.Add(new SqlParameter("@AccCodeFrom", SqlDbType.VarChar)).Value = ps_AccCodeFrom;
                _cmd.Parameters.Add(new SqlParameter("@AccCodeTo", SqlDbType.VarChar)).Value = ps_AccCodeTo;
                _cmd.Parameters.Add(new SqlParameter("@AllDate", SqlDbType.Int)).Value = pi_AllDate;
                _cmd.Parameters.Add(new SqlParameter("@DateFrom", SqlDbType.DateTime)).Value = pdt_DateFrom;
                _cmd.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.DateTime)).Value = pdt_DateTo;

                _cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(_cmd);
                da.Fill(_ds, "LedgerDetail");

                Connection.ReportConnection("Close");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _ds;
        }
        #endregion

        #region Trial Balance
        public DataSet TrialBalance()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand _cmd = new SqlCommand();
            DataSet _ds = new DataSet();

            try
            {
                con = Connection.ReportConnection("Open");
                if (con.State != System.Data.ConnectionState.Open)
                {
                    return null;
                }

                _cmd.Connection = con;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "sp_ReportTrialBalance";

                //_ReportDocument.SetParameterValue("@AllLocation", 1);
                //_ReportDocument.SetParameterValue("@LocationId", "''");
                //_ReportDocument.SetParameterValue("@AllVoucherType", 1);
                //_ReportDocument.SetParameterValue("@VoucherTypeId", "''");
                //_ReportDocument.SetParameterValue("@AllDate", 1);
                //_ReportDocument.SetParameterValue("@DateFrom", "''");
                //_ReportDocument.SetParameterValue("@DateTo", "''");

                _cmd.Parameters.Add(new SqlParameter("@AllLocation", SqlDbType.Int)).Value = 1;
                _cmd.Parameters.Add(new SqlParameter("@LocationId", SqlDbType.VarChar)).Value = " ";

                _cmd.Parameters.Add(new SqlParameter("@AllVoucherType", SqlDbType.Int)).Value = 1;
                _cmd.Parameters.Add(new SqlParameter("@VoucherTypeId", SqlDbType.VarChar)).Value = " ";

                _cmd.Parameters.Add(new SqlParameter("@AllDate", SqlDbType.Int)).Value = 1;
                _cmd.Parameters.Add(new SqlParameter("@DateFrom", SqlDbType.VarChar)).Value = " ";
                _cmd.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.VarChar)).Value = " ";

                _cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(_cmd);
                da.Fill(_ds, "TrialBalance");

                Connection.ReportConnection("Close");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _ds;
        }
        #endregion

    }
}
