using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);

        IEnumerable<Customer> GetAllIncludeLeads();

        bool IsCustomerExisted(string email);
    }
}
