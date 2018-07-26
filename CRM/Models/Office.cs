using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Office
    {
        [Key]
        public int Id { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public IEnumerable<Agent> Agents { get; set; }
    }
}
