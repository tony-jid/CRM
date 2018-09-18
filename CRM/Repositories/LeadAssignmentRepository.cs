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

        public void AddByViewModel(LeadAssignmentSelectedPartnerViewModel viewModel)
        {
            foreach (var branchId in viewModel.PartnerBranchIds)
            {
                var assignment = new LeadAssignment() { LeadId = viewModel.LeadId, PartnerBranchId = branchId };
                _context.LeadAssignments.Add(assignment);
                this.SetState(assignment.Id, EnumState.SLA1, EnumStateAction.Assigned);
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

        public void AcceptAssignment(LeadAssignmentResponseVM responseVM)
        {
            //var leadAssignment = this.Get(responseVM.LeadAssignmentId);
            this.SetState(responseVM.LeadAssignmentId, responseVM.Action.NextStateId, EnumStateAction.Accepted);

        }

        public void RejectAssignment(LeadAssignmentResponseVM responseVM)
        {
            //var leadAssignment = this.Get(responseVM.LeadAssignmentId);
            this.SetState(responseVM.LeadAssignmentId, responseVM.Action.NextStateId, EnumStateAction.Rejected);
        }

        public void SetState(int assignmentId, string stateId, EnumStateAction action)
        {
            var userName = "admin";

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

        public void SetState(int assignmentId, EnumState state, EnumStateAction action)
        {
            var userName = "admin";

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
