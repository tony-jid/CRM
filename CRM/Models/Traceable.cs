using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public abstract class Traceable
    {
        public Guid CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }

        public Guid UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public Guid DeleteUserId { get; set; }
        public DateTime DeleteDateTime { get; set; }
    }
}
