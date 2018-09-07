using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class HookVM
    {
        public int LeadTypeId { get; set; }
        public string Details { get; set; }
        public string ContactName { get; set; }
        public string BusinessName { get; set; }
        public string ContactNumber { get; set; }
        public string EMail { get; set; }
    }
}
