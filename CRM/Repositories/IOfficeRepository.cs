using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IOfficeRepository : IRepository<Office>
    {
        IEnumerable<Office> GetOfficesByCompany(int companyId);
    }
}
