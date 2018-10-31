using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enum
{
    public enum EnumStateActionTaken
    {
        Sent,
        Messaged,
        Requested_Info,
        Created,
        Viewed,
        Assigned,
        Reassigned,
        Commented,
        Accepted,
        Rejected,
        Invoiced,
        Reinvoiced
    }

    public enum EnumStateAction
    {
        /// <summary>
        /// Send message
        /// </summary>
        AL0,
        /// <summary>
        /// Request info
        /// </summary>
        AL1,
        /// <summary>
        /// Assign partners
        /// </summary>
        AL2,
        /// <summary>
        /// Re-assign partners
        /// </summary>
        AL3,
        /// <summary>
        /// Generate invoice by lead
        /// </summary>
        AL4,
        /// <summary>
        /// Send partner message
        /// </summary>
        ALA0,
        /// <summary>
        /// Comment lead
        /// </summary>
        ALA1,
        /// <summary>
        /// Accept lead
        /// </summary>
        ALA2,
        /// <summary>
        /// Reject lead
        /// </summary>
        ALA3,
        /// <summary>
        /// Generate invoice
        /// </summary>
        ALA4,
        /// <summary>
        /// Re-generate invoice
        /// </summary>
        ALA5
    }
}
