using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class InvoiceItem
    {
        public int InvoiceNo { get; set; }

        public Invoice Invoice { get; set; }

        public int LeadAssignmentId { get; set; }

        public LeadAssignment LeadAssignment { get; set; }

        public bool Reinvoiced { get; set; }
    }
}
