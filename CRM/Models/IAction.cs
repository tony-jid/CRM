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

        string ActionTarget { get; set; } // "Window", "Modal", "Ajax"

        string RequestType { get; set; } // "Get", "Post", "Put", "Delete"

        string DisplayName { get; set; }

        string Icon { get; set; }

        string NextStateId { get; set; }
    }
}
