using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Models.ViewModels;

namespace CRM.Repositories
{
    public interface ILeadAssignmentRepository : IRepository<LeadAssignment>
    {
        IEnumerable<LeadAssignment> GetByLead(Guid leadId);

        void AddByViewModel(LeadAssignmentSelectedPartnerViewModel viewModel);
    }
}
