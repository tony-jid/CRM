using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class AgentVM : Agent
    {
        [Required]
        public string RoleName { get; set; }
    }
}
