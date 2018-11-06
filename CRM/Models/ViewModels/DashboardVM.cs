using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class DashboardVM
    {
        public int TodayLeadsAmount { get; set; }
        public int TodayAcceptedLeadsAmount { get; set; }
        public int TodayRejectedLeadsAmount { get; set; }

        public List<DashboardLeadOverviewVM> DashboardLeadOverviewVMs { get; set; }
    }
}
