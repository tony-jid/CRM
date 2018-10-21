using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class MessageViewModel
    {
        public string CustomerId { get; set; }

        public string LeadId { get; set; }

        public int LeadAssignmentId { get; set; }

        public List<RecipientViewModel> Recipients { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
