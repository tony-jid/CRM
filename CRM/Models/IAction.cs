using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public interface IAction
    {
        string Controller { get; set; }

        string Action { get; set; }

        int NextStateId { get; set; }
    }
}
