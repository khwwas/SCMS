using System;
using System.Data;
using System.Data.SqlClient;
using SCMSApp.Common;

namespace SCMSApp.DAO
{
    public abstract class BaseDAO
    {
        
        public static string LOCAL_CONN_STRING = Constants.CONNECTION_STRING;
        public static string REMOTE_CONN_STRING = Constants.REMOTE_CONNECTION_STRING;

        public BaseDAO()
        {
            //Constructor
        }

        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(LOCAL_CONN_STRING);
            if (con != null && con.State != ConnectionState.Open)
            {
                con.Open();
            }
            return con;
        }

        public static SqlConnection GetRemoteConnection()
        {
            SqlConnection con = new SqlConnection(REMOTE_CONN_STRING);
            if (con != null && con.State != ConnectionState.Open)
            {
                con.Open();
            }
            return con;
        }

        public static void CloseReader(SqlDataReader reader)
        {
            try
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
            catch (Exception)
            {

            }
        }
        public static void CloseConnection(SqlConnection cn)
        {
            try
            {
                if (cn != null && cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
