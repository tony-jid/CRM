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
                    return GetHtmlSmallBadgeNewStatus();
                default:
                    return String.Empty;
            }
        }

        public static string GetHtmlSmallBadgeNewStatus()
        {
            return String.Format(StatusHelper.SMALL_BADGE_FORMAT, "danger", "New");
        }
    }
}
