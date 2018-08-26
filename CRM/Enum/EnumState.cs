using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enum
{
    public enum EnumState
    {
        LeadNew = 1,
        LeadRead = 2,
        LeadAssigned = 3,
        LeadReAssigned = 4,
        LeadAssignmentConsidering = 5,
        LeadAssignmentAccepted = 6,
        LeadAssignmentRejected = 7,
        LeadAssignmentInvoiced = 8,
        LeadAssignmentReInvoiced = 9
    }
}
