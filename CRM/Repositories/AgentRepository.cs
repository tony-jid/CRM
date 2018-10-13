using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private ApplicationDbContext _context;

        public AgentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Agent entity)
        {
            _context.Agents.Add(entity);
        }

        public IEnumerable<Agent> Get()
        {
            return _context.Agents.Include(i => i.ApplicationUser);
        }

        public Agent Get(int id)
        {
            throw new NotImplementedException();
        }

        public Agent GetByUid(Guid uid)
        {
            return _context.Agents.Where(w => w.Id == uid).Include(i => i.ApplicationUser).FirstOrDefault();
        }

        public IEnumerable<Agent> GetAgentsByOffice(int officeId)
        {
            return _context.Agents.Where(w => w.OfficeId == officeId).Include(i => i.ApplicationUser);
        }

        public void Remove(Agent entity)
        {
            _context.Remove(entity);
        }

        public void Update(Agent entity)
        {
            _context.Update(entity);
        }
    }
}
