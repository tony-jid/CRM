using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Enum;
using CRM.Models;
using CRM.Models.ViewModels;

namespace CRM.Repositories
{
    public interface ILeadAssignmentRepository : IRepository<LeadAssignment>
    {
        IEnumerable<LeadAssignment> GetByLead(Guid leadId);

        IEnumerable<LeadAssignment> GetByPartner(Guid partnerId);

        void AddByViewModel(LeadAssignmentSelectedPartnerViewModel viewModel, string userName);

        void AcceptAssignment(LeadAssignmentResponseVM responseVM, string userName);

        void RejectAssignment(LeadAssignmentResponseVM responseVM, string userName);

        void SetState(int assignmentId, EnumState state, EnumStateAction action, string userName);
    }
}
