using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace SCMSDataLayer.DB
{
    public static class Connection
    {
        public static SCMSDataContext Create()
        {
            try
            {
                String ConString = WebConfigurationManager.ConnectionStrings["dbSCMS"].ConnectionString;
                SCMSDataContext db = new SCMSDataContext(@ConString);
                return db;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static SqlConnection ReportConnection(string _sCase)
        {
            SqlConnection _SQLConnection = new SqlConnection();
            string ConnectionString = "";

            try
            {
                ConnectionString = WebConfigurationManager.ConnectionStrings["dbSCMS"].ConnectionString;
                _SQLConnection = new SqlConnection(ConnectionString);

                if (_sCase.ToLower() == "Open".ToLower())
                {
                    if (_SQLConnection.State != System.Data.ConnectionState.Open)
                    {
                        _SQLConnection.Open();
                    }
                }
                else if (_sCase.ToLower() == "Close".ToLower())
                {
                    if (_SQLConnection.State == System.Data.ConnectionState.Open)
                    {
                        _SQLConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _SQLConnection;
        }

    }
}
