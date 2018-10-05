using CRM.Models;
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

        IEnumerable<MessageTemplate> GetTemplates();

        MessageTemplate GetTemplate(int id);

        void AddTemplate(MessageTemplate entity);

        void UpdateTemplate(MessageTemplate entity);

        void RemoveTemplate(MessageTemplate entity);
    }
}
