using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public interface IAssessable
    {
        int Rating { get; set; }

        string Comment { get; set; }
    }
}
