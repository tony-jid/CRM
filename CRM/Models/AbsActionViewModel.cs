using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class AbsActionViewModel
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public int NextStateId { get; set; }
    }
}
