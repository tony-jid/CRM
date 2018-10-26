using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class LeadAssignmentRatingVM : IAssessable
    {
        public Guid LeadId { get; set; }
        public int LeadAssignmentId { get; set; }

        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime CommentedOn { get; set; }
        public string CommentedBy { get; set; }
    }
}
