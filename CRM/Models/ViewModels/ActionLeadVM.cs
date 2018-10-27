using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class ActionLeadVM : IAction
    {
        public string Id { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerEmail { get; set; }

        public Guid LeadId { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ActionTarget { get; set; }
        public string RequestType { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public string NextStateId { get; set; }
    }
}
