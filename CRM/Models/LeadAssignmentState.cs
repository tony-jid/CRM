using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class LeadAssignmentState : IHistory
    {
        public int LeadAssignmentId { get; set; }
        public LeadAssignment LeadAssignment { get; set; }

        public string StateId { get; set; }
        public State State { get; set; }

        public string Actor { get; set; }

        public string Action { get; set; }

        public DateTime ActionTimestamp { get; set; }
    }
}

