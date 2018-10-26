using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class ActionPermission
    {
        public string ActionId { get; set; }

        public Action Action { get; set; }
        
        public string ApplicationRoleName { get; set; }

        // Identity's ApplicationRole
    }
}
