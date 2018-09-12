using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;

namespace CRM.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Company entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> Get()
        {
            throw new NotImplementedException();
        }

        public Company Get(int id)
        {
            throw new NotImplementedException();
        }

        public Company GetByUid(Guid uid)
        {
            throw new NotImplementedException();
        }

        public Company GetFirst()
        {
            return _context.Company.FirstOrDefault();
        }

        public void Remove(Company entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Company entity)
        {
            _context.Update(entity);
        }
    }
}
