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
    public class SalesPeopleController : BaseController
    {
        private IUnitOfWork _uow;
        private ISalesPersonRepository _salesRepo;

        public SalesPeopleController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _salesRepo = unitOfWork.SalesPersonRepository;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_salesRepo.Get(), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new SalesPerson();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _salesRepo.Add(model);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Guid key, string values)
        {
            var model = _salesRepo.GetByUid(key);
            if (model == null)
                return StatusCode(409, "Sales person of the branch not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _salesRepo.Update(model);

            return Ok();
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var model = _salesRepo.GetByUid(key);
            _salesRepo.Remove(model);
        }

        [HttpGet]
        public object GetSalesPeopleByBranch(Guid branchId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_salesRepo.GetSalesPeopleByBranch(branchId), loadOptions);
        }
    }
}