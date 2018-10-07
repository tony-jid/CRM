using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class SalesPerson : IPerson
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression(RegexHelpers.NAME, ErrorMessage = RegexHelpers.NAME_ERROR_MSG)]
        public string ContactName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(RegexHelpers.PHONE, ErrorMessage = RegexHelpers.PHONE_ERROR_MSG)]
        public string ContactNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(RegexHelpers.EMAIL, ErrorMessage = RegexHelpers.EMAIL_ERROR_MSG)]
        public string EMail { get; set; }

        public Guid BranchId { get; set; }
        public PartnerBranch Branch { get; set; }
    }
}
