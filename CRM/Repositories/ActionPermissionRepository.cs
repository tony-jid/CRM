using CRM.Data;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class ActionPermissionRepository
    {
        private ApplicationDbContext _context;

        public ActionPermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ActionPermission> Get()
        {
            return _context.ActionPermissions;
        }

        public ActionPermission Get(string actionId, string userRoleName)
        {
            return _context.ActionPermissions.Where(w => w.ActionId == actionId && w.ApplicationRoleName == userRoleName).FirstOrDefault();
        }
    }
}
