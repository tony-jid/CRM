using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
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

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new Customer();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _cusRepo.Add(model);
            _uow.Commit();

            return Ok();
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

            return Ok();
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var model = _cusRepo.GetByUid(key);

            _cusRepo.Remove(model);
        }
    }
}