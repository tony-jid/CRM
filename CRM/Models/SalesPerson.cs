using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class SalesPerson : IPerson
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ContactName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        public Guid BranchId { get; set; }
        public PartnerBranch Branch { get; set; }
    }
}
