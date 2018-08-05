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

        private ILeadRepository _leadRepo;
        public ILeadRepository LeadRepository {
            get {
                if (_leadRepo == null)
                    _leadRepo = new LeadRepository(_context);

                return _leadRepo;
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

    }
}
