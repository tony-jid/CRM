using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;

namespace CRM.Repositories
{
    public class SalesPersonRepository : ISalesPersonRepository
    {
        private ApplicationDbContext _context;

        public SalesPersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(SalesPerson entity)
        {
            _context.SalesPeople.Add(entity);
        }

        public IEnumerable<SalesPerson> Get()
        {
            return _context.SalesPeople;
        }

        public SalesPerson Get(int id)
        {
            throw new NotImplementedException();
        }

        public SalesPerson GetByUid(Guid uid)
        {
            return _context.SalesPeople.FirstOrDefault(w => w.Id == uid);
        }

        public IEnumerable<SalesPerson> GetSalesPeopleByBranch(Guid branchId)
        {
            return _context.SalesPeople
                .Where(w => w.BranchId == branchId);
        }

        public void Remove(SalesPerson entity)
        {
            _context.Remove(entity);
        }

        public void Update(SalesPerson entity)
        {
            _context.Update(entity);
        }
    }
}
