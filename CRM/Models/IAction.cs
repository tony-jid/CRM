using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public interface IAction
    {
        string ControllerName { get; set; }

        string ActionName { get; set; }

        string DisplayName { get; set; }

        string Icon { get; set; }

        int NextStateId { get; set; }
    }
}
