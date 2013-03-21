using System;
using System.Diagnostics;
using System.ServiceProcess;
using SCMSApp.Common;
using SCMSApp.Business;

namespace SCMSService
{
    public partial class SCMSService : ServiceBase
    {

        System.Timers.Timer objTimer = new System.Timers.Timer();
        
        public SCMSService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            objTimer.Enabled = true;
            objTimer.AutoReset = true;
            objTimer.Interval = Constants.SERVICE_TIME_INTERVAL;
            objTimer.Elapsed += new System.Timers.ElapsedEventHandler(objTimer_Elapsed);
            objTimer.Start();

            EventLog.WriteEntry("iCM application service started.");

        }

        protected void objTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                new ImportExportGLVoucherData().CheckStatusAndProcessData();
            }
            catch (Exception)
            {

            }

        }

        protected override void OnStop()
        {

            objTimer.Stop();
            objTimer.Enabled = false;

        }
    }
}
