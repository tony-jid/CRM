using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Enum;
using CRM.Models;
using CRM.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CRM.Repositories
{
    public class LeadAssignmentRepository : ILeadAssignmentRepository
    {
        private ApplicationDbContext _context;

        public LeadAssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddByViewModel(LeadAssignmentSelectedPartnerViewModel viewModel, string userName)
        {
            foreach (var branchId in viewModel.PartnerBranchIds)
            {
                var assignment = new LeadAssignment() { LeadId = viewModel.LeadId, PartnerBranchId = branchId };
                _context.LeadAssignments.Add(assignment);
                this.SetState(assignment.Id, EnumState.SLA1, EnumStateActionTaken.Assigned, userName);
            }

            //_context.SaveChanges(); // will be commit at Controller
        }

        public void Add(LeadAssignment entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LeadAssignment> GetByLead(Guid leadId)
        {
            return _context.LeadAssignments
                .Where(w => w.LeadId == leadId)
                .Include(i => i.PartnerBranch).ThenInclude(i => i.Partner)
                .Include(i => i.PartnerBranch).ThenInclude(i => i.Address)
                .Include(i => i.LeadAssignmentStates).ThenInclude(i => i.State.StateActions).ThenInclude(i => i.Action);
        }

        public IEnumerable<LeadAssignment> GetByLeads(List<Guid> leadIds)
        {
            return _context.LeadAssignments
                .Where(w => leadIds.Contains(w.LeadId))
                .Include(i => i.PartnerBranch).ThenInclude(i => i.Partner)
                .Include(i => i.PartnerBranch).ThenInclude(i => i.Address)
                .Include(i => i.LeadAssignmentStates).ThenInclude(i => i.State.StateActions).ThenInclude(i => i.Action);
        }

        public IEnumerable<LeadAssignment> GetByPartner(Guid partnerId)
        {
            return _context.LeadAssignments
                .Include(i => i.PartnerBranch).ThenInclude(i => i.Partner).Where(w => w.PartnerBranch.PartnerId == partnerId)
                .Include(i => i.PartnerBranch).ThenInclude(i => i.Address)
                .Include(i => i.Lead).ThenInclude(i => i.LeadType)
                .Include(i => i.Lead).ThenInclude(i => i.Customer).ThenInclude(i => i.Address)
                .Include(i => i.LeadAssignmentStates).ThenInclude(i => i.State.StateActions).ThenInclude(i => i.Action);
        }

        public IEnumerable<LeadAssignment> Get()
        {
            return _context.LeadAssignments;
        }

        public LeadAssignment Get(int id)
        {
            return _context.LeadAssignments
                //.Include(i => i.LeadAssignmentStates)
                .Where(w => w.Id == id).SingleOrDefault();
        }

        public LeadAssignment GetByUid(Guid uid)
        {
            throw new NotImplementedException();
        }

        public void Remove(LeadAssignment entity)
        {
            _context.Remove(entity);
            //_context.SaveChanges();
        }

        public void Update(LeadAssignment entity)
        {
            throw new NotImplementedException();
        }

        public void AcceptAssignment(LeadAssignmentResponseVM responseVM, string userName)
        {
            //var leadAssignment = this.Get(responseVM.LeadAssignmentId);
            this.SetState(responseVM.LeadAssignmentId, responseVM.Action.NextStateId, EnumStateActionTaken.Accepted, userName);

        }

        public void RejectAssignment(LeadAssignmentResponseVM responseVM, string userName)
        {
            //var leadAssignment = this.Get(responseVM.LeadAssignmentId);
            this.SetState(responseVM.LeadAssignmentId, responseVM.Action.NextStateId, EnumStateActionTaken.Rejected, userName);
        }

        public void CommentLeadAssignment(int leadAssignmentId, string comment, string userName)
        {
            var leadAssignment = this.Get(leadAssignmentId);
            leadAssignment.Comment = comment;
            leadAssignment.CommentedOn = DateTime.Now;
            leadAssignment.CommentedBy = userName;

            _context.Update(leadAssignment);
        }

        public void SetState(int assignmentId, string stateId, EnumStateActionTaken action, string userName)
        {
            var itemState = new LeadAssignmentState
            {
                LeadAssignmentId = assignmentId,
                StateId = stateId,
                Actor = userName,
                Action = action.ToString(),
                ActionTimestamp = DateTime.Now
            };

            _context.LeadAssignmentStates.Add(itemState);
        }

        public void SetState(int assignmentId, EnumState state, EnumStateActionTaken action, string userName)
        {
            var itemState = new LeadAssignmentState
            {
                LeadAssignmentId = assignmentId,
                StateId = state.ToString(),
                Actor = userName,
                Action = action.ToString(),
                ActionTimestamp = DateTime.Now
            };

            _context.LeadAssignmentStates.Add(itemState);
        }
    }
}
