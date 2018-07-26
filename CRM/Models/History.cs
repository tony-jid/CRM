using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class History
    {
        public string Subject { get; set; }

        public string Action { get; set; }

        public string Object{ get; set; }

        public DateTime When { get; set; }
    }
}
