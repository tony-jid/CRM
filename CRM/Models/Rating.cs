using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Rating : IAssessable
    {
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime CommentedOn { get; set; }
        public string CommentedOnShortFormat {
            get {
                if (CommentedOn != DateTime.MinValue)
                    return CommentedOn.ToString(DateHelper.FORMAT_SHORT_MONTH_STR) + " at " + CommentedOn.ToShortTimeString()
                        + " by " + CommentedBy;
                else
                    return "Never commented.";
            }
        }
        public string CommentedBy { get; set; }
    }
}
