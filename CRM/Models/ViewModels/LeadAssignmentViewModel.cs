using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class LeadAssignmentViewModel
    {
        public int Id { get; set; }

        public Guid LeadId { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusTag { get; set; }
        public List<ActionLeadAssignmentViewModel> Actions { get; set; }

        public Guid PartnerId { get; set; }
        public string PartnerName { get; set; }
        public string PartnerLogo { get; set; }

        public Guid PartnerBranchId { get; set; }
        public string PartnerBranchAddress { get; set; }
        public string PartnerBranchStreetAddress { get; set; }
        public string PartnerBranchSuburb { get; set; }
        public string PartnerBranchState { get; set; }
        public string PartnerBranchPostCode { get; set; }

        public string History { get; set; }
    }
}
