using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    public class PartnersController : BaseController
    {
        private IUnitOfWork _uow;
        private IPartnerRepository _partnerRepo;

        public PartnersController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _partnerRepo = unitOfWork.PartnerRepository;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_partnerRepo.Get(), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new Partner();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _partnerRepo.Add(model);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Guid key, string values)
        {
            var model = _partnerRepo.GetByUid(key);
            if (model == null)
                return StatusCode(409, "Partner not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _partnerRepo.Update(model);

            return Ok();
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var model = _partnerRepo.GetByUid(key);

            _partnerRepo.Remove(model);
        }
    }
}