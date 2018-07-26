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

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public Partner Partner { get; set; }
        public Guid PartnerId { get; set; }

        public IEnumerable<LeadAssignment> LeadAssignments { get; set; }

    }
}
