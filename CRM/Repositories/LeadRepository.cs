using CRM.Data;
using CRM.Enum;
using CRM.Models;
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
