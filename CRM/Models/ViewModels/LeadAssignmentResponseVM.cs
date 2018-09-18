using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class LeadAssignmentResponseVM
    {
        public Guid LeadId { get; set; }

        public int LeadAssignmentId { get; set; }

        public ActionLeadAssignmentVM Action { get; set; }
    }
}
