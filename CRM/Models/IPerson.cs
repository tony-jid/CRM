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
        [DataType(DataType.PhoneNumber)]
        string ContactNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        string EMail { get; set; }
    }
}
