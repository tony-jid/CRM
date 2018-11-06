using CRM.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public static class DashboardHelper
    {
        public static Dictionary<int, string[]> DateArgumentFields = new Dictionary<int, string[]>() {
            { (int)EnumDateRangeType.Day, new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" } }
            , { (int)EnumDateRangeType.Month, new string[] { "Week 1", "Week 2", "Week 3", "Week 4" } }
            , { (int)EnumDateRangeType.Year, new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" } }
        };
    }
}
