using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Action : IAction
    {
        [Key]
        public int Id { get; set; }
        public string ControllerName { get; set; }
        
        public string ActionName { get; set; }

        public string DisplayName { get; set; }

        [StringLength(30)]
        public string Icon { get; set; }

        public int NextStateId { get; set; }
        public State NextState { get; set; }

        public IEnumerable<StateAction> StateActions { get; set; }
    }
}
