using System;
using System.Data;
using System.Data.SqlClient;
using SCMSApp.DAO;

namespace SCMSApp.Repositories
{
    public class SCMSRepository : BaseDAO
    {

        public DataTable GetGL_VchrMastersData()
        {
            SqlConnection cn = null;
            DataTable dt = new DataTable("GL_VchrMaster");
            try
            {
                cn = GetConnection();

                String Query = "Select * from GL_VchrMaster where SyncStatus=0"; 
                SqlDataAdapter da = new SqlDataAdapter(Query, cn);
                da.Fill(dt);

                cn.Close();
                
            }
            catch
            {
               
            }
            return dt;
        }


        public DataTable GetGL_VchrDetailsData()
        {
            DataTable dt = new DataTable("GL_VchrDetail");
            SqlConnection cn = null;
            try
            {
                cn = GetConnection();
                String Query = "Select GL_VchrDetail.* from GL_VchrDetail";
                Query += " INNER JOIN GL_VchrMaster On GL_VchrDetail.VchMas_Id=GL_VchrMaster.VchMas_Id";
                Query += " where GL_VchrMaster.SyncStatus=0";

                SqlDataAdapter da = new SqlDataAdapter(Query, cn);
                da.Fill(dt);
                cn.Close();
                
            }
            catch
            {
                
            }
            return dt;
        }

        public void SaveOrUpdateGL_VchrMaster(DataRow drow)
        {
            try
            {
                  string VchMas_Id = drow["VchMas_Id"].ToString();

                  if (CheckIfExist(VchMas_Id))
                  {
                      UpdateInfo(drow);
                  }
                  else {

                      InsertInfo(drow);
                  }

                  UpdateVoucherMasterStatus(VchMas_Id);

            }
            catch
            {

            }
            
        }

        public void SaveGL_VchrDetails(DataRow drow)
        {
            
            string VchDet_Id = drow["VchDet_Id"].ToString();
            string VchMas_Id = drow["VchMas_Id"].ToString();
            string ChrtAcc_Id = drow["ChrtAcc_Id"].ToString();
            string VchMas_DrAmount = drow["VchMas_DrAmount"].ToString();
            string VchMas_CrAmount = drow["VchMas_CrAmount"].ToString();
            string VchDet_Remarks = drow["VchDet_Remarks"].ToString();
            



            SqlConnection conn = GetRemoteConnection();
            string sql = " INSERT INTO GL_VchrDetail (VchDet_Id,VchMas_Id,ChrtAcc_Id,VchMas_DrAmount,VchMas_CrAmount,VchDet_Remarks) ";
            sql += " VALUES (@VchDet_Id,@VchMas_Id,@ChrtAcc_Id,@VchMas_DrAmount,@VchMas_CrAmount,@VchDet_Remarks)";
            try
            {
               
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@VchDet_Id", VchDet_Id);
                cmd.Parameters.AddWithValue("@VchMas_Id", VchMas_Id);
                cmd.Parameters.AddWithValue("@ChrtAcc_Id", ChrtAcc_Id);
                cmd.Parameters.AddWithValue("@VchMas_DrAmount", VchMas_DrAmount);
                cmd.Parameters.AddWithValue("@VchMas_CrAmount", VchMas_CrAmount);
                cmd.Parameters.AddWithValue("@VchDet_Remarks", VchDet_Remarks);
                                
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);


            }
            finally
            {
                conn.Close();
            }

        }

        private void InsertInfo(DataRow drow)
        {
            string VchMas_Id = drow["VchMas_Id"].ToString();
            string VchMas_Code = drow["VchMas_Code"].ToString();
            string Cmp_Id = drow["Cmp_Id"].ToString();
            string Loc_Id = drow["Loc_Id"].ToString();
            string VchrType_Id = drow["VchrType_Id"].ToString();
            string VchMas_Remarks = drow["VchMas_Remarks"].ToString();
            string VchMas_Status = drow["VchMas_Status"].ToString();
            
            string VchMas_EnteredBy = drow["VchMas_EnteredBy"].ToString();
            string VchMas_EnteredDate = drow["VchMas_EnteredDate"].ToString();
            string VchMas_ApprovedBy = drow["VchMas_ApprovedBy"].ToString();
            bool SyncStatus = Convert.ToBoolean(drow["SyncStatus"]);
           
            SqlConnection conn = GetRemoteConnection();
            string sql = " INSERT INTO GL_VchrMaster (VchMas_Id,VchMas_Code,Cmp_Id,Loc_Id,VchrType_Id,VchMas_Remarks,VchMas_Status,VchMas_EnteredBy,VchMas_EnteredDate,VchMas_ApprovedBy,SyncStatus) ";
            sql += " VALUES (@VchMas_Id,@VchMas_Code,@Cmp_Id,@Loc_Id,@VchrType_Id,@VchMas_Remarks,@VchMas_Status,@VchMas_EnteredBy,@VchMas_EnteredDate,@VchMas_ApprovedBy,@SyncStatus)";
            try
            {
              
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@VchMas_Id", VchMas_Id);
                cmd.Parameters.AddWithValue("@VchMas_Code", VchMas_Code);
                cmd.Parameters.AddWithValue("@Cmp_Id", Cmp_Id);
                cmd.Parameters.AddWithValue("@Loc_Id", Loc_Id);
                cmd.Parameters.AddWithValue("@VchrType_Id", VchrType_Id);
                cmd.Parameters.AddWithValue("@VchMas_Remarks", VchMas_Remarks);
                cmd.Parameters.AddWithValue("@VchMas_Status", VchMas_Status);
                cmd.Parameters.AddWithValue("@VchMas_EnteredBy", VchMas_EnteredBy);
                cmd.Parameters.AddWithValue("@VchMas_EnteredDate", VchMas_EnteredDate);
                cmd.Parameters.AddWithValue("@VchMas_ApprovedBy", VchMas_ApprovedBy);
                cmd.Parameters.AddWithValue("@SyncStatus", SyncStatus);


                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);


            }
            finally
            {
                conn.Close();
            }


        }


        private void UpdateInfo(DataRow drow)
        {

            string VchMas_Id = drow["VchMas_Id"].ToString();
            string VchMas_Code = drow["VchMas_Code"].ToString();
            string Cmp_Id = drow["Cmp_Id"].ToString();
            string Loc_Id = drow["Loc_Id"].ToString();
            string VchrType_Id = drow["VchrType_Id"].ToString();
            string VchMas_Remarks = drow["VchMas_Remarks"].ToString();
            string VchMas_Status = drow["VchMas_Status"].ToString();
            string VchMas_EnteredBy = drow["VchMas_EnteredBy"].ToString();
            string VchMas_EnteredDate = drow["VchMas_EnteredDate"].ToString();
            string VchMas_ApprovedBy = drow["VchMas_ApprovedBy"].ToString();
            bool SyncStatus = Convert.ToBoolean(drow["SyncStatus"]);
            

            SqlConnection conn = GetRemoteConnection();

            string sql = "UPDATE GL_VchrMaster SET VchMas_Code = @VchMas_Code, Loc_Id = @Loc_Id, VchrType_Id = @VchrType_Id, VchMas_Remarks = @VchMas_Remarks, VchMas_Status = @VchMas_Status";
            sql += ", VchMas_EnteredBy = @VchMas_EnteredBy, VchMas_EnteredDate = @VchMas_EnteredDate, VchMas_ApprovedBy = @VchMas_ApprovedBy,SyncStatus=@SyncStatus WHERE VchMas_Id = @VchMas_Id";
            try
            {
               
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@VchMas_Id", VchMas_Id);
                cmd.Parameters.AddWithValue("@VchMas_Code", VchMas_Code);
                cmd.Parameters.AddWithValue("@Cmp_Id", Cmp_Id);
                cmd.Parameters.AddWithValue("@Loc_Id", Loc_Id);
                cmd.Parameters.AddWithValue("@VchrType_Id", VchrType_Id);
                cmd.Parameters.AddWithValue("@VchMas_Remarks", VchMas_Remarks);
                cmd.Parameters.AddWithValue("@VchMas_Status", VchMas_Status);
                cmd.Parameters.AddWithValue("@VchMas_EnteredBy", VchMas_EnteredBy);
                cmd.Parameters.AddWithValue("@VchMas_EnteredDate", VchMas_EnteredDate);
                cmd.Parameters.AddWithValue("@VchMas_ApprovedBy", VchMas_ApprovedBy);
                cmd.Parameters.AddWithValue("@SyncStatus", SyncStatus);
                
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Update Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                conn.Close();
            }


        }

        private void UpdateVoucherMasterStatus(string id) {

            SqlConnection conn = GetConnection();
            DataTable dt = new DataTable();
            try
            {

                string sql = "UPDATE GL_VchrMaster SET SyncStatus = @SyncStatus WHERE VchMas_Id=@id";
              
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@SyncStatus", "1");

                cmd.ExecuteNonQuery();
               

            }
            catch (Exception){
                
            }
            finally
            {
                conn.Close();
            }
        
        }

        private bool CheckIfExist(string id)
        {

            bool check = false;
            SqlConnection conn = GetRemoteConnection();
            DataTable dt = new DataTable();
            try
            {
               
                String sql = "Select * from GL_VchrMaster where VchMas_Id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    check = true;
                    String sqlDel = "Delete from GL_VchrDetail where VchMas_Id=@id";
                    SqlCommand cmdDel = new SqlCommand(sqlDel, conn);
                    cmdDel.Parameters.AddWithValue("@id", id);
                    cmdDel.CommandType = CommandType.Text;
                    cmdDel.ExecuteNonQuery();
                }
                
            }
            catch (System.Data.SqlClient.SqlException)
            {
                check = false;
            }
            finally
            {
                conn.Close();
            }

            return check;
        }
    }  
       
}
