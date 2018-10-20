using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using CRM.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

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
            var company = _context.Company.Select(s => new RecipientViewModel { Name = s.Name, Email = s.Email });

            var agents = _context.Agents.Select(s => new RecipientViewModel { Name = s.ContactName, Email = s.EMail });

            var customers = _context.Customers.Select(s => new RecipientViewModel { Name = s.ContactName, Email = s.EMail });

            var salesPeople = _context.SalesPeople.Select(s => new RecipientViewModel { Name = s.ContactName, Email = s.EMail });

            return company.Union(agents).Union(customers).Union(salesPeople).OrderBy(o => o.Name);
        }

        public IEnumerable<RecipientViewModel> GetRecipientsForPartner(Guid partnerId)
        {
            var company = _context.Company.Select(s => new RecipientViewModel { Name = s.Name, Email = s.Email });

            var agents = _context.Agents.Select(s => new RecipientViewModel { Name = s.ContactName, Email = s.EMail });

            List<Customer> customers = new List<Customer>();
            var partner = _context.Partners.Where(w => w.Id == partnerId)
                .Include(a => a.Branches).ThenInclude(b => b.LeadAssignments).ThenInclude(c => c.Lead).ThenInclude(d => d.Customer).FirstOrDefault();

            if (partner != null)
            {
                var branchCustomers = partner.Branches.Select(a => a.LeadAssignments.Select(b => b.Lead).Select(c => c.Customer));
                foreach (var branchCustomer in branchCustomers)
                {
                    customers.AddRange(branchCustomer);
                }
            }
            else
            {
                throw new ApplicationException("Partner is not found.");
            }

            var customerEmails = customers.Distinct().Select(s => new RecipientViewModel { Name = s.ContactName, Email = s.EMail });

            return company.Union(agents).Union(customerEmails).OrderBy(o => o.Name);
        }

        public IEnumerable<MessageTemplate> GetTemplates()
        {
            return _context.MessageTemplates;
        }

        public MessageTemplate GetTemplate(int id)
        {
            return _context.MessageTemplates.SingleOrDefault(w => w.Id == id);
        }

        public void AddTemplate(MessageTemplate entity)
        {
            _context.MessageTemplates.Add(entity);
        }

        public void RemoveTemplate(MessageTemplate entity)
        {
            _context.MessageTemplates.Remove(entity);
        }

        public void UpdateTemplate(MessageTemplate entity)
        {
            _context.MessageTemplates.Update(entity);
        }
    }
}
