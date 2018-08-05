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
            entity.CreatedBy = "admin";
            entity.CreatedDateTime = DateTime.Now;

            _context.Leads.Add(entity);
            this.AddLeadState(entity.Id, EnumState.LeadNew, "admin", EnumStateAction.Created, EnumStateActionObject.Lead);

            _context.SaveChanges();
        }

        public Lead Get(int id)
        {
            throw new NotImplementedException();
        }

        public Lead GetByUid(Guid uid)
        {
            return _context.Leads.Include(i => i.LeadType).FirstOrDefault(w => w.Id == uid);
        }

        public IEnumerable<Lead> Get()
        {
            return _context.Leads.Include(i => i.LeadType);
        }

        public IEnumerable<Lead> GetLeadsByCustomer(Guid customerId)
        {
            return _context.Leads
                .Include(i => i.LeadType)
                .Include(i => i.LeadStates)
                .Where(w => w.CustomerId == customerId);
        }

        public void Update(Lead entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Remove(Lead entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void AddLeadState(Guid leadId, EnumState stateId, string actor, EnumStateAction action, EnumStateActionObject actionObject)
        {
            var state = new LeadState();
            state.LeadId = leadId;
            state.StateId = (int)stateId;
            state.Actor = actor;
            state.Action = nameof(action);
            //state.Object = nameof(actionObject);
            state.ActionTimestamp = DateTime.Now;
            state.CreatedTimestamp = DateTime.Now;

            _context.LeadStates.Add(state);
        }
    }
}
