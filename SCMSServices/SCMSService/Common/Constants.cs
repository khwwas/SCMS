﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCMSApp.Common
{
    public class Constants
    {

        public const String CONNECTION_STRING = "Data Source=localhost;Initial Catalog=SCMS;user id =sa; password=Boingo36!;";
        public const String REMOTE_CONNECTION_STRING = "Data Source=localhost;Initial Catalog=SCMSRR;user id =sa; password=Boingo36!;";
        public const String XMLFILE = "C://DotNet/MyPersnalProjects/Projects/VchMasterDetails.xml";
        public const String SERVICE_DISPALY_NAME = "SCMS Service";
        public const String SERVICE_NAME = "SCMS Service";
        public const int SERVICE_TIME_INTERVAL = 900000;  //1000 means 1 seconds
      
   }
}
