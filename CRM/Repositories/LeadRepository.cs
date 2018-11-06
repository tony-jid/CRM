using CRM.Data;
using CRM.Enum;
using CRM.Models;
using CRM.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private ApplicationDbContext _context;

        public LeadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Lead entity)
        {
            _context.Leads.Add(entity);
            this.SetState(entity.Id, EnumState.SL1, EnumStateActionTaken.Created, "Anonymous");
        }

        public void Add(Lead entity, string userName)
        {
            _context.Leads.Add(entity);
            this.SetState(entity.Id, EnumState.SL1, EnumStateActionTaken.Created, userName);

            //_context.SaveChanges(); will be committed at Controller
        }

        public Lead Get(int id)
        {
            throw new NotImplementedException();
        }

        public Lead GetByUid(Guid uid)
        {
            return _context.Leads.Where(w => w.Id == uid)
                .Include(i => i.Customer).ThenInclude(i => i.Address)
                .Include(i => i.LeadType)
                .Include(i => i.LeadStates).ThenInclude(i => i.State.StateActions).ThenInclude(i => i.Action)
                .FirstOrDefault();
        }

        public IEnumerable<Lead> Get()
        {
            return _context.Leads
                .Include(i => i.Customer).ThenInclude(i => i.Address)
                .Include(i => i.LeadType)
                .Include(i => i.LeadStates).ThenInclude(i => i.State.StateActions).ThenInclude(i => i.Action);
        }

        public IEnumerable<Lead> GetLeadsByCustomer(Guid customerId)
        {
            return this.Get()
                .Where(w => w.CustomerId == customerId);
        }

        public IEnumerable<Lead> GetTodayLeads()
        {
            var todayLeadIds = this.GetTodayLeadIds();
            return this.Get().Where(w => todayLeadIds.Contains(w.Id));
        }

        private List<Guid> GetTodayLeadIds()
        {
            return _context.LeadStates
                .Where(w => w.StateId == nameof(EnumState.SL1) && w.ActionTimestamp.Date == DateTime.Now.Date)
                .Select(s => s.LeadId).ToList();
        }

        //private IEnumerable<DashboardLeadVM> GetDashboardLeadVMs(DateTime dateStart, DateTime dateEnd)
        public IEnumerable<DashboardLeadVM> GetDashboardLeadVMs(DateTime dateStart, DateTime dateEnd)
        {
            var statusNewLeadVMs = _context.LeadStates
                .Where(w => w.StateId == nameof(EnumState.SL1) && 
                    (w.ActionTimestamp.Date >= dateStart.Date)
                    && (w.ActionTimestamp.Date <= dateEnd.Date)
                )
                .Include(i => i.Lead).ThenInclude(i => i.LeadType)
                .Select(s => new DashboardLeadVM() {
                    LeadId = s.LeadId,
                    LeadTypeId = s.Lead.LeadTypeId,
                    LeadTypeName = s.Lead.LeadType.Name,
                    LeadStateType = s.StateId,
                    CreatedOn = s.ActionTimestamp.Date
                });

            List<DashboardLeadVM> adjustedStatusLeadVMs = new List<DashboardLeadVM>();
            foreach (var leadVM in statusNewLeadVMs)
            {
                var assignments = _context.LeadAssignments
                    .Where(w => w.LeadId == leadVM.LeadId)
                    .Include(i => i.LeadAssignmentStates);

                string leadStateType = nameof(EnumState.SL1);
                DateTime actionTimestamp = leadVM.CreatedOn;

                foreach (var assignment in assignments)
                {
                    var assignmentCurrentStatus = assignment.LeadAssignmentStates.OrderByDescending(o => o.ActionTimestamp).First();

                    if (assignmentCurrentStatus.StateId == nameof(EnumState.SLA2)
                        || assignmentCurrentStatus.StateId == nameof(EnumState.SLA4)
                        || assignmentCurrentStatus.StateId == nameof(EnumState.SLA5))
                    {
                        // If any of assignments is accepted, the lead is supposed to be accepted.
                        leadStateType = nameof(EnumState.SLA2);
                        actionTimestamp = assignmentCurrentStatus.ActionTimestamp;
                        break;
                    } else if (assignmentCurrentStatus.StateId == nameof(EnumState.SLA3))
                    {
                        leadStateType = nameof(EnumState.SLA3);
                        actionTimestamp = assignmentCurrentStatus.ActionTimestamp;
                    }
                }

                adjustedStatusLeadVMs.Add(new DashboardLeadVM() {
                    LeadId = leadVM.LeadId,
                    LeadTypeId = leadVM.LeadTypeId,
                    LeadTypeName = leadVM.LeadTypeName,
                    CreatedOn = leadVM.CreatedOn,
                    LeadStateType = leadStateType,
                    ActionTimestamp = actionTimestamp
                });
            }

            /*
            var leadIds = statusNewLeadVMs.Select(s => s.LeadId).ToList();

            var leadAssignmentIds = _context.LeadAssignments
                .Where(w => leadIds.Contains(w.LeadId))
                .Select(s => s.Id).ToList();

            var currentAssignmentsStatus = _context.LeadAssignmentStates
                .Where(w => leadAssignmentIds.Contains(w.LeadAssignmentId))
                .Include(i => i.LeadAssignment)
                .GroupBy(g => new { g.LeadAssignment.LeadId, g.LeadAssignmentId })
                .Select(s => new
                {
                    LeadId = s.Key.LeadId
                    , LeadAssignmentId = s.Key.LeadAssignmentId
                    , CurrentTimestamp = s.Max(max => max.ActionTimestamp)
                });

            var leadAssignments = _context.LeadAssignmentStates
                .Include(i => i.LeadAssignment)
                .Where(w => currentAssignmentsStatus.Contains(
                    new { LeadId = w.LeadAssignment.LeadId, LeadAssignmentId = w.LeadAssignmentId, CurrentTimestamp = w.ActionTimestamp }
                    ));
            */

            return adjustedStatusLeadVMs;
        }

        public void Update(Lead entity)
        {
            _context.Update(entity);
            //_context.SaveChanges(); will be committed at Controller
        }

        public void Remove(Lead entity)
        {
            _context.Remove(entity);
            //_context.SaveChanges(); will be committed at Controller
        }

        public void SetLeadAssignedState(Guid leadId, string userName)
        {
            var currentStatus = this.GetLeadCurrentStatus(leadId);

            if (currentStatus.StateId == EnumState.SL1.ToString())
                this.SetState(leadId, EnumState.SL2, EnumStateActionTaken.Assigned, userName);
            else
                this.SetState(leadId, EnumState.SL3, EnumStateActionTaken.Reassigned, userName);
        }

        public LeadState GetLeadCurrentStatus(Guid leadId)
        {
            var lead = this.GetByUid(leadId);
            return lead.LeadStates.OrderByDescending(o => o.ActionTimestamp).FirstOrDefault();
        }

        public void SetState(Guid leadId, EnumState state, EnumStateActionTaken action, string userName)
        {
            var leadState = new LeadState()
            {
                LeadId = leadId,
                StateId = state.ToString(),
                Actor = userName,
                Action = action.ToString(),
                ActionTimestamp = DateTime.Now
            };

            _context.LeadStates.Add(leadState);
        }
    }
}
