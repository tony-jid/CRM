using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public interface IAssessable
    {
        int Rate { get; set; }

        string Comment { get; set; }

        DateTime CommentedOn { get; set; }

        string CommentedBy { get; set; }
    }
}
