using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class LeadAssignmentVM
    {
        public int Id { get; set; }

        public Guid LeadId { get; set; }
        public string LeadDetails { get; set; }
        public string LeadTypeName { get; set; }
        public string LeadTypeImage { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerUnique { get; set; }
        public string CustomerDetails { get; set; }
        public string CustomerName { get; set; }
        public string CustomerBusinessName { get; set; }
        public string CustomerEMail { get; set; }
        public string CustomerContactNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerStreetAddress { get; set; }
        public string CustomerSuburb { get; set; }
        public string CustomerState { get; set; }
        public string CustomerPostCode { get; set; }
        
        public string StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusTag { get; set; }
        public string RatingTag { get; set; }
        public List<ActionLeadAssignmentVM> Actions { get; set; }

        public Guid PartnerId { get; set; }
        public string PartnerName { get; set; }
        public string PartnerLogo { get; set; }

        public Guid PartnerBranchId { get; set; }
        public string PartnerBranchAddress { get; set; }
        public string PartnerBranchStreetAddress { get; set; }
        public string PartnerBranchSuburb { get; set; }
        public string PartnerBranchState { get; set; }
        public string PartnerBranchPostCode { get; set; }

        public DateTime AssignedOn { get; set; }
        public string CreatedOnShortFormat { get { return AssignedOn.ToString(DateHelper.FORMAT_SHORT_MONTH_STR); } }

        public string History { get; set; }
    }
}
