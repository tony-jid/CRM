using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private ApplicationDbContext _context;

        public OfficeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Office entity)
        {
            if (entity.Address == null)
                entity.Address = new Address();

            _context.Addresses.Add(entity.Address);

            _context.Offices.Add(entity);
            //_context.SaveChanges();
        }

        public IEnumerable<Office> Get()
        {
            return _context.Offices.Include(i => i.Address);
        }

        public Office Get(int id)
        {
            return _context.Offices.Include(i => i.Address).FirstOrDefault(w => w.Id == id);
        }

        public Office GetByUid(Guid uid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Office> GetOfficesByCompany(int companyId)
        {
            return _context.Offices.Include(i => i.Address).Where(w => w.CompanyId == companyId);
        }

        public void Remove(Office entity)
        {
            _context.Remove(entity);
            _context.Remove(entity.Address);
            //_context.SaveChanges();
        }

        public void Update(Office entity)
        {
            _context.Update(entity);
            //_context.SaveChanges();
        }
    }
}
