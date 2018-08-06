using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IAgentRepository : IRepository<Agent>
    {
        IEnumerable<Agent> GetAgentsByOffice(int officeId);
    }
}
