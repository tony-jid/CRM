using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Enum;
using CRM.Models;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    [Authorize(Roles = nameof(EnumApplicationRole.Admin) + "," + nameof(EnumApplicationRole.Manager) + "," + nameof(EnumApplicationRole.Agent))]
    public class PartnerBranchesController : BaseController
    {
        private IUnitOfWork _uow;
        private IPartnerBranchRepository _partnerBranchRepo;
        private ILeadAssignmentRepository _leadAssRepo;

        public PartnerBranchesController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _partnerBranchRepo = _uow.PartnerBranchRepository;
            _leadAssRepo = _uow.LeadAssignmentRepository;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_partnerBranchRepo.Get(), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new PartnerBranch();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _partnerBranchRepo.Add(model);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public IActionResult Put(Guid key, string values)
        {
            var model = _partnerBranchRepo.GetByUid(key);
            if (model == null)
                return StatusCode(409, "Branch of partner not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _partnerBranchRepo.Update(model);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public IActionResult Delete(Guid key)
        {
            var model = _partnerBranchRepo.GetByUid(key);

            _partnerBranchRepo.Remove(model);

            if (_uow.Commit(this.ModelState))
                return Ok();
            else
                return BadRequest(GetFullErrorMessage(this.ModelState));
        }

        [HttpGet]
        public object GetBranchesByPartner(Guid partnerId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_partnerBranchRepo.GetBranchesByPartner(partnerId), loadOptions);
        }

        [HttpGet]
        public object GetBranchesByLeadType(int leadTypeId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_partnerBranchRepo.GetBranchesByLeadType(leadTypeId), loadOptions);
        }

        [HttpGet]
        public object GetBranchesByLead(Guid leadId, int leadTypeId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(this.FilterBranchesByLead(leadId, leadTypeId), loadOptions);
        }

        protected IEnumerable<PartnerBranch> FilterBranchesByLead(Guid leadId, int leadTypeId)
        {
            var assignments = _leadAssRepo.GetByLead(leadId);
            List<Guid> partnerWaitingAssignments = new List<Guid>();
            List<Guid> partnerAcceptedAssignments = new List<Guid>();

            foreach (var item in assignments)
            {
                var currentState = item.LeadAssignmentStates.OrderByDescending(o => o.ActionTimestamp).FirstOrDefault();

                if (currentState == null)
                    throw new Exception("An assignment must have at least 1 status!");

                if (currentState.StateId == EnumState.SLA1.ToString())
                    partnerWaitingAssignments.Add(item.PartnerBranchId);
                else if (currentState.StateId == EnumState.SLA2.ToString())
                    partnerAcceptedAssignments.Add(item.PartnerBranchId);
            }

            return _partnerBranchRepo.GetBranchesByLeadType(leadTypeId)
                .Where(w => !partnerWaitingAssignments.Contains(w.Id) && !partnerAcceptedAssignments.Contains(w.Id));
        }
    }
}