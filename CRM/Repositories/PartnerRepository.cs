using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;

namespace CRM.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private ApplicationDbContext _context;

        public PartnerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Partner entity)
        {
            _context.Partners.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Partner> Get()
        {
            return _context.Partners;
        }

        public Partner Get(int id)
        {
            throw new NotImplementedException();
        }

        public Partner GetByUid(Guid uid)
        {
            return _context.Partners.FirstOrDefault(w => w.Id == uid);
        }

        public void Remove(Partner entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Partner entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
