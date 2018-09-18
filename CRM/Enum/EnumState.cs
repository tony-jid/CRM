using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enum
{
    public enum EnumState
    {
        /// <summary>
        /// Status of unknown
        /// </summary>
        S0,
        /// <summary>
        /// Status of a lead - New
        /// </summary>
        SL1,
        /// <summary>
        /// Status of a lead - Assigned
        /// </summary>
        SL2,
        /// <summary>
        /// Status of a lead - Reassigned
        /// </summary>
        SL3,
        /// <summary>
        /// Status of an assignment - Considering
        /// </summary>
        SLA1,
        /// <summary>
        /// Status of an assignment - Accepted
        /// </summary>
        SLA2,
        /// <summary>
        /// Status of an assignment - Rejected
        /// </summary>
        SLA3,
        /// <summary>
        /// Status of an assignment - Invoiced
        /// </summary>
        SLA4,
        /// <summary>
        /// Status of an assignment - Reinvoiced
        /// </summary>
        SLA5,


        //LeadNew = 1,
        //LeadRead = 2,
        //LeadAssigned = 3,
        //LeadReAssigned = 4,
        //LeadAssignmentConsidering = 5,
        //LeadAssignmentAccepted = 6,
        //LeadAssignmentRejected = 7,
        //LeadAssignmentInvoiced = 8,
        //LeadAssignmentReInvoiced = 9
    }
}
