using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class StateAction
    {
        public int StateId { get; set; }
        public State State { get; set; }
        
        public int ActionId { get; set; }
        public Action Action { get; set; }

    }
}
