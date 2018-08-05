using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IUnitOfWork
    {
        ILeadRepository LeadRepository { get; }

        ILeadTypeRepository LeadTypeRepository { get; }

        ICustomerRepository CustomerRepository { get; }
    }
}
