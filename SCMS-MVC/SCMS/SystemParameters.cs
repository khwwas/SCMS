using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMS
{
    public static class SystemParameters
    {
        public static string CurrentCmpId
        {
            get;
            set;
        }
        public static string CurrentCmpCode
        {
            get;
            set;
        }
        public static string CurrentCmpName
        {
            get;
            set;
        }
        public static string CurrentUserId
        {
            get;
            set;
        }
        public static string CurrentUserGrpId
        {
            get;
            set;
        }

        public static string CurrentUserCode
        {
            get;
            set;
        }
        public static string CurrentUserName
        {
            get;
            set;
        }
        public static int ModuleId
        {
            get;
            set;
        }
        public static string ModuleDesc
        {
            get;
            set;
        }
        public static string ModuleAbbr
        {
            get;
            set;
        }
    }
}