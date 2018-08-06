using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    public class CompanyController : BaseController
    {
        private IUnitOfWork _uow;
        private ICompanyRepository _comRepo;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _comRepo = unitOfWork.CompanyRepository;
        }
        
        public override IActionResult Index()
        {
            return View(_comRepo.GetFirst());
        }

        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            var model = _comRepo.GetFirst();
            if (model == null)
                return StatusCode(409, "Company not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _comRepo.Update(model);

            return Ok();
        }
    }
}