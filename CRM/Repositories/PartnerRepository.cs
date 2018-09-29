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
        }

        public void AddServices(Guid partnerId, int[] services)
        {
            if (services != null)
            {
                var partnerServices = services.Select(leadTypeId => new PartnerService()
                {
                    PartnerId = partnerId,
                    LeadTypeId = leadTypeId
                });

                _context.PartnerServices.AddRange(partnerServices);
            }
        }

        public IEnumerable<Partner> Get()
        {
            return _context.Partners
                .Include(i => i.PartnerServices).ThenInclude(i => i.LeadType);
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
            _context.Partners.Remove(entity);
        }

        public void RemoveServices(Guid partnerId)
        {
            var partnerServices = _context.PartnerServices.Where(w => w.PartnerId == partnerId);

            if (partnerServices != null)
            {
                _context.PartnerServices.RemoveRange(partnerServices);
            }
        }

        public void Update(Partner entity)
        {
            _context.Partners.Update(entity);
        }

        public void UpdateServices(Guid partnerId, int[] services)
        {
            this.RemoveServices(partnerId);

            if (services != null)
            {
                foreach (var leadTypeId in services)
                {
                    _context.PartnerServices.Add(new PartnerService() { PartnerId = partnerId, LeadTypeId = leadTypeId });
                }
            }
        }
    }
}
