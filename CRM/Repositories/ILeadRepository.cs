using CRM.Enum;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface ILeadRepository : IRepository<Lead>
    {
        IEnumerable<Lead> GetLeadsByCustomer(Guid customerId);

        void SetLeadAssignedState(Guid leadId);

        LeadState GetLeadCurrentStatus(Guid leadId);

        void SetState(Guid leadId, EnumState state, EnumStateAction action);
    }
}
