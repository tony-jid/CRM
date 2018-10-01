using CRM.Models;
using CRM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        void AddServices(Guid partnerId, int[] services);

        void UpdateServices(Guid partnerId, int[] services);

        void RemoveServices(Guid partnerId);

        void UpdateLogo(Guid partnerId, string fileName);
    }
}
