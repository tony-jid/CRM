using CRM.Helpers;
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
        //[RegularExpression(RegexHelpers.NAME, ErrorMessage = RegexHelpers.NAME_ERROR_MSG)]
        public string ContactName { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(RegexHelpers.PHONE, ErrorMessage = RegexHelpers.PHONE_ERROR_MSG)]
        public string ContactNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression(RegexHelpers.EMAIL, ErrorMessage = RegexHelpers.EMAIL_ERROR_MSG)]
        public string EMail { get; set; }
        
        [StringLength(256)]
        public string BusinessName { get; set; }


        public int AddressId { get; set; }
        public Address Address { get; set; }

        public IEnumerable<Lead> Leads { get; set; }
    }
}
