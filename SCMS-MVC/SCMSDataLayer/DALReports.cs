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

        #region Voucher Document
        public DataSet VoucherDocument(string ps_Location, int pi_AllDoc, string ps_DocFrom, string ps_DocTo,
                                       int pi_AllDate, DateTime pdt_DateFrom, DateTime pdt_DateTo)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand _cmd = new SqlCommand();
            DataSet _ds = new DataSet();
            string _Sql = "";

            try
            {
                con = Connection.ReportConnection("Open");
                if (con.State != System.Data.ConnectionState.Open)
                {
                    return null;
                }

                _Sql += "   Select GL_VchrMaster.VchMas_Id, ";
                _Sql += "          GL_VchrMaster.VchMas_Code, ";
                _Sql += "          GL_VchrMaster.VchMas_Date, ";
                _Sql += "          SETUP_VoucherType.VchrType_Title, ";
                _Sql += "          GL_VchrMaster.VchMas_Status, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          GL_VchrDetail.VchDet_Remarks, ";
                _Sql += "          GL_VchrDetail.VchMas_DrAmount, ";
                _Sql += "          GL_VchrDetail.VchMas_CrAmount ";
                _Sql += "          From GL_VchrMaster, ";
                _Sql += "          GL_VchrDetail, ";
                _Sql += "          SETUP_VoucherType, ";
                _Sql += "          SETUP_ChartOfAccount ";
                _Sql += "          Where ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) And ";
                _Sql += "          ( GL_VchrMaster.VchrType_Id = SETUP_VoucherType.VchrType_Id ) And ";
                _Sql += "          ( GL_VchrDetail.ChrtAcc_Id = SETUP_ChartOfAccount.ChrtAcc_Id ) And ";
                _Sql += "          ( GL_VchrMaster.Loc_Id = '" + ps_Location + "' ) And ";
                _Sql += "          ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, GL_VchrMaster.VchMas_Date, 103 ), 103 ) Between ";
                _Sql += "                                      Convert( datetime, Convert( Char, '" + pdt_DateFrom.ToString() + "', 103 ), 103 ) And ";
                _Sql += "                                      Convert( datetime, Convert( Char, '" + pdt_DateTo.ToString() + "', 103 ), 103 ) ) And ";
                _Sql += "          ( " + pi_AllDoc + " = 1 Or GL_VchrMaster.VchMas_Id Between '" + ps_DocFrom + "' And '" + ps_DocTo + "' ) ";
                //_Sql += "          ( SETUP_VoucherType.VchrType_Id = '' ) And ";
                //_Sql += "          ( Lower( GL_VchrMaster.VchMas_Status ) = Lower( '' ) )  ";
                _Sql += "          Order By GL_VchrMaster.VchMas_Date, ";
                _Sql += "          GL_VchrDetail.VchDet_Id ";

                SqlDataAdapter da = new SqlDataAdapter(_Sql, con);
                da.Fill(_ds, "VoucherDocument");

                Connection.ReportConnection("Close");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _ds;
        }
        #endregion

        #region Ledger Detail - Location Wise
        public DataSet LedgerDetail_LocWise(string ps_Location, int pi_AllAccCode, string ps_AccCodeFrom, string ps_AccCodeTo,
                                    int pi_AllDate, DateTime pdt_DateFrom, DateTime pdt_DateTo)
        {
            SqlConnection con = new SqlConnection();
            //SqlCommand _cmd = new SqlCommand();
            DataSet _ds = new DataSet();
            string _Sql = "";

            try
            {
                con = Connection.ReportConnection("Open");
                if (con.State != System.Data.ConnectionState.Open)
                {
                    return null;
                }

                //_cmd.Connection = con;
                //_cmd.CommandType = CommandType.Text;
                //_cmd.CommandType = CommandType.StoredProcedure;
                //_cmd.CommandText = "sp_ReportLedgerDetail";
                //_cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar)).Value = ps_Location;
                //_cmd.Parameters.Add(new SqlParameter("@AllAccCode", SqlDbType.Int)).Value = pi_AllAccCode;
                //_cmd.Parameters.Add(new SqlParameter("@AccCodeFrom", SqlDbType.VarChar)).Value = ps_AccCodeFrom;
                //_cmd.Parameters.Add(new SqlParameter("@AccCodeTo", SqlDbType.VarChar)).Value = ps_AccCodeTo;
                //_cmd.Parameters.Add(new SqlParameter("@AllDate", SqlDbType.Int)).Value = pi_AllDate;
                //_cmd.Parameters.Add(new SqlParameter("@DateFrom", SqlDbType.DateTime)).Value = pdt_DateFrom;
                //_cmd.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.DateTime)).Value = pdt_DateTo;

                _Sql += "   Select * ";
                _Sql += "     From GL_VchrMaster, ";
                _Sql += "          GL_VchrDetail, ";
                _Sql += "          SETUP_Location, ";
                _Sql += "          SETUP_VoucherType, ";
                _Sql += "          SETUP_ChartOfAccount  ";
                _Sql += "    Where ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) And ";
                _Sql += "          ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "          ( GL_VchrMaster.VchrType_Id = SETUP_VoucherType.VchrType_Id ) And ";
                _Sql += "          ( GL_VchrDetail.ChrtAcc_Id = SETUP_ChartOfAccount.ChrtAcc_Id ) And ";
                _Sql += "          ( 1 = 1 Or Lower( GL_VchrMaster.VchMas_Status ) = LOWER( 'Approved' ) ) and  ";
                _Sql += "          ( SETUP_Location.Loc_Id = '" + ps_Location + "' ) And ";
                _Sql += "          ( " + pi_AllAccCode + " = 1 Or SETUP_ChartOfAccount.ChrtAcc_Id Between '" + ps_AccCodeFrom + "' And '" + ps_AccCodeTo + "' ) And ";
                _Sql += "          ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, GL_VchrMaster.VchMas_Date, 103 ), 103 ) Between ";
                _Sql += "                            Convert( datetime, Convert( Char, '" + pdt_DateFrom.ToString() + "', 103 ), 103 ) And ";
                _Sql += "                            Convert( datetime, Convert( Char, '" + pdt_DateTo.ToString() + "', 103 ), 103 ) ) ";
                _Sql += " Order By SETUP_Location.Loc_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          GL_VchrMaster.VchMas_Date ";

                //_cmd.ExecuteNonQuery();
                //SqlDataAdapter da = new SqlDataAdapter(_cmd);
                SqlDataAdapter da = new SqlDataAdapter(_Sql, con);
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

        #region Ledger Detail - Account Wise
        public DataSet LedgerDetail_AccWise(string ps_Location, int pi_AllAccCode, string ps_AccCodeFrom, string ps_AccCodeTo,
                                            int pi_AllDate, DateTime pdt_DateFrom, DateTime pdt_DateTo)
        {
            SqlConnection con = new SqlConnection();
            //SqlCommand _cmd = new SqlCommand();
            DataSet _ds = new DataSet();
            string _Sql = "";

            try
            {
                con = Connection.ReportConnection("Open");
                if (con.State != System.Data.ConnectionState.Open)
                {
                    return null;
                }

                //_cmd.Connection = con;
                //_cmd.CommandType = CommandType.Text;
                //_cmd.CommandType = CommandType.StoredProcedure;
                //_cmd.CommandText = "sp_ReportLedgerDetail";
                //_cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar)).Value = ps_Location;
                //_cmd.Parameters.Add(new SqlParameter("@AllAccCode", SqlDbType.Int)).Value = pi_AllAccCode;
                //_cmd.Parameters.Add(new SqlParameter("@AccCodeFrom", SqlDbType.VarChar)).Value = ps_AccCodeFrom;
                //_cmd.Parameters.Add(new SqlParameter("@AccCodeTo", SqlDbType.VarChar)).Value = ps_AccCodeTo;
                //_cmd.Parameters.Add(new SqlParameter("@AllDate", SqlDbType.Int)).Value = pi_AllDate;
                //_cmd.Parameters.Add(new SqlParameter("@DateFrom", SqlDbType.DateTime)).Value = pdt_DateFrom;
                //_cmd.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.DateTime)).Value = pdt_DateTo;

                _Sql += "   Select * ";
                _Sql += "     From GL_VchrMaster, ";
                _Sql += "          GL_VchrDetail, ";
                _Sql += "          SETUP_Location, ";
                _Sql += "          SETUP_VoucherType, ";
                _Sql += "          SETUP_ChartOfAccount  ";
                _Sql += "    Where ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) And ";
                _Sql += "          ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "          ( GL_VchrMaster.VchrType_Id = SETUP_VoucherType.VchrType_Id ) And ";
                _Sql += "          ( GL_VchrDetail.ChrtAcc_Id = SETUP_ChartOfAccount.ChrtAcc_Id ) And ";
                _Sql += "          ( 1 = 1 Or Lower( GL_VchrMaster.VchMas_Status ) = LOWER( 'Approved' ) ) and  ";
                _Sql += "          ( SETUP_Location.Loc_Id = '" + ps_Location + "' ) And ";
                _Sql += "          ( " + pi_AllAccCode + " = 1 Or SETUP_ChartOfAccount.ChrtAcc_Id Between '" + ps_AccCodeFrom + "' And '" + ps_AccCodeTo + "' ) And ";
                _Sql += "          ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, GL_VchrMaster.VchMas_Date, 103 ), 103 ) Between ";
                _Sql += "                            Convert( datetime, Convert( Char, '" + pdt_DateFrom.ToString() + "', 103 ), 103 ) And ";
                _Sql += "                            Convert( datetime, Convert( Char, '" + pdt_DateTo.ToString() + "', 103 ), 103 ) ) ";
                _Sql += " Order By SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_Location.Loc_Title, ";
                _Sql += "          GL_VchrMaster.VchMas_Date ";

                //_cmd.ExecuteNonQuery();
                //SqlDataAdapter da = new SqlDataAdapter(_cmd);
                SqlDataAdapter da = new SqlDataAdapter(_Sql, con);
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
        public DataSet TrialBalance(string ps_Location, int pi_AllAccCode, string ps_AccCodeFrom, string ps_AccCodeTo,
                                    int pi_AllDate, DateTime pdt_DateFrom, DateTime pdt_DateTo)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand _cmd = new SqlCommand();
            DataSet _ds = new DataSet();
            string _Sql = "";

            try
            {
                con = Connection.ReportConnection("Open");
                if (con.State != System.Data.ConnectionState.Open)
                {
                    return null;
                }

                //_cmd.Connection = con;
                //_cmd.CommandType = CommandType.StoredProcedure;
                //_cmd.CommandText = "sp_ReportTrialBalance";

                //_ReportDocument.SetParameterValue("@AllLocation", 1);
                //_ReportDocument.SetParameterValue("@LocationId", "''");
                //_ReportDocument.SetParameterValue("@AllVoucherType", 1);
                //_ReportDocument.SetParameterValue("@VoucherTypeId", "''");
                //_ReportDocument.SetParameterValue("@AllDate", 1);
                //_ReportDocument.SetParameterValue("@DateFrom", "''");
                //_ReportDocument.SetParameterValue("@DateTo", "''");

                //_cmd.Parameters.Add(new SqlParameter("@AllLocation", SqlDbType.Int)).Value = 1;
                //_cmd.Parameters.Add(new SqlParameter("@LocationId", SqlDbType.VarChar)).Value = " ";

                //_cmd.Parameters.Add(new SqlParameter("@AllVoucherType", SqlDbType.Int)).Value = 1;
                //_cmd.Parameters.Add(new SqlParameter("@VoucherTypeId", SqlDbType.VarChar)).Value = " ";

                //_cmd.Parameters.Add(new SqlParameter("@AllDate", SqlDbType.Int)).Value = 1;
                //_cmd.Parameters.Add(new SqlParameter("@DateFrom", SqlDbType.VarChar)).Value = " ";
                //_cmd.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.VarChar)).Value = " ";

                //_cmd.ExecuteNonQuery();

                _Sql += "   Select SETUP_ChartOfAccount.ChrtAcc_Id, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code,";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title,";
                //_Sql += "          ChrtAcc_Code = Case SETUP_ChartOfAccount.ChrtAcc_Level ";
                //_Sql += "               When 1 Then SETUP_ChartOfAccount.ChrtAcc_Code ";
                //_Sql += "               When 2 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Code ";
                //_Sql += "               When 3 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Code ";
                //_Sql += "               When 4 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Code ";
                //_Sql += "               When 5 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Code ";
                //_Sql += "               When 6 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Code ";
                //_Sql += "               When 7 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Code ";
                //_Sql += "               When 8 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Code ";
                //_Sql += "               When 9 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Code ";
                //_Sql += "               Else SETUP_ChartOfAccount.ChrtAcc_Code ";
                //_Sql += "          End, ";
                //_Sql += "          ChrtAcc_Title = Case SETUP_ChartOfAccount.ChrtAcc_Level ";
                //_Sql += "               When 1 Then SETUP_ChartOfAccount.ChrtAcc_Title ";
                //_Sql += "               When 2 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Title ";
                //_Sql += "               When 3 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Title ";
                //_Sql += "               When 4 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Title ";
                //_Sql += "               When 5 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Title ";
                //_Sql += "               When 6 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Title ";
                //_Sql += "               When 7 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Title ";
                //_Sql += "               When 8 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Title ";
                //_Sql += "               When 9 Then SPACE(SETUP_ChartOfAccount.ChrtAcc_Level * 3) + SETUP_ChartOfAccount.ChrtAcc_Title ";
                //_Sql += "               Else SETUP_ChartOfAccount.ChrtAcc_Title ";
                //_Sql += "          End, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Type, ";
                _Sql += "          IsNULL( Sum( IsNULL( VoucherDetail.VchMas_DrAmount, 0 ) ), 0 ) As VchMas_DrAmount, ";
                _Sql += "          IsNULL( Sum( IsNULL( VoucherDetail.VchMas_CrAmount, 0 ) ), 0 ) As VchMas_CrAmount";
                _Sql += "     From SETUP_ChartOfAccount LEFT OUTER JOIN ";
                _Sql += "          ( Select SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "                   GL_VchrDetail.VchMas_DrAmount, ";
                _Sql += "                   GL_VchrDetail.VchMas_CrAmount ";
                _Sql += "              From SETUP_ChartOfAccount, ";
                _Sql += "                   GL_VchrDetail, ";
                _Sql += "                   GL_VchrMaster, ";
                _Sql += "                   SETUP_Location ";
                _Sql += "             Where ( SETUP_ChartOfAccount.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) And ";
                _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and ";
                _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "                   ( SETUP_Location.Loc_Id = '" + ps_Location + "' ) And ";
                _Sql += "                   ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, GL_VchrMaster.VchMas_Date, 103 ), 103 ) Between ";
                _Sql += "                                               Convert( datetime, Convert( Char, '" + pdt_DateFrom.ToString() + "', 103 ), 103 ) And ";
                _Sql += "                                               Convert( datetime, Convert( Char, '" + pdt_DateTo.ToString() + "', 103 ), 103 ) ) ";
                _Sql += "          ) VoucherDetail On ";
                _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( VoucherDetail.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ) ";
                _Sql += "    Where ( " + pi_AllAccCode + " = 1 Or SETUP_ChartOfAccount.ChrtAcc_Id Between '" + ps_AccCodeFrom + "' And '" + ps_AccCodeTo + "' ) ";

                ////
                //_Sql += "          GL_VchrDetail.VchMas_DrAmount, ";
                //_Sql += "          GL_VchrDetail.VchMas_CrAmount ";
                //_Sql += "     From SETUP_ChartOfAccount LEFT OUTER JOIN GL_VchrDetail On ";
                //_Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) and ";
                //_Sql += "               ( " + pi_AllAccCode + " = 1 Or SETUP_ChartOfAccount.ChrtAcc_Id Between '" + ps_AccCodeFrom + "' And '" + ps_AccCodeTo + "' ) ";
                //_Sql += "          LEFT OUTER JOIN GL_VchrMaster On ";
                //_Sql += "               ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and ";
                //_Sql += "               ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, GL_VchrMaster.VchMas_Date, 103 ), 103 ) Between ";
                //_Sql += "                                           Convert( datetime, Convert( Char, '" + pdt_DateFrom.ToString() + "', 103 ), 103 ) And ";
                //_Sql += "                                           Convert( datetime, Convert( Char, '" + pdt_DateTo.ToString() + "', 103 ), 103 ) ) ";
                //_Sql += "          LEFT OUTER JOIN SETUP_Location On ";
                //_Sql += "               ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                //_Sql += "               ( SETUP_Location.Loc_Id = '" + ps_Location + "' ) ";
                //////
                _Sql += " Group By SETUP_ChartOfAccount.ChrtAcc_Id, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Type ";
                _Sql += " Order By SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level ";

                SqlDataAdapter da = new SqlDataAdapter(_Sql, con);
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

        #region Income Statement
        #endregion
    }
}
