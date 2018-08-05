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
    }
}
