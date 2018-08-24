using CRM.Data;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private ICompanyRepository _companyRepo;
        public ICompanyRepository CompanyRepository {
            get {
                if (_companyRepo == null)
                    _companyRepo = new CompanyRepository(_context);
                return _companyRepo;
            }
        }

        private IOfficeRepository _officeRepo;

        public IOfficeRepository OfficeRepository
        {
            get
            {
                if (_officeRepo == null)
                    _officeRepo = new OfficeRepository(_context);
                return _officeRepo;
            }
        }


        private IAgentRepository _agentRepo;

        public IAgentRepository AgentRepository
        {
            get
            {
                if (_agentRepo == null)
                    _agentRepo = new AgentRepository(_context);
                return _agentRepo;
            }
        }

        private ILeadRepository _leadRepo;
        public ILeadRepository LeadRepository {
            get {
                if (_leadRepo == null)
                    _leadRepo = new LeadRepository(_context);
                return _leadRepo;
            }
        }

        private ILeadAssignmentRepository _leadAssRepo;
        public ILeadAssignmentRepository LeadAssignmentRepository
        {
            get
            {
                if (_leadAssRepo == null)
                    _leadAssRepo = new LeadAssignmentRepository(_context);
                return _leadAssRepo;
            }
        }

        private ILeadTypeRepository _leadTypeRepo;
        public ILeadTypeRepository LeadTypeRepository
        {
            get
            {
                if (_leadTypeRepo == null)
                    _leadTypeRepo = new LeadTypeRepository(_context);
                return _leadTypeRepo;
            }
        }

        private ICustomerRepository _cusRepo;

        public ICustomerRepository CustomerRepository
        {
            get {
                if (_cusRepo == null)
                    _cusRepo = new CustomerRepository(_context);
                return _cusRepo;
            }
        }

        private IPartnerRepository _partnerRepo;

        public IPartnerRepository PartnerRepository
        {
            get
            {
                if (_partnerRepo == null)
                    _partnerRepo = new PartnerRepository(_context);
                return _partnerRepo;
            }
        }

        private IPartnerBranchRepository _partnerBranchRepo;
        public IPartnerBranchRepository PartnerBranchRepository
        {
            get
            {
                if (_partnerBranchRepo == null)
                    _partnerBranchRepo = new PartnerBranchRepository(_context);
                return _partnerBranchRepo;
            }
        }
        

        private ISalesPersonRepository _salesPersonRepo;

        public ISalesPersonRepository SalesPersonRepository
        {
            get
            {
                if (_salesPersonRepo == null)
                    _salesPersonRepo = new SalesPersonRepository(_context);
                return _salesPersonRepo;
            }
        }
    }
}
