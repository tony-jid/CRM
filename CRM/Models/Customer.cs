using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Customer : IPerson
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string ContactName { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Required]
        [StringLength(256)]
        public string BusinessName { get; set; }


        public int AddressId { get; set; }
        public Address Address { get; set; }

        public IEnumerable<Lead> Leads { get; set; }
    }
}
