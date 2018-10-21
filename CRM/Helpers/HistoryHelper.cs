using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public static class HistoryHelper
    {
        public const string HISTORY_TAG_FORMAT = @"
<button type='button' class='btn btn-xs btn-secondary' data-toggle='tooltip' data-placement='auto' title='{0}'>
    <span class='batch-icon batch-icon-search'></span>
    History
</button>";
        public const string HISTORY_LINE_FORMAT = "On {0} at {1}, {2} by <u>{3}</u>";
        public const string HISTORY_SEPARATOR = "<br/>";

        public static string GetHtmlHistoryLine(DateTime when, string action, string actor)
        {
            return String.Format(HistoryHelper.HISTORY_LINE_FORMAT, when.ToShortDateString(), when.ToShortTimeString(), action.Replace("_", " "), actor);
        }

        public static string GetHtmlHistoryTag(List<string> historyLines)
        {
            //return String.Format(HistoryHelper.HISTORY_TAG_FORMAT, String.Join(HistoryHelper.HISTORY_SEPARATOR, historyLines));
            return String.Join(HistoryHelper.HISTORY_SEPARATOR, historyLines);
        }
    }
}
