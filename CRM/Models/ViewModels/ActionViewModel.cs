using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class ActionViewModel : IAction
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public int NextStateId { get; set; }
    }
}
