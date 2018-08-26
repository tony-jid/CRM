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
            var userName = "admin";

            foreach (var branchId in viewModel.PartnerBranchIds)
            {
                var assignment = new LeadAssignment() { LeadId = viewModel.LeadId, PartnerBranchId = branchId };
                _context.LeadAssignments.Add(assignment);
                this.SetConsideringState(assignment.Id, userName);
            }

            //_context.LeadAssignments.Add(entity);
            //this.SetConsideringState(entity.Id, userName);

            _context.SaveChanges();
        }

        public void Add(LeadAssignment entity)
        {
            var userName = "admin";

            _context.LeadAssignments.Add(entity);
            this.SetConsideringState(entity.Id, userName);

            _context.SaveChanges();
        }

        public IEnumerable<LeadAssignment> GetByLead(Guid leadId)
        {
            return _context.LeadAssignments
                .Where(w => w.LeadId == leadId)
                .Include(i => i.PartnerBranch).ThenInclude(i => i.Partner)
                .Include(i => i.PartnerBranch).ThenInclude(i => i.Address)
                .Include(i => i.LeadAssignmentStates).ThenInclude(i => i.State.StateActions).ThenInclude(i => i.Action);
        }

        public IEnumerable<LeadAssignment> Get()
        {
            throw new NotImplementedException();
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
            _context.SaveChanges();
        }

        public void Update(LeadAssignment entity)
        {
            throw new NotImplementedException();
        }

        private void SetConsideringState(int assignmentId, string actor)
        {
            var state = new LeadAssignmentState();
            state.LeadAssignmentId = assignmentId;
            state.StateId = (int)EnumState.LeadAssignmentConsidering;
            state.Actor = actor;
            state.Action = nameof(EnumStateAction.Assigned);
            state.ActionTimestamp = DateTime.Now;

            _context.LeadAssignmentStates.Add(state);
        }
    }
}
