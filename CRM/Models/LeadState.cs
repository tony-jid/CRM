using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class LeadState : IHistory
    {
        public Guid LeadId { get; set; }
        public Lead Lead { get; set; }

        public string StateId { get; set; }
        public State State { get; set; }

        //public DateTime CreatedTimestamp { get; set; } // use ActionTimestamp

        public string Actor { get; set; }

        public string Action { get; set; }

        //public string Object { get; set; }

        public DateTime ActionTimestamp { get; set; }
    }
}
