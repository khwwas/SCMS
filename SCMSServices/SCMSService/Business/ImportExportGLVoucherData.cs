using System;
using System.Collections.Generic;
using System.Threading;
using SCMSApp.Repositories;
using System.Data;
using System.Data.SqlClient;
using SCMSApp.Common;

namespace SCMSApp.Business
{
    public class ImportExportGLVoucherData
    {
      
        public void ProcessData()
        {
            ThreadStart job = new ThreadStart(CheckStatusAndProcessData);
            Thread thread = new Thread(job);
            thread.Start();
        }

        public void CheckStatusAndProcessData()
        {

                DataTable dtGL_Master = new SCMSRepository().GetGL_VchrMastersData();
                DataTable dtGL_Details = new SCMSRepository().GetGL_VchrDetailsData();
                if (dtGL_Master != null && dtGL_Master.Rows.Count > 0)
                {
                       // ExportVouchersDataToXML(dtGL_Master, dtGL_Details);
                       foreach (DataRow drow in dtGL_Master.Rows)
                        {
                            int VchMas_Id = Convert.ToInt32(drow["VchMas_Id"]);

                            new SCMSRepository().SaveOrUpdateGL_VchrMaster(drow); 
                             
                            DataView dv_Vsdetail = new DataView(dtGL_Details , "VchMas_Id=" + VchMas_Id, "", DataViewRowState.CurrentRows);
                            DataTable dtFilteredData = dv_Vsdetail.ToTable();
                            
                            if (dtFilteredData != null && dtFilteredData.Rows.Count > 0)
                            {
                                foreach (DataRow dtrow in dtFilteredData.Rows)
                                {
                                    new SCMSRepository().SaveGL_VchrDetails(dtrow);
                                }

                            }
                       }
                }
          }


        public void ExportVouchersDataToXML(DataTable dtGL_Master, DataTable dtGL_Details)
        {
           
            try
            {
                System.Data.DataSet Mas_DetailsDataSet = new System.Data.DataSet("VchMasterDetails");
                Mas_DetailsDataSet.Tables.Add(dtGL_Master);
                Mas_DetailsDataSet.Tables.Add(dtGL_Details);
                
                if (dtGL_Details.Rows.Count > 0)
                {
                    System.Data.DataRelation Mas_DetailsDataRelation = Mas_DetailsDataSet.Relations.Add("FRRelation", Mas_DetailsDataSet.Tables["GL_VchrMaster"].Columns["VchMas_Id"], Mas_DetailsDataSet.Tables["GL_VchrDetail"].Columns["VchMas_Id"]);
                    Mas_DetailsDataRelation.Nested = true;
                }

                Mas_DetailsDataSet.WriteXml(Constants.XMLFILE, System.Data.XmlWriteMode.WriteSchema);
            
            }
            catch(Exception)
            {
              
            }
     
        }
     }
 }
