using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public interface IDataTimestamp
    {
        DateTime CreatedDateTime { get; set; }

        string CreatedBy { get; set; }
    }
}
