using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class LeadAssignment
    {
        [Key]
        public int Id { get; set; }

        public Guid LeadId { get; set; }
        public Lead Lead { get; set; }

        public Guid PartnerBranchId { get; set; }
        public PartnerBranch PartnerBranch { get; set; }

        public int StateId { get; set; }
        public State State { get; set; }

        //public int Rating { get; set; }
        //public string Comment { get; set; }
    }
}
