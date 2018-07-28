using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public interface IHistory
    {
        string Actor { get; set; }

        string Action { get; set; }

        DateTime ActionTimestamp { get; set; }
    }
}
