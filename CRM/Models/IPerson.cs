using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public interface IPerson
    {
        [Required]
        string ContactName { get; set; }

        [Required]
        string ContactNumber { get; set; }

        [Required]
        string EMail { get; set; }
    }
}
