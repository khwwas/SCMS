using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMS.Reports
{
    public static class ReportParameters
    {
        public static string ReportName
        {
            get;
            set;
        }
        public static string Location
        {
            get;
            set;
        }
        public static int Level
        {
            get;
            set;
        }

        public static int AllDate
        {
            get;
            set;
        }
        public static DateTime DateFrom
        {
            get;
            set;
        }
        public static DateTime DateTo
        {
            get;
            set;
        }

        public static int AllAccCode
        {
            get;
            set;
        }
        public static string AccCodeFrom
        {
            get;
            set;
        }
        public static string AccCodeTo
        {
            get;
            set;
        }
    }
}