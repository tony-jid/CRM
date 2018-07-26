using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string ContactName { get; set; }

        [Required]
        [StringLength(256)]
        public string BusinessName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }

        [StringLength(256)]
        public string StreetAddress { get; set; }

        [StringLength(128)]
        public string Suburb { get; set; }

        [StringLength(4)]
        public string State { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        public string Postcode { get; set; }

        public IEnumerable<Lead> Leads { get; set; }
    }
}
