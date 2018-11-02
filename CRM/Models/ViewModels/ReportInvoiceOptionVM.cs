using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class ReportInvoiceOptionVM
    {
        // Array or List cannot be passed to the route so have to do this way which is passing concatinated-strings of values and split them at the contoller
        public string StringLeadIds { get; set; }

        public string StringLeadAssignmentIds { get; set; }

        public List<Guid> LeadIds
        {
            get {
                return StringLeadIds?.Split(";").Select(s => new Guid(s)).ToList();
            }
        }

        public List<int> LeadAssignmentIds
        {
            get {
                return StringLeadAssignmentIds?.Split(";").Select(s => Int32.Parse(s)).ToList();
            }
        }
    }
}
