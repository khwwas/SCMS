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
        public DataSet VoucherDocument(string ps_Location, string ps_VoucherTypes, int pi_AllDoc, string ps_DocFrom, string ps_DocTo,
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
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay As ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          GL_VchrDetail.VchDet_Remarks, ";
                _Sql += "          GL_VchrDetail.VchMas_DrAmount, ";
                _Sql += "          GL_VchrDetail.VchMas_CrAmount, ";
                _Sql += "          SETUP_Location.Loc_Title ";
                _Sql += "     From GL_VchrMaster, ";
                _Sql += "          GL_VchrDetail, ";
                _Sql += "          SETUP_VoucherType, ";
                _Sql += "          SETUP_ChartOfAccount, ";
                _Sql += "          SETUP_Location ";
                _Sql += "    Where ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) And ";
                _Sql += "          ( GL_VchrMaster.VchrType_Id = SETUP_VoucherType.VchrType_Id ) And ";
                _Sql += "          ( GL_VchrDetail.ChrtAcc_Id = SETUP_ChartOfAccount.ChrtAcc_Id ) And ";
                _Sql += "          ( SETUP_Location.Loc_Id = GL_VchrMaster.Loc_Id ) And ";
                _Sql += "          ( GL_VchrMaster.Loc_Id In (" + ps_Location + ") ) And ";
                _Sql += "          ( SETUP_VoucherType.VchrType_Id In (" + ps_VoucherTypes + ") ) And ";
                _Sql += "          ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, GL_VchrMaster.VchMas_Date, 103 ), 103 ) Between ";
                _Sql += "                                      Convert( datetime, Convert( Char, '" + pdt_DateFrom.ToString("dd/MM/yyyy") + "', 103 ), 103 ) And ";
                _Sql += "                                      Convert( datetime, '" + pdt_DateTo.ToString("dd/MM/yyyy") + "', 103 ) ) And ";
                _Sql += "          ( " + pi_AllDoc + " = 1 Or GL_VchrMaster.VchMas_Id Between '" + ps_DocFrom + "' And '" + ps_DocTo + "' ) ";
                _Sql += " Order By SETUP_Location.Loc_Title, ";
                _Sql += "          GL_VchrMaster.VchMas_Date, ";
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

                _Sql += "   Select SETUP_Location.Loc_Id, ";
                _Sql += "          SETUP_Location.Loc_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Id, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay As ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          GL_VchrMaster.VchMas_Id, ";
                _Sql += "          GL_VchrMaster.VchMas_Code,  ";
                _Sql += "          GL_VchrMaster.VchMas_Date,  ";
                _Sql += "          IsNULL( GL_VchrDetail.VchMas_DrAmount, 0 ) As VchMas_DrAmount, ";
                _Sql += "          IsNULL( GL_VchrDetail.VchMas_CrAmount, 0 ) As VchMas_CrAmount, ";
                _Sql += "          GL_VchrDetail.VchDet_Remarks, ";
                _Sql += "          GL_VchrDetail.VchDet_Id, ";
                if (pi_AllDate == 1)
                {
                    _Sql += "      0.0 As OpeningBalance  ";
                }
                else
                {
                    _Sql += "      OpeningBalance = ";
                    _Sql += "           CASE ROW_NUMBER() OVER (PARTITION BY SETUP_ChartOfAccount.ChrtAcc_Id ORDER BY SETUP_ChartOfAccount.ChrtAcc_Id Asc) ";
                    _Sql += "                WHEN 1 THEN ";
                    _Sql += "                       ( Select IsNULL( Sum( ISNULL( b.VchMas_DrAmount, 0 ) - ISNULL( b.VchMas_CrAmount, 0 ) ), 0 )  ";
                    _Sql += "                           From GL_VchrMaster a, ";
                    _Sql += "                                GL_VchrDetail b ";
                    _Sql += "                          Where ( b.ChrtAcc_Id = SETUP_ChartOfAccount.ChrtAcc_Id ) And ";
                    _Sql += "                                ( b.VchMas_Id = a.VchMas_Id ) and ";
                    _Sql += "                                ( a.Loc_Id In (" + ps_Location + ") ) And ";
                    _Sql += "                                ( " + pi_AllAccCode + " = 1 Or SETUP_ChartOfAccount.ChrtAcc_Id Between '' And '' ) And ";
                    _Sql += "                                ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, a.VchMas_Date, 103 ), 103 ) < ";
                    _Sql += "                                                            Convert( datetime, '" + pdt_DateFrom.ToString("dd/MM/yyyy") + "', 103 ) ) ";
                    _Sql += "                       )";
                    _Sql += "                ELSE 0.0";
                    _Sql += "           END";
                }
                _Sql += "     From SETUP_Location, ";
                _Sql += "          SETUP_ChartOfAccount, ";
                _Sql += "          GL_VchrMaster, ";
                _Sql += "          GL_VchrDetail ";
                _Sql += "    Where ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) And ";
                _Sql += "          ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "          ( GL_VchrDetail.ChrtAcc_Id = SETUP_ChartOfAccount.ChrtAcc_Id ) And ";
                _Sql += "          ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                _Sql += "          ( " + pi_AllAccCode + " = 1 Or SETUP_ChartOfAccount.ChrtAcc_Id Between '" + ps_AccCodeFrom + "' And '" + ps_AccCodeTo + "' ) And ";
                _Sql += "          ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, GL_VchrMaster.VchMas_Date, 103 ), 103 ) Between ";
                _Sql += "                                      Convert( datetime, '" + pdt_DateFrom.ToString("dd/MM/yyyy") + "', 103 ) And ";
                _Sql += "                                      Convert( datetime, '" + pdt_DateTo.ToString("dd/MM/yyyy") + "', 103 ) ) ";
                _Sql += " Order By SETUP_Location.Loc_Title,  ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          VchMas_Date ";

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

                _Sql += "   Select SETUP_Location.Loc_Id, ";
                _Sql += "          SETUP_Location.Loc_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Id, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay As ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          GL_VchrMaster.VchMas_Id, ";
                _Sql += "          GL_VchrMaster.VchMas_Code,  ";
                _Sql += "          GL_VchrMaster.VchMas_Date,  ";
                _Sql += "          IsNULL( GL_VchrDetail.VchMas_DrAmount, 0 ) As VchMas_DrAmount, ";
                _Sql += "          IsNULL( GL_VchrDetail.VchMas_CrAmount, 0 ) As VchMas_CrAmount, ";
                _Sql += "          GL_VchrDetail.VchDet_Remarks, ";
                _Sql += "          GL_VchrDetail.VchDet_Id, ";
                if (pi_AllDate == 1)
                {
                    _Sql += "      0.0 As OpeningBalance  ";
                }
                else
                {
                    _Sql += "      OpeningBalance = ";
                    _Sql += "           CASE ROW_NUMBER() OVER (PARTITION BY SETUP_ChartOfAccount.ChrtAcc_Id ORDER BY SETUP_ChartOfAccount.ChrtAcc_Id Asc) ";
                    _Sql += "                WHEN 1 THEN ";
                    _Sql += "                       ( Select IsNULL( Sum( ISNULL( b.VchMas_DrAmount, 0 ) - ISNULL( b.VchMas_CrAmount, 0 ) ), 0 )  ";
                    _Sql += "                           From GL_VchrMaster a, ";
                    _Sql += "                                GL_VchrDetail b ";
                    _Sql += "                          Where ( b.ChrtAcc_Id = SETUP_ChartOfAccount.ChrtAcc_Id ) And ";
                    _Sql += "                                ( b.VchMas_Id = a.VchMas_Id ) and ";
                    _Sql += "                                ( a.Loc_Id In (" + ps_Location + ") ) And ";
                    _Sql += "                                ( " + pi_AllAccCode + " = 1 Or SETUP_ChartOfAccount.ChrtAcc_Id Between '' And '' ) And ";
                    _Sql += "                                ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, a.VchMas_Date, 103 ), 103 ) < ";
                    _Sql += "                                                            Convert( datetime, '" + pdt_DateFrom.ToString("dd/MM/yyyy") + "', 103 ) ) ";
                    _Sql += "                       )";
                    _Sql += "                ELSE 0.0";
                    _Sql += "           END";
                }
                _Sql += "     From SETUP_Location, ";
                _Sql += "          SETUP_ChartOfAccount, ";
                _Sql += "          GL_VchrMaster, ";
                _Sql += "          GL_VchrDetail ";
                _Sql += "    Where ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) And ";
                _Sql += "          ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "          ( GL_VchrDetail.ChrtAcc_Id = SETUP_ChartOfAccount.ChrtAcc_Id ) And ";
                _Sql += "          ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                _Sql += "          ( " + pi_AllAccCode + " = 1 Or SETUP_ChartOfAccount.ChrtAcc_Id Between '" + ps_AccCodeFrom + "' And '" + ps_AccCodeTo + "' ) And ";
                _Sql += "          ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, GL_VchrMaster.VchMas_Date, 103 ), 103 ) Between ";
                _Sql += "                                      Convert( datetime, '" + pdt_DateFrom.ToString("dd/MM/yyyy") + "', 103 ) And ";
                _Sql += "                                      Convert( datetime, '" + pdt_DateTo.ToString("dd/MM/yyyy") + "', 103 ) ) ";
                _Sql += " Order By SETUP_Location.Loc_Title,  ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          VchMas_Date ";

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
                                    int pi_AllDate, DateTime pdt_DateFrom, DateTime pdt_DateTo, int pi_Level, string ps_TrialActivity)
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

                if (ps_TrialActivity == "Activity" || ps_TrialActivity == "ActivityAll")
                {
                    _Sql += "   Select SETUP_ChartOfAccount.ChrtAcc_Id, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code,";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title,";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Type, ";
                    _Sql += "          IsNULL( Sum( IsNULL( VoucherDetail.VchMas_DrAmount, 0 ) ), 0 ) As VchMas_DrAmount, ";
                    _Sql += "          IsNULL( Sum( IsNULL( VoucherDetail.VchMas_CrAmount, 0 ) ), 0 ) As VchMas_CrAmount, ";
                    if (pi_AllDate == 1)
                    {
                        _Sql += "      0.0 As OpeningBalance,  ";
                    }
                    else
                    {
                        _Sql += "      ( Select IsNULL( Sum( IsNULL( GL_VchrDetail.VchMas_DrAmount, 0 ) ), 0 ) - ";
                        _Sql += "               IsNULL( Sum( IsNULL( GL_VchrDetail.VchMas_CrAmount, 0 ) ), 0 ) ";
                        _Sql += "          From GL_VchrDetail, ";
                        _Sql += "               GL_VchrMaster, ";
                        _Sql += "               SETUP_Location, ";
                        _Sql += "               SETUP_ChartOfAccount a ";
                        _Sql += "         Where ( a.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) and ";
                        _Sql += "               ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and ";
                        _Sql += "               ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                        _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( a.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ) And ";
                        _Sql += "               ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                        _Sql += "               ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, GL_VchrMaster.VchMas_Date, 103 ), 103 ) < ";
                        _Sql += "                                              Convert( datetime, '" + pdt_DateFrom.ToString("dd/MM/yyyy") + "', 103 )  ) ";
                        _Sql += "      ) OpeningBalance, ";
                    }
                    _Sql += "          SYSTEM_Nature.Natr_SystemTitle ";
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
                    _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                    _Sql += "                   ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, GL_VchrMaster.VchMas_Date, 103 ), 103 ) Between ";
                    _Sql += "                                               Convert( datetime, '" + pdt_DateFrom.ToString("dd/MM/yyyy") + "', 103 ) And ";
                    _Sql += "                                               Convert( datetime, '" + pdt_DateTo.ToString("dd/MM/yyyy") + "', 103 ) ) ";
                    _Sql += "          ) VoucherDetail On ";
                    _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( VoucherDetail.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ), ";
                    _Sql += "          SYSTEM_Nature ";
                    _Sql += "    Where ( " + pi_AllAccCode + " = 1 Or SETUP_ChartOfAccount.ChrtAcc_Id Between '" + ps_AccCodeFrom + "' And '" + ps_AccCodeTo + "' ) And ";
                    _Sql += "          ( SETUP_ChartOfAccount.ChrtAcc_Level <= " + pi_Level.ToString() + " ) And ";
                    _Sql += "          ( SETUP_ChartOfAccount.Natr_Id = SYSTEM_Nature.Natr_Id ) ";
                    _Sql += " Group By SETUP_ChartOfAccount.ChrtAcc_Id, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Type, ";
                    _Sql += "          SYSTEM_Nature.Natr_SystemTitle ";
                    _Sql += " Order By SETUP_ChartOfAccount.ChrtAcc_Code, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level ";
                }
                else if (ps_TrialActivity == "Opening")
                {
                    _Sql += "   Select SETUP_ChartOfAccount.ChrtAcc_Id, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code,";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title,";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Type, ";
                    _Sql += "          0.0 As VchMas_DrAmount, ";
                    _Sql += "          0.0 As VchMas_CrAmount, ";
                    _Sql += "          ( Select IsNULL( Sum( IsNULL( GL_VchrDetail.VchMas_DrAmount, 0 ) ), 0 ) - ";
                    _Sql += "                   IsNULL( Sum( IsNULL( GL_VchrDetail.VchMas_CrAmount, 0 ) ), 0 ) ";
                    _Sql += "              From GL_VchrDetail, ";
                    _Sql += "                   GL_VchrMaster, ";
                    _Sql += "                   SETUP_Location, ";
                    _Sql += "                   SETUP_ChartOfAccount a ";
                    _Sql += "             Where ( a.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) and ";
                    _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and ";
                    _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                    _Sql += "                   ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( a.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ) And ";
                    _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                    _Sql += "                   ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, GL_VchrMaster.VchMas_Date, 103 ), 103 ) < ";
                    _Sql += "                                               Convert( datetime, '" + pdt_DateFrom.ToString("dd/MM/yyyy") + "', 103 )  ) ";
                    _Sql += "          ) OpeningBalance, ";
                    _Sql += "          SYSTEM_Nature.Natr_SystemTitle ";
                    _Sql += "     From SETUP_ChartOfAccount, ";
                    _Sql += "          SYSTEM_Nature ";
                    _Sql += "    Where ( " + pi_AllAccCode + " = 1 Or SETUP_ChartOfAccount.ChrtAcc_Id Between '" + ps_AccCodeFrom + "' And '" + ps_AccCodeTo + "' ) And ";
                    _Sql += "          ( SETUP_ChartOfAccount.ChrtAcc_Level <= " + pi_Level.ToString() + " ) And ";
                    _Sql += "          ( SETUP_ChartOfAccount.Natr_Id = SYSTEM_Nature.Natr_Id ) ";
                    _Sql += " Group By SETUP_ChartOfAccount.ChrtAcc_Id, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Type, ";
                    _Sql += "          SYSTEM_Nature.Natr_SystemTitle ";
                    _Sql += " Order By SETUP_ChartOfAccount.ChrtAcc_Code, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level ";
                }
                else if (ps_TrialActivity == "Closing")
                {
                    _Sql += "   Select SETUP_ChartOfAccount.ChrtAcc_Id, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code,";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title,";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Type, ";
                    _Sql += "          0.0 As VchMas_DrAmount, ";
                    _Sql += "          0.0 As VchMas_CrAmount, ";
                    _Sql += "          ( Select IsNULL( Sum( IsNULL( GL_VchrDetail.VchMas_DrAmount, 0 ) ), 0 ) - ";
                    _Sql += "                   IsNULL( Sum( IsNULL( GL_VchrDetail.VchMas_CrAmount, 0 ) ), 0 ) ";
                    _Sql += "              From GL_VchrDetail, ";
                    _Sql += "                   GL_VchrMaster, ";
                    _Sql += "                   SETUP_Location, ";
                    _Sql += "                   SETUP_ChartOfAccount a ";
                    _Sql += "             Where ( a.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) and ";
                    _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and ";
                    _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                    _Sql += "                   ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( a.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ) And ";
                    _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                    _Sql += "                   ( " + pi_AllDate + " = 1 Or Convert( datetime, Convert( Char, GL_VchrMaster.VchMas_Date, 103 ), 103 ) <= ";
                    _Sql += "                                               Convert( datetime, '" + pdt_DateTo.ToString("dd/MM/yyyy") + "', 103 )  ) ";
                    _Sql += "          ) OpeningBalance, ";
                    _Sql += "          SYSTEM_Nature.Natr_SystemTitle ";
                    _Sql += "     From SETUP_ChartOfAccount, ";
                    _Sql += "          SYSTEM_Nature ";
                    _Sql += "    Where ( " + pi_AllAccCode + " = 1 Or SETUP_ChartOfAccount.ChrtAcc_Id Between '" + ps_AccCodeFrom + "' And '" + ps_AccCodeTo + "' ) And ";
                    _Sql += "          ( SETUP_ChartOfAccount.ChrtAcc_Level <= " + pi_Level.ToString() + " ) And ";
                    _Sql += "          ( SETUP_ChartOfAccount.Natr_Id = SYSTEM_Nature.Natr_Id ) ";
                    _Sql += " Group By SETUP_ChartOfAccount.ChrtAcc_Id, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Type, ";
                    _Sql += "          SYSTEM_Nature.Natr_SystemTitle ";
                    _Sql += " Order By SETUP_ChartOfAccount.ChrtAcc_Code, ";
                    _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level ";
                }

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
        public DataSet IncomeStatement(string ps_Location, int pi_Level, int pi_Year)
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


                _Sql += "   Select SYSTEM_Nature.Natr_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                //_Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          ChrtAcc_Title = CASE ChrtAcc_Level ";
                _Sql += "                               WHEN 1 THEN '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 2 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 3 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 4 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 5 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               ELSE Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                          END,";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level, ";
                _Sql += "          IsNULL( Sum( IsNULL( CurrentYear.VchMas_CrAmount, 0 ) ), 0 ) As CurrentYear_Amount, ";
                _Sql += "          IsNULL( Sum( IsNULL( PreviousYear.VchMas_CrAmount, 0 ) ), 0 ) As PreviousYear_Amount, ";
                _Sql += "          1 As SortOrder ";
                _Sql += "     From SETUP_ChartOfAccount LEFT OUTER JOIN ";
                _Sql += "          ( Select SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "                   GL_VchrDetail.VchMas_CrAmount  ";
                _Sql += "              From SETUP_ChartOfAccount,  ";
                _Sql += "                   GL_VchrDetail,  ";
                _Sql += "                   GL_VchrMaster,  ";
                _Sql += "                   SETUP_Location  ";
                _Sql += "             Where ( SETUP_ChartOfAccount.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) And ";
                _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and ";
                _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                _Sql += "                   ( Year( GL_VchrMaster.VchMas_Date ) = " + pi_Year.ToString() + " ) ";
                _Sql += "          ) CurrentYear On ";
                _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( CurrentYear.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ) ";
                _Sql += "          LEFT OUTER JOIN ";
                _Sql += "          ( Select SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "                   GL_VchrDetail.VchMas_CrAmount ";
                _Sql += "              From SETUP_ChartOfAccount, ";
                _Sql += "                   GL_VchrDetail, ";
                _Sql += "                   GL_VchrMaster, ";
                _Sql += "                   SETUP_Location ";
                _Sql += "             Where ( SETUP_ChartOfAccount.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) And  ";
                _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and ";
                _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                _Sql += "                   ( Year( GL_VchrMaster.VchMas_Date ) = " + pi_Year.ToString() + " - 1 ) ";
                _Sql += "          ) PreviousYear On ";
                _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( PreviousYear.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ), ";
                _Sql += "          SYSTEM_Nature ";
                _Sql += "    Where ( SYSTEM_Nature.Natr_Id = SETUP_ChartOfAccount.Natr_Id ) and ";
                _Sql += "          ( Lower( SYSTEM_Nature.Natr_SystemTitle ) IN ( LOWER( 'Revenue' ) ) ) and ";
                _Sql += "          ( SETUP_ChartOfAccount.ChrtAcc_Level <= " + pi_Level.ToString() + " ) ";
                _Sql += " Group By SYSTEM_Nature.Natr_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code,  ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level ";
                _Sql += " UNION ALL";
                _Sql += "   Select SYSTEM_Nature.Natr_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                //_Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          ChrtAcc_Title = CASE ChrtAcc_Level ";
                _Sql += "                               WHEN 1 THEN '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 2 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 3 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 4 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 5 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               ELSE Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                          END,";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level,";
                _Sql += "          -( IsNULL( Sum( IsNULL( CurrentYear.VchMas_DrAmount, 0 ) ), 0 ) ) As CurrentYear_Amount,";
                _Sql += "          -( IsNULL( Sum( IsNULL( PreviousYear.VchMas_DrAmount, 0 ) ), 0 ) ) As PreviousYear_Amount,";
                _Sql += "          2 As SortOrder";
                _Sql += "     From SETUP_ChartOfAccount LEFT OUTER JOIN ";
                _Sql += "          ( Select SETUP_ChartOfAccount.ChrtAcc_Code,";
                _Sql += "                   GL_VchrDetail.VchMas_DrAmount,";
                _Sql += "                   GL_VchrDetail.VchMas_CrAmount ";
                _Sql += "              From SETUP_ChartOfAccount, ";
                _Sql += "                   GL_VchrDetail,";
                _Sql += "                   GL_VchrMaster,";
                _Sql += "                   SETUP_Location";
                _Sql += "             Where ( SETUP_ChartOfAccount.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) And ";
                _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and";
                _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And";
                _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                _Sql += "                   ( Year( GL_VchrMaster.VchMas_Date ) = " + pi_Year.ToString() + " ) ";
                _Sql += "          ) CurrentYear On";
                _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( CurrentYear.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) )";
                _Sql += "          LEFT OUTER JOIN";
                _Sql += "          ( Select SETUP_ChartOfAccount.ChrtAcc_Code,";
                _Sql += "                   GL_VchrDetail.VchMas_DrAmount,";
                _Sql += "                   GL_VchrDetail.VchMas_CrAmount";
                _Sql += "              From SETUP_ChartOfAccount, ";
                _Sql += "                   GL_VchrDetail, ";
                _Sql += "                   GL_VchrMaster,";
                _Sql += "                   SETUP_Location";
                _Sql += "             Where ( SETUP_ChartOfAccount.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) And";
                _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and";
                _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                _Sql += "                   ( Year( GL_VchrMaster.VchMas_Date ) = " + pi_Year.ToString() + " - 1 )";
                _Sql += "          ) PreviousYear On ";
                _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( PreviousYear.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ),";
                _Sql += "          SYSTEM_Nature";
                _Sql += "    Where ( SYSTEM_Nature.Natr_Id = SETUP_ChartOfAccount.Natr_Id ) and";
                _Sql += "          ( Lower( SYSTEM_Nature.Natr_SystemTitle ) = LOWER( 'Expense' ) ) and";
                _Sql += "          ( SETUP_ChartOfAccount.ChrtAcc_Level <= " + pi_Level.ToString() + " )";
                _Sql += " Group By SYSTEM_Nature.Natr_Title,";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title,";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level ";
                _Sql += " Order By SortOrder, SYSTEM_Nature.Natr_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level ";

                SqlDataAdapter da = new SqlDataAdapter(_Sql, con);
                da.Fill(_ds, "IncomeStatement");

                Connection.ReportConnection("Close");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _ds;
        }
        #endregion

        #region Balance Sheet
        public DataSet BalanceSheet(string ps_Location, int pi_Level, int pi_Year)
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

                #region  ASSETS
                _Sql += "   Select 'ASSETS ' As RowTitle, ";
                _Sql += "          SYSTEM_Nature.Natr_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                //_Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          ChrtAcc_Title = CASE ChrtAcc_Level ";
                _Sql += "                               WHEN 1 THEN '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 2 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 3 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 4 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 5 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               ELSE Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                          END,";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level, ";
                _Sql += "          IsNULL( Sum( IsNULL( CurrentYear.VchMas_CrAmount, 0 ) ), 0 ) As CurrentYear_Amount, ";
                _Sql += "          IsNULL( Sum( IsNULL( PreviousYear.VchMas_CrAmount, 0 ) ), 0 ) As PreviousYear_Amount, ";
                _Sql += "          1 As SortOrder ";
                _Sql += "     From SETUP_ChartOfAccount LEFT OUTER JOIN ";
                _Sql += "          ( Select SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "                   GL_VchrDetail.VchMas_CrAmount  ";
                _Sql += "              From SETUP_ChartOfAccount,  ";
                _Sql += "                   GL_VchrDetail,  ";
                _Sql += "                   GL_VchrMaster,  ";
                _Sql += "                   SETUP_Location  ";
                _Sql += "             Where ( SETUP_ChartOfAccount.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) And ";
                _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and ";
                _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And  ";
                _Sql += "                   ( Year( GL_VchrMaster.VchMas_Date ) = " + pi_Year.ToString() + " ) ";
                _Sql += "          ) CurrentYear On ";
                _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( CurrentYear.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ) ";
                _Sql += "          LEFT OUTER JOIN ";
                _Sql += "          ( Select SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "                   GL_VchrDetail.VchMas_CrAmount ";
                _Sql += "              From SETUP_ChartOfAccount, ";
                _Sql += "                   GL_VchrDetail, ";
                _Sql += "                   GL_VchrMaster, ";
                _Sql += "                   SETUP_Location ";
                _Sql += "             Where ( SETUP_ChartOfAccount.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) And  ";
                _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and ";
                _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                _Sql += "                   ( Year( GL_VchrMaster.VchMas_Date ) = " + pi_Year.ToString() + " - 1 ) ";
                _Sql += "          ) PreviousYear On ";
                _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( PreviousYear.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ), ";
                _Sql += "          SYSTEM_Nature ";
                _Sql += "    Where ( SYSTEM_Nature.Natr_Id = SETUP_ChartOfAccount.Natr_Id ) and ";
                _Sql += "          ( Lower( SYSTEM_Nature.Natr_SystemTitle ) IN ( LOWER( 'Assets' ) ) ) and ";
                _Sql += "          ( SETUP_ChartOfAccount.ChrtAcc_Level <= " + pi_Level.ToString() + " ) ";
                _Sql += " Group By SYSTEM_Nature.Natr_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code,  ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level ";
                #endregion

                SqlDataAdapter da = new SqlDataAdapter(_Sql, con);
                da.Fill(_ds, "Assets");

                //_Sql += " UNION ALL";
                _Sql = "";
                #region LIABILITIES
                _Sql += "   Select 'LIABILITIES' As RowTitle, ";
                _Sql += "          SYSTEM_Nature.Natr_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                //_Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          ChrtAcc_Title = CASE ChrtAcc_Level ";
                _Sql += "                               WHEN 1 THEN '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 2 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 3 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 4 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 5 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               ELSE Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                          END,";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level, ";
                _Sql += "          - ( IsNULL( Sum( IsNULL( CurrentYear.VchMas_CrAmount, 0 ) ), 0 ) ) As CurrentYear_Amount, ";
                _Sql += "          - ( IsNULL( Sum( IsNULL( PreviousYear.VchMas_CrAmount, 0 ) ), 0 ) ) As PreviousYear_Amount, ";
                _Sql += "          2 As SortOrder ";
                _Sql += "     From SETUP_ChartOfAccount LEFT OUTER JOIN ";
                _Sql += "          ( Select SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "                   GL_VchrDetail.VchMas_CrAmount  ";
                _Sql += "              From SETUP_ChartOfAccount,  ";
                _Sql += "                   GL_VchrDetail,  ";
                _Sql += "                   GL_VchrMaster,  ";
                _Sql += "                   SETUP_Location  ";
                _Sql += "             Where ( SETUP_ChartOfAccount.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) And ";
                _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and ";
                _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                _Sql += "                   ( Year( GL_VchrMaster.VchMas_Date ) = " + pi_Year.ToString() + " ) ";
                _Sql += "          ) CurrentYear On ";
                _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( CurrentYear.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ) ";
                _Sql += "          LEFT OUTER JOIN ";
                _Sql += "          ( Select SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "                   GL_VchrDetail.VchMas_CrAmount ";
                _Sql += "              From SETUP_ChartOfAccount, ";
                _Sql += "                   GL_VchrDetail, ";
                _Sql += "                   GL_VchrMaster, ";
                _Sql += "                   SETUP_Location ";
                _Sql += "             Where ( SETUP_ChartOfAccount.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) And  ";
                _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and ";
                _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                _Sql += "                   ( Year( GL_VchrMaster.VchMas_Date ) = " + pi_Year.ToString() + " - 1 ) ";
                _Sql += "          ) PreviousYear On ";
                _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( PreviousYear.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ), ";
                _Sql += "          SYSTEM_Nature ";
                _Sql += "    Where ( SYSTEM_Nature.Natr_Id = SETUP_ChartOfAccount.Natr_Id ) and ";
                _Sql += "          ( Lower( SYSTEM_Nature.Natr_SystemTitle ) IN ( LOWER( 'Liability' ) ) ) and ";
                _Sql += "          ( SETUP_ChartOfAccount.ChrtAcc_Level <= " + pi_Level.ToString() + " ) ";
                _Sql += " Group By SYSTEM_Nature.Natr_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code,  ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level ";
                #endregion
                _Sql += " UNION ALL";
                #region EQUITY
                _Sql += "   Select 'SHAREHOLDER''S EQUITY' As RowTitle, ";
                _Sql += "          SYSTEM_Nature.Natr_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                //_Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                _Sql += "          ChrtAcc_Title = CASE ChrtAcc_Level ";
                _Sql += "                               WHEN 1 THEN '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 2 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 3 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 4 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               WHEN 5 THEN  Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                               ELSE Space( SETUP_ChartOfAccount.ChrtAcc_Level * 3 ) + '[' + SETUP_ChartOfAccount.ChrtAcc_CodeDisplay + '] ' + SETUP_ChartOfAccount.ChrtAcc_Title ";
                _Sql += "                          END,";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level,";
                _Sql += "          IsNULL( Sum( IsNULL( CurrentYear.VchMas_DrAmount, 0 ) ), 0 ) As CurrentYear_Amount,";
                _Sql += "          IsNULL( Sum( IsNULL( PreviousYear.VchMas_DrAmount, 0 ) ), 0 ) As PreviousYear_Amount,";
                _Sql += "          3 As SortOrder";
                _Sql += "     From SETUP_ChartOfAccount LEFT OUTER JOIN ";
                _Sql += "          ( Select SETUP_ChartOfAccount.ChrtAcc_Code,";
                _Sql += "                   GL_VchrDetail.VchMas_DrAmount,";
                _Sql += "                   GL_VchrDetail.VchMas_CrAmount ";
                _Sql += "              From SETUP_ChartOfAccount, ";
                _Sql += "                   GL_VchrDetail,";
                _Sql += "                   GL_VchrMaster,";
                _Sql += "                   SETUP_Location";
                _Sql += "             Where ( SETUP_ChartOfAccount.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) And ";
                _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and";
                _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And";
                _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                _Sql += "                   ( Year( GL_VchrMaster.VchMas_Date ) = " + pi_Year.ToString() + " ) ";
                _Sql += "          ) CurrentYear On";
                _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( CurrentYear.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) )";
                _Sql += "          LEFT OUTER JOIN";
                _Sql += "          ( Select SETUP_ChartOfAccount.ChrtAcc_Code,";
                _Sql += "                   GL_VchrDetail.VchMas_DrAmount,";
                _Sql += "                   GL_VchrDetail.VchMas_CrAmount";
                _Sql += "              From SETUP_ChartOfAccount, ";
                _Sql += "                   GL_VchrDetail, ";
                _Sql += "                   GL_VchrMaster,";
                _Sql += "                   SETUP_Location";
                _Sql += "             Where ( SETUP_ChartOfAccount.ChrtAcc_Id = GL_VchrDetail.ChrtAcc_Id ) And";
                _Sql += "                   ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) and";
                _Sql += "                   ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And ";
                _Sql += "                   ( SETUP_Location.Loc_Id In (" + ps_Location + ") ) And ";
                _Sql += "                   ( Year( GL_VchrMaster.VchMas_Date ) = " + pi_Year.ToString() + " - 1 )";
                _Sql += "          ) PreviousYear On ";
                _Sql += "               ( SETUP_ChartOfAccount.ChrtAcc_Code = Substring( PreviousYear.ChrtAcc_Code, 1, Len( SETUP_ChartOfAccount.ChrtAcc_Code ) ) ),";
                _Sql += "          SYSTEM_Nature";
                _Sql += "    Where ( SYSTEM_Nature.Natr_Id = SETUP_ChartOfAccount.Natr_Id ) and";
                _Sql += "          ( Lower( SYSTEM_Nature.Natr_SystemTitle ) = LOWER( 'Equity' ) ) and";
                _Sql += "          ( SETUP_ChartOfAccount.ChrtAcc_Level <= " + pi_Level.ToString() + " )";
                _Sql += " Group By SYSTEM_Nature.Natr_Title,";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title,";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level ";
                _Sql += " Order By SortOrder, RowTitle, SYSTEM_Nature.Natr_Title, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code, ";
                _Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level ";
                #endregion

                da = new SqlDataAdapter(_Sql, con);
                da.Fill(_ds, "Equity");

                Connection.ReportConnection("Close");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _ds;
        }
        #endregion

        #region Audit Trial
        public DataSet AuditTrail()
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

                _Sql += "   Select * ";
                _Sql += "     From SYSTEM_AuditTrail, ";
                _Sql += "          SYSTEM_Screens, ";
                _Sql += "          SECURITY_User ";
                _Sql += "    Where ( SYSTEM_AuditTrail.Scr_Id = SYSTEM_Screens.Scr_Id ) And ";
                _Sql += "          ( SYSTEM_AuditTrail.User_Id = SECURITY_User.User_Id )  ";
                //_Sql += "          ( SETUP_ChartOfAccount.ChrtAcc_Level <= " + pi_Level.ToString() + " ) ";
                //_Sql += " Group By SYSTEM_Nature.Natr_Title, ";
                //_Sql += "          SETUP_ChartOfAccount.ChrtAcc_Code,  ";
                //_Sql += "          SETUP_ChartOfAccount.ChrtAcc_CodeDisplay, ";
                //_Sql += "          SETUP_ChartOfAccount.ChrtAcc_Title, ";
                //_Sql += "          SETUP_ChartOfAccount.ChrtAcc_Level ";
                _Sql += " Order By SYSTEM_Screens.Scr_Type, ";
                _Sql += "          SYSTEM_Screens.Scr_Id, ";
                _Sql += "          SYSTEM_AuditTrail.AdtTrl_Action, ";
                _Sql += "          SYSTEM_AuditTrail.AdtTrl_EntryId, ";
                _Sql += "          SECURITY_User.User_Title ";

                SqlDataAdapter da = new SqlDataAdapter(_Sql, con);
                da.Fill(_ds, "AuditTrail");

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
