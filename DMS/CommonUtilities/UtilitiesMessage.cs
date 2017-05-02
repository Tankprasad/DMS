using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.CommonUtilities
{
    public static class UtilitiesMessage
    {
        public static string validationError = "Validation Error !";
        public static string save = "Successfully Saved !";
        public static string savefailed = "Save Failed ! Please provide required data";
        public static string edit = "Successfully Updated !";
        public static string editfailed = "Update Failed !";
        public static string delete = "Successfully Deleted !";
        public static string deletefailed = "Deletion Failed !";
        public static string StartDateGreaterthenEndDate = "You Must be Select Start Date less then End Date !";
        public static string HasValues = "Check Start And End Date and Insert Unique Date Which is Not Insert In Database !";
        public static string AlreadyExits = "This is Already Exit !";
        public static string StartTimeGreaterThenEndTime = "You Must be Select Start Time less then End Time !";
        public static string BalanceLeaveisGreterThenDays = "You can not Access Grater Then Balance Leave Please Try Again Other Leave !";
    }
}