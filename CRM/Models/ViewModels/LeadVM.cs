using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class LeadVM
    {
        public Guid Id { get; set; }   

        public string Details { get; set; }

        public string StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusTag { get; set; }
        public List<ActionLeadVM> Actions { get; set; }


        public int LeadTypeId { get; set; }
        public string LeadTypeName { get; set; }
        public string LeadTypeImage { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerUnique { get; set; }
        public string CustomerName { get; set; }
        public string CustomerBusinessName { get; set; }
        public string CustomerContactNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerDetails { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedOnShortFormat { get { return CreatedOn.ToString(DateHelper.FORMAT_SHORT_MONTH_STR); } }
        public string History { get; set; }
    }
}
