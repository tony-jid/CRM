using CRM.Models;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Controllers
{
    public class LeadTypesController : BaseController
    {
        private IUnitOfWork _uow;
        private ILeadTypeRepository _leadTypeRepo;

        public LeadTypesController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _leadTypeRepo = unitOfWork.LeadTypeRepository;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_leadTypeRepo.Get(), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new LeadType();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _leadTypeRepo.Add(model);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            var model = _leadTypeRepo.Get(key);
            if (model == null)
                return StatusCode(409, "LeadType not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _leadTypeRepo.Update(model);

            return Ok();
        }

        [HttpDelete]
        public void Delete(int key)
        {
            var model = _leadTypeRepo.Get(key);

            _leadTypeRepo.Remove(model);
        }
    }
}