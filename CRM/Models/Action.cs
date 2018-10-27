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
        [StringLength(5)]
        public string Id { get; set; }

        public string ControllerName { get; set; }
        
        public string ActionName { get; set; }

        public string ActionTarget { get; set; }

        public string RequestType { get; set; }

        public string DisplayName { get; set; }
        
        public string Icon { get; set; }

        public bool IsGroupAction { get; set; }

        public string GroupActionDisplayName { get; set; }

        public string NextStateId { get; set; }
        public State NextState { get; set; }

        public IEnumerable<StateAction> StateActions { get; set; }

        public IEnumerable<ActionPermission> ActionPermissions { get; set; }
    }
}
