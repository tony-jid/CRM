using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CRM.Models
{
    public class LeadType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        //[JsonIgnore]
        public IEnumerable<Lead> Leads { get; set; }
    }
}
