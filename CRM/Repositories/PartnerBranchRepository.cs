using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.Repositories
{
    public class PartnerBranchRepository : IPartnerBranchRepository
    {
        private ApplicationDbContext _context;

        public PartnerBranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(PartnerBranch entity)
        {
            if (entity.Address == null)
                entity.Address = new Address();

            _context.Addresses.Add(entity.Address);

            _context.PartnerBranches.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<PartnerBranch> Get()
        {
            return _context.PartnerBranches.Include(i => i.Address);
        }

        public PartnerBranch Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PartnerBranch> GetBranchesByPartner(Guid partnerId)
        {
            return _context.PartnerBranches
                .Include(i => i.Address)
                .Where(w => w.PartnerId == partnerId);
        }

        public PartnerBranch GetByUid(Guid uid)
        {
            return _context.PartnerBranches.Include(i => i.Address).FirstOrDefault(w => w.Id == uid);
        }

        public void Remove(PartnerBranch entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(PartnerBranch entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
