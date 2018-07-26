using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }

        public string Owner { get; set; }

        public int Seq { get; set; }

        public string Name { get; set; }

        public IEnumerable<StateAction> StateActions { get; set; }

        public IEnumerable<LeadAssignment> LeadAssignments { get; set; }
    }
}
