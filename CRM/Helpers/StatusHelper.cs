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

        public static string GetHtmlSmallBadge(int statusId)
        {
            EnumState state = (EnumState)statusId;

            switch(state)
            {
                case EnumState.LeadNew:
                    return GetHtmlSmallBadge("New", "danger");
                case EnumState.LeadAssigned:
                    return GetHtmlSmallBadge("Assigned", "success");
                case EnumState.LeadReAssigned:
                    return GetHtmlSmallBadge("Reassigned", "success");
                case EnumState.LeadAssignmentConsidering:
                    return GetHtmlSmallBadge("Considering", "warning");
                default:
                    return String.Empty;
            }
        }

        private static string GetHtmlSmallBadge(string statusText, string color)
        {
            return String.Format(StatusHelper.SMALL_BADGE_FORMAT, color, statusText);
        }
    }
}
