using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCMSDataLayer.DB;
using System.Data;
using System.Data.SqlClient;

namespace SCMSDataLayer
{
    public enum CodeInitialization
    {
        Year = 1,
        Month = 2
    }

    public enum CalenderLevel
    {
        Yearly = 1,
        Monthly = 2,
        Fornigthtly = 3,
        Weekly = 4,
        Daily = 5
    }

    public static class DALCommon
    {
        public static Int32 AutoCodeGeneration(String ps_TableName)
        {
            string _Sql = "";
            Int32 _ReturnValue = 0;
            DataSet _ds = new DataSet();

            try
            {
                var dbSCMS = Connection.Create();
                SqlConnection con = (SqlConnection)dbSCMS.Connection;
                con.Open();

                _Sql += " Select CodeGen_AutoTag ";
                _Sql += "   From SYSTEM_CodeGeneration ";
                _Sql += "  Where ( Lower( SYSTEM_CodeGeneration.CodeGen_TableName ) = Lower( '" + ps_TableName + "' ) ) ";

                SqlCommand cmd = new SqlCommand(_Sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(_ds);

                if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0)
                {
                    if (_ds.Tables[0].Rows[0]["CodeGen_AutoTag"] != null &&
                        _ds.Tables[0].Rows[0]["CodeGen_AutoTag"].ToString() != "")
                    {
                        _ReturnValue = Convert.ToInt32(_ds.Tables[0].Rows[0]["CodeGen_AutoTag"].ToString());
                    }
                }

                con.Close();
            }
            catch
            {
                return _ReturnValue;
            }

            return _ReturnValue;
        }

        public static String GetMaximumCode(String ps_TableName)
        {
            string _Sql = "", _ColumnName = "", _ReturnValue = "";
            Int32 _AutoCodeTag = 0, _CodeLength = 0, _MaxCode = 0;
            DataSet _ds = new DataSet();

            try
            {
                var dbSCMS = Connection.Create();
                SqlConnection con = (SqlConnection)dbSCMS.Connection;
                con.Open();

                _Sql += " Select * ";
                _Sql += "   From SYSTEM_CodeGeneration ";
                _Sql += "  Where ( Lower( SYSTEM_CodeGeneration.CodeGen_TableName ) = Lower( '" + ps_TableName + "' ) ) ";

                SqlCommand cmd = new SqlCommand(_Sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(_ds);

                if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0)
                {
                    if (_ds.Tables[0].Rows[0]["CodeGen_ColumnName"] != null &&
                        _ds.Tables[0].Rows[0]["CodeGen_ColumnName"].ToString() != "")
                    {
                        _ColumnName = _ds.Tables[0].Rows[0]["CodeGen_ColumnName"].ToString();
                    }

                    if (_ds.Tables[0].Rows[0]["CodeGen_AutoTag"] != null &&
                        _ds.Tables[0].Rows[0]["CodeGen_AutoTag"].ToString() != "")
                    {
                        _AutoCodeTag = Convert.ToInt32(_ds.Tables[0].Rows[0]["CodeGen_AutoTag"].ToString());
                    }

                    if (_ds.Tables[0].Rows[0]["CodeGen_Length"] != null &&
                        _ds.Tables[0].Rows[0]["CodeGen_Length"].ToString() != "")
                    {
                        _CodeLength = Convert.ToInt32(_ds.Tables[0].Rows[0]["CodeGen_Length"].ToString());
                    }

                    //if (ps_TableName == "SETUP_VoucherType")
                    //{
                    //    VoucherTypeCodeLength = _CodeLength;
                    //}

                    _Sql = "";
                    _Sql += " Select IsNULL( Max( IsNULL( " + _ColumnName + ", 0 ) ), 0 ) + 1 ";
                    _Sql += "   From " + ps_TableName;

                    cmd = new SqlCommand(_Sql, con);
                    _MaxCode = (Int32)cmd.ExecuteScalar();
                    _ReturnValue = _MaxCode.ToString().PadLeft(_CodeLength, '0');
                }

                con.Close();
            }
            catch
            {
                return _ReturnValue;
            }

            return _ReturnValue;
        }

        public static String GetMaxVoucherCode(string ps_TableName, string ps_VoucherTypeId, string ps_VoucherTypePrefix, string ps_LocationId)
        {
            string _Sql = "", _ReturnValue = "";
            Int32 _CodeLength = 0, _MaxCode = 0;
            DataSet _ds = new DataSet();

            try
            {
                var dbSCMS = Connection.Create();
                SqlConnection con = (SqlConnection)dbSCMS.Connection;
                con.Open();

                _Sql += " Select * ";
                _Sql += "   From SYSTEM_CodeGeneration ";
                _Sql += "  Where ( Lower( SYSTEM_CodeGeneration.CodeGen_TableName ) = Lower( '" + ps_TableName + "' ) ) ";

                SqlCommand cmd = new SqlCommand(_Sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(_ds);

                if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0)
                {
                    if (_ds.Tables[0].Rows[0]["CodeGen_Length"] != null &&
                        _ds.Tables[0].Rows[0]["CodeGen_Length"].ToString() != "")
                    {
                        _CodeLength = Convert.ToInt32(_ds.Tables[0].Rows[0]["CodeGen_Length"].ToString());
                    }

                    _Sql = "";
                    _Sql += " Select IsNULL( Max( SubString( IsNULL( GL_VchrMaster.VchMas_Code, 0 ), ( Len( '" + ps_VoucherTypePrefix + "' ) + Len( " + Convert.ToInt32(ps_VoucherTypeId) + " ) + Len( " + Convert.ToInt32(ps_LocationId) + ") + 1 ), 20 ) ), 0 ) + 1 ";
                    _Sql += "   From GL_VchrMaster ";
                    _Sql += "  Where ( GL_VchrMaster.VchrType_Id = '" + ps_VoucherTypeId + "' ) And ";
                    _Sql += "        ( GL_VchrMaster.Loc_Id = '" + ps_LocationId + "' ) ";

                    cmd = new SqlCommand(_Sql, con);
                    _MaxCode = (Int32)cmd.ExecuteScalar();
                    if (_MaxCode != null && _MaxCode != 0)
                    {
                        _ReturnValue = ps_VoucherTypePrefix + Convert.ToInt32(ps_LocationId) + Convert.ToInt32(ps_VoucherTypeId) + _MaxCode.ToString().PadLeft(_CodeLength, '0');
                    }
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _ReturnValue;
        }

        public static String GetMaxVoucherId(string ps_YearPrefix)
        {
            string _Sql = "", _ReturnValue = "";
            Int32 _CodeLength = 10, _MaxId = 0;

            try
            {
                var dbSCMS = Connection.Create();
                SqlConnection con = (SqlConnection)dbSCMS.Connection;
                con.Open();

                _Sql += " Select IsNULL( Max( SubString( IsNULL( GL_VchrMaster.VchMas_Id, 0 ), ( Len( '" + ps_YearPrefix + "' ) + 1 ), 10) ), 0 ) + 1 ";
                _Sql += "   From GL_VchrMaster ";
                _Sql += "  Where ( Left( GL_VchrMaster.VchrType_Id, Len( '" + ps_YearPrefix + "' ) ) = '" + ps_YearPrefix + "' )";

                SqlCommand cmd = new SqlCommand(_Sql, con);
                cmd = new SqlCommand(_Sql, con);
                _MaxId = (Int32)cmd.ExecuteScalar();

                if (_MaxId != null && _MaxId != 0)
                {
                    _ReturnValue = ps_YearPrefix + _MaxId.ToString().PadLeft((_CodeLength - ps_YearPrefix.Length), '0');
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _ReturnValue;
        }

        //public static int VoucherTypeCodeLength
        //{
        //    get;
        //    set;
        //}

        //public static int ModuleId
        //{
        //    get;
        //    set;
        //}

        //public static String aGetMaximumCode(String ps_TableName, String ps_ColumnName)
        //{
        //    string _Sql = "";
        //    DataSet _ds = new DataSet();

        //    try
        //    {
        //        var dbSCMS = Connection.Create();
        //        SqlConnection con = (SqlConnection)dbSCMS.Connection;
        //        con.Open();

        //        _Sql += " Select SYSTEM_CodeGeneration.CodeGen_AutoTag ";
        //        _Sql += "   From SYSTEM_CodeGeneration ";
        //        _Sql += "  Where ( Lower( SYSTEM_CodeGeneration.CodeGen_TableName ) = Lower( " + ps_TableName + " ) ) ";


        //        SqlCommand cmd = new SqlCommand(_Sql, con);
        //        //_ds =  cmd.


        //        int MaxCode = (int)cmd.ExecuteScalar();
        //        con.Close();
        //        return MaxCode.ToString().PadLeft(5, '0');
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}

        //public static DataTable GetInstallationParameter(string TableName)
        //{
        //    string Prefix;
        //    int NumericCode = 0;
        //    int PrefixLength = 0;
        //    int CodeTotalLength = 0;
        //    /////////// Create Data Table/////////
        //    DataTable _Installdt = new DataTable();
        //    _Installdt.Columns.Add("AUTOTAG");
        //    _Installdt.Columns.Add("PREFIX");
        //    _Installdt.Columns.Add("LENGTH");
        //    _Installdt.Columns.Add("PREFIXLENGTH");
        //    DataRow drInstall = _Installdt.NewRow();

        //    IBLInstallationParameter _bl = (IBLInstallationParameter)SetupBLFactory.GetInstallationParameter();
        //    DataSet _dsTableInfo = (DataSet)_bl.GetTableInformation(TableName).GetMaster();

        //    if (_dsTableInfo != null)
        //    {
        //        if (_dsTableInfo.Tables[0].Rows.Count > 0)
        //        {
        //            CodeTotalLength = Convert.ToInt32(_dsTableInfo.Tables[0].Rows[0]["LENGTH"]);
        //            if (Convert.ToInt32(_dsTableInfo.Tables[0].Rows[0]["AUTOTAG"]) == 1)
        //            {
        //                drInstall["AUTOTAG"] = "true";
        //            }
        //            else
        //            {
        //                drInstall["AUTOTAG"] = "false";
        //            }
        //            if (_dsTableInfo.Tables[0].Rows[0]["PREFIX"] != null && _dsTableInfo.Tables[0].Rows[0]["PREFIX"].ToString() != string.Empty)
        //            {
        //                Prefix = _dsTableInfo.Tables[0].Rows[0]["PREFIX"].ToString();
        //                PrefixLength = Prefix.Length;
        //                NumericCode = CodeTotalLength - PrefixLength;
        //                drInstall["LENGTH"] = NumericCode;
        //                drInstall["PREFIX"] = Prefix;
        //                drInstall["PREFIXLENGTH"] = PrefixLength;
        //            }
        //            else
        //            {
        //                drInstall["LENGTH"] = CodeTotalLength;
        //                drInstall["PREFIX"] = "";
        //                drInstall["PREFIXLENGTH"] = 0;
        //            }
        //            _Installdt.Rows.Add(drInstall);
        //            return _Installdt;
        //        }
        //    }
        //    return _Installdt;
        //}

        //public static string GenerateCode(string TableName, string ColumnName)
        //{
        //    string code = string.Empty;
        //    string Prefix = "";
        //    int NumericCode = 0;
        //    int CodeTotalLength = 0;
        //    int PrefixLength = 0;
        //    IBLInstallationParameter _bl = (IBLInstallationParameter)SetupBLFactory.GetInstallationParameter();
        //    DataSet _dsTableInfo = (DataSet)_bl.GetTableInformation(TableName).GetMaster();

        //    if (_dsTableInfo != null)
        //    {
        //        if (_dsTableInfo.Tables[0].Rows.Count > 0)
        //        {
        //            CodeTotalLength = Convert.ToInt32(_dsTableInfo.Tables[0].Rows[0]["LENGTH"]);
        //            if (_dsTableInfo.Tables[0].Rows[0]["PREFIX"] != null && _dsTableInfo.Tables[0].Rows[0]["PREFIX"].ToString() != string.Empty)
        //            {
        //                Prefix = _dsTableInfo.Tables[0].Rows[0]["PREFIX"].ToString();
        //                PrefixLength = Prefix.Length;
        //                NumericCode = CodeTotalLength - PrefixLength;

        //                code = MakeCode(TableName, ColumnName, PrefixLength, Prefix, NumericCode, CodeTotalLength);
        //                return code;

        //            }
        //            else
        //            {
        //                code = MakeCodeWithOutPrefix(TableName, ColumnName, CodeTotalLength);
        //                return code;
        //            }
        //        }
        //    }
        //    return code;

        //}

        //static string MakeCode(string TableName, string ColumnName, int PrefixLength, string Prefix, int NumericCode, int CodeTotalLength)
        //{
        //    string Code = string.Empty;
        //    string MaxCode = string.Empty;
        //    string Zeros = "000000000000";
        //    int NumericValue;
        //    string MakeCode = string.Empty; ;

        //    IBLInstallationParameter _bl = (IBLInstallationParameter)SetupBLFactory.GetInstallationParameter();
        //    string _sMaxCode = (string)_bl.GetMaxCode(TableName, ColumnName).GetMaster();

        //    if (_sMaxCode != "1")
        //    {
        //        MaxCode = _sMaxCode;
        //        NumericValue = Convert.ToInt32(MaxCode.Substring(PrefixLength));
        //        NumericValue += 1;
        //        Code = Prefix + NumericValue;
        //        int Remaining = CodeTotalLength - Code.Length;
        //        MakeCode = Zeros.Substring(0, Remaining);
        //        Code = Prefix + MakeCode + NumericValue;
        //        return Code;
        //    }
        //    else
        //    {

        //        MakeCode = Zeros.Substring(0, NumericCode - 1);
        //        MakeCode = MakeCode + "1";
        //        Code = Prefix + MakeCode;
        //        return Code;
        //    }


        //}
    }

}
