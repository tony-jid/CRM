using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class LeadAssignment : IDataTimestamp, IAssessable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDateTime { get; set; }

        public string CreatedBy { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }

        public Guid LeadId { get; set; }
        public Lead Lead { get; set; }

        public Guid PartnerBranchId { get; set; }
        public PartnerBranch PartnerBranch { get; set; }

        public IEnumerable<LeadAssignmentState> LeadAssignmentStates { get; set; }

        public IEnumerable<InvoiceItem> InvoiceItems { get; set; }
    }
}
