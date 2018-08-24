using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;

namespace CRM.Repositories
{
    public interface ILeadAssignmentRepository : IRepository<LeadAssignment>
    {
        IEnumerable<LeadAssignment> GetByLead(Guid leadId);
    }
}
