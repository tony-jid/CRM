using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class StateAction
    {
        public string StateId { get; set; }
        public State State { get; set; }
        
        public string ActionId { get; set; }
        public Action Action { get; set; }

    }
}
