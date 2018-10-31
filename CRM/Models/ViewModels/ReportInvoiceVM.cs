using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class ReportInvoiceVM
    {
        public Guid LeadId { get; set; }
        public string LeadDetails { get; set; }
        public DateTime SubmittedDateTime { get; set; }
        public string SubmittedDateTimeString { get; set; }

        public int LeadTypeId { get; set; }
        public string LeadTypeName { get; set; }
        public string LeadTypeImage { get; set; }
        public double LeadTypePrice { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerUnique { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContactNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerBusinessName { get; set; }
        public string CustomerState { get; set; }
        public string CustomerAddress { get; set; }

        public Guid PartnerId { get; set; }
        public string PartnerName { get; set; }
        public string PartnerLogo { get; set; }
        public string PartnerAddress { get; set; }

        public int LeadAssignmentId { get; set; }
        public DateTime AssignedDateTime { get; set; }
        public string AssignedDateTimeString { get; set; }
        public DateTime AcceptedDateTime { get; set; }
        public string AcceptedDateTimeString { get; set; }

        public string CurrentStateId { get; set; }
        public string CurrentStateName { get; set; }
        public string CurrentStateTag { get; set; }

        public string CurrentActionId { get; set; }
        public string CurrentActionName { get; set; }


    }
}
