using CRM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IMessageRepository
    {
        IEnumerable<RecipientViewModel> GetRecipients();
    }
}
