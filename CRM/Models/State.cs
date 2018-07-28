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

        [Required]
        public string Owner { get; set; }

        [Required]
        public int Seq { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Repeatable { get; set; }

        public IEnumerable<StateAction> StateActions { get; set; }

        public IEnumerable<Action> ActionsWithNextSate { get; set; }


        public IEnumerable<LeadAssignmentState> LeadAssignmentStates { get; set; }

        public IEnumerable<LeadState> LeadStates { get; set; }

    }
}
