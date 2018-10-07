using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Agent : IPerson
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        [Key]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression(RegexHelpers.NAME, ErrorMessage = RegexHelpers.NAME_ERROR_MSG)]
        public string ContactName { get; set; }

        [Required]
        [RegularExpression(RegexHelpers.PHONE, ErrorMessage = RegexHelpers.PHONE_ERROR_MSG)]
        public string ContactNumber { get; set; }

        [Required]
        [RegularExpression(RegexHelpers.EMAIL, ErrorMessage = RegexHelpers.EMAIL_ERROR_MSG)]
        public string EMail { get; set; }

        public int OfficeId { get; set; }
        public Office Office { get; set; }
    }
}
