using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class HistoryViewModel : IHistory
    {
        public string Actor { get; set; }
        public string Action { get; set; }
        public DateTime ActionTimestamp { get; set; }
    }
}
