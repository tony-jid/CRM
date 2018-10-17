using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Enum;
using CRM.Models;
using CRM.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    [Authorize(Roles = nameof(EnumApplicationRole.Admin) + "," + nameof(EnumApplicationRole.Manager))]
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
            base.SetViewDataModalTarget();
            return View(_comRepo.GetFirst());
        }

        [HttpPut]
        public JsonResult UpdateCompany([FromBody]Company data)
        {
            var model = _comRepo.GetFirst();
            if (model == null)
                return Json(StatusCode(409, "Company not found"));

            model.Name = data.Name;
            model.ABN = data.ABN;
            model.GST = data.GST;

            _comRepo.Update(model);

            return Json(_uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError));
        }
    }
}