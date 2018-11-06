using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class DashboardLeadVM
    {
        public Guid LeadId { get; set; }

        public int LeadTypeId { get; set; }

        public string LeadTypeName { get; set; }

        public string LeadStateType { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ActionTimestamp { get; set; }

        public string DayCreated
        {
            get {
                return CreatedOn.ToString("dddd");
            }
        }
        public string WeekCreated
        {
            get
            {
                return CreatedOn.ToString("dd");
            }
        }
        public string MonthCreated
        {
            get
            {
                return CreatedOn.ToString("MMMM");
            }
        }


    }
}
