using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class LeadTypeRepository : ILeadTypeRepository
    {
        private ApplicationDbContext _context;

        public LeadTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(LeadType entity)
        {
            _context.LeadTypes.Add(entity);
            //_context.SaveChanges();
        }

        public LeadType Get(int id)
        {
            return _context.LeadTypes.FirstOrDefault(w => w.Id == id);
        }

        public LeadType GetByUid(Guid uid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LeadType> Get()
        {
            return _context.LeadTypes;
        }

        public void Update(LeadType entity)
        {
            _context.Update(entity);
           // _context.SaveChanges();
        }

        public void Remove(LeadType entity)
        {
            _context.Remove(entity);
            //_context.SaveChanges();
        }
    }
}
