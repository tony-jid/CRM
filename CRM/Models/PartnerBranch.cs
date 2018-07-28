using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class PartnerBranch
    {
        [Key]
        public Guid Id { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }

        //public IEnumerable<LeadType> LeadTypes { get; set; }

        public IEnumerable<SalesPerson> SalesPeople { get; set; }
        
        public IEnumerable<LeadAssignment> LeadAssignments { get; set; }

    }
}
