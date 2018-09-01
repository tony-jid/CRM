using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models.ViewModels;

namespace CRM.Repositories
{
    class MessageRepository : IMessageRepository
    {
        private ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RecipientViewModel> GetRecipients()
        {
            var agents = _context.Agents.Select(s => new RecipientViewModel { Name = s.ContactName, Email = s.EMail });

            var customers = _context.Customers.Select(s => new RecipientViewModel { Name = s.ContactName, Email = s.EMail });

            var salesPeople = _context.SalesPeople.Select(s => new RecipientViewModel { Name = s.ContactName, Email = s.EMail });

            return agents.Union(customers).Union(salesPeople).OrderBy(o => o.Name);
        }
    }
}
