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
        void Add(Lead entity, string userName);

        IEnumerable<Lead> GetLeadsByCustomer(Guid customerId);

        void SetLeadAssignedState(Guid leadId, string userName);

        LeadState GetLeadCurrentStatus(Guid leadId);

        void SetState(Guid leadId, EnumState state, EnumStateActionTaken action, string userName);
    }
}
