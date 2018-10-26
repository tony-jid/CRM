using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public static class RatingHelper
    {
        public const string RATING_TAG_FORMAT = @"
<a href='#/' data-toggle='tooltip'
    data-placement='bottom'
    data-html='true'
    title='{0}'
    <span class='batch-icon batch-icon-speech-bubble-left-tip-text text-priamry'></span>
</a>";
    
        public const string RATING_LINE_FORMAT = "Comment: {0} <br/> By {1} on {2}";
        public const string RATING_SEPARATOR = "<br/>";

        public static string GetHtmlRatingTag(string comment, string commentedBy, DateTime commentedOn)
        {
            if (commentedOn != DateTime.MinValue)
                return String.Format(RatingHelper.RATING_TAG_FORMAT,
                    String.Format(RatingHelper.RATING_LINE_FORMAT, comment, commentedBy, commentedOn.ToString(DateHelper.FORMAT_SHORT_MONTH_STR) + " at " + commentedOn.ToShortTimeString())
                );
            else
                return string.Empty;
        }
    }
}
