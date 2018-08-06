using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    public class PartnerBranchesController : BaseController
    {
        private IUnitOfWork _uow;
        private IPartnerBranchRepository _partnerBranchRepo;

        public PartnerBranchesController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _partnerBranchRepo = _uow.PartnerBranchRepository;
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

            return Ok();
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

            return Ok();
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var model = _partnerBranchRepo.GetByUid(key);

            _partnerBranchRepo.Remove(model);
        }

        [HttpGet]
        public object GetBranchesByPartner(Guid partnerId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_partnerBranchRepo.GetBranchesByPartner(partnerId), loadOptions);
        }
    }
}