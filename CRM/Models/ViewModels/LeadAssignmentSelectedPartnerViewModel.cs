using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class LeadAssignmentSelectedPartnerViewModel
    {
        public Guid LeadId { get; set; }

        public List<Guid> PartnerBranchIds { get; set; }
    }
}
