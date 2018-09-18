using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Enum;

namespace CRM.Helpers
{
    public static class StatusHelper
    {
        public const string SMALL_BADGE_FORMAT = "<span class=\"badge badge-{0} badge-status\">{1}</span>";

        public static string GetHtmlBadge(string statusId, string statusName)
        {
            if (statusId.Equals(EnumState.SL1.ToString()))
                return GetHtmlSmallBadge(statusName, "danger");

            else if (statusId.Equals(EnumState.SL2.ToString()))
                return GetHtmlSmallBadge(statusName, "success");

            else if (statusId.Equals(EnumState.SL3.ToString()))
                return GetHtmlSmallBadge(statusName, "success");

            else if (statusId.Equals(EnumState.SLA1.ToString()))
                return GetHtmlSmallBadge(statusName, "warning");

            else if (statusId.Equals(EnumState.SLA2.ToString()))
                return GetHtmlSmallBadge(statusName, "success");

            else if (statusId.Equals(EnumState.SLA3.ToString()))
                return GetHtmlSmallBadge(statusName, "danger");

            else if (statusId.Equals(EnumState.SLA4.ToString()))
                return GetHtmlSmallBadge(statusName, "primary");

            else if (statusId.Equals(EnumState.SLA5.ToString()))
                return GetHtmlSmallBadge(statusName, "primary");

            return GetHtmlSmallBadge("Unknown", "danger");
        }

        private static string GetHtmlSmallBadge(string statusText, string color)
        {
            return String.Format(StatusHelper.SMALL_BADGE_FORMAT, color, statusText);
        }
    }
}
