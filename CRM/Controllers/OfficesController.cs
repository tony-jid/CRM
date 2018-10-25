using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Enum;
using CRM.Models;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    [Authorize(Roles = nameof(EnumApplicationRole.Admin) + "," + nameof(EnumApplicationRole.Manager) + "," + nameof(EnumApplicationRole.Agent))]
    public class OfficesController : BaseController
    {
        private IUnitOfWork _uow;
        private IOfficeRepository _officeRepo;

        public OfficesController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _officeRepo = _uow.OfficeRepository;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_officeRepo.Get(), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new Office();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _officeRepo.Add(model);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            var model = _officeRepo.Get(key);
            if (model == null)
                return StatusCode(409, "Office not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _officeRepo.Update(model);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            var model = _officeRepo.Get(key);

            _officeRepo.Remove(model);

            if (_uow.Commit(this.ModelState))
                return Ok();
            else
                return BadRequest(GetFullErrorMessage(this.ModelState));
        }

        [HttpGet]
        public object GetOfficesByCompany(int companyId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_officeRepo.GetOfficesByCompany(companyId), loadOptions);
        }
    }
}
