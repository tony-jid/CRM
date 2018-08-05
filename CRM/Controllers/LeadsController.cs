using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    public class LeadsController : BaseController
    {
        private IUnitOfWork _uow;
        private ILeadRepository _leadRepo;

        public LeadsController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _leadRepo = unitOfWork.LeadRepository;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_leadRepo.Get(), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(Guid customerId, string values)
        {
            var model = new Lead();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _leadRepo.Add(model);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Guid key, string values)
        {
            var model = _leadRepo.GetByUid(key);
            if (model == null)
                return StatusCode(409, "Lead not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _leadRepo.Update(model);

            return Ok();
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var model = _leadRepo.GetByUid(key);

            _leadRepo.Remove(model);
        }

        public object GetLeadsByCustomer(Guid customerId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_leadRepo.GetLeadsByCustomer(customerId), loadOptions);
        }
    }
}
