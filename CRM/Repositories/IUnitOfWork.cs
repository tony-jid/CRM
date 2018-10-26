using CRM.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IUnitOfWork
    {
        bool Commit();

        bool Commit(ModelStateDictionary modelState);

        IMessageRepository MessageRepository { get; }

        ICompanyRepository CompanyRepository { get; }

        IOfficeRepository OfficeRepository { get; }

        IAgentRepository AgentRepository { get; }

        ILeadRepository LeadRepository { get; }

        ILeadAssignmentRepository LeadAssignmentRepository { get; }

        ILeadTypeRepository LeadTypeRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        IPartnerRepository PartnerRepository { get; }

        IPartnerBranchRepository PartnerBranchRepository { get; }

        ISalesPersonRepository SalesPersonRepository { get; }

        ActionPermissionRepository ActionPermissionRepository { get; }
    }
}
