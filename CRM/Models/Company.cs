using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ABN { get; set; }

        [Required]
        public double GST { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [Required]
        [RegularExpression(RegexHelpers.EMAIL, ErrorMessage = RegexHelpers.EMAIL_ERROR_MSG)]
        public string Email { get; set; }

        public IEnumerable<Office> Offices { get; set; }
    }
}
