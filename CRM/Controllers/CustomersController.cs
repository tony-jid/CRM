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

namespace CRM.Controllers
{
    [Authorize(Roles = nameof(EnumApplicationRole.Admin) + "," + nameof(EnumApplicationRole.Manager) + "," + nameof(EnumApplicationRole.Agent))]
    public class CustomersController : BaseController
    {
        private IUnitOfWork _uow;
        private ICustomerRepository _cusRepo;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _cusRepo = unitOfWork.CustomerRepository;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_cusRepo.Get(), loadOptions);
        }

        [HttpGet]
        public object GetForSelectionBox()
        {
            return _cusRepo.Get().Select(s => new {
                Id = s.Id,
                CustomerUnique = String.Format("{0} ({1})", s.ContactName, s.EMail)
            });
        }

        [HttpGet]
        public object GetForLookup()
        {
            return _cusRepo.Get().Select(s => new {
                Id = s.Id,
                CustomerUnique = String.Format("{0} ({1})", s.ContactName, s.EMail)
            });
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new Customer();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _cusRepo.Add(model);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public IActionResult Put(Guid key, string values)
        {
            var model = _cusRepo.GetByUid(key);
            if (model == null)
                return StatusCode(409, "Customer not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _cusRepo.Update(model);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public IActionResult Delete(Guid key)
        {
            var model = _cusRepo.GetByUid(key);

            _cusRepo.Remove(model);

            if (_uow.Commit(this.ModelState))
                return Ok();
            else
                return BadRequest(GetFullErrorMessage(this.ModelState));
        }
    }
}