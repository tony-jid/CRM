using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Lead
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Details { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int LeadTypeId { get; set; }
        public LeadType LeadType { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }

        public IEnumerable<LeadAssignment> LeadAssignments { get; set; }

    }
}
