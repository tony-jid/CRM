using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Repositories;
using CRM.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    public class SalesPeopleController : BaseController
    {
        private IUnitOfWork _uow;
        private ISalesPersonRepository _salesRepo;
        private AccountManager _accountManager;

        public SalesPeopleController(IUnitOfWork unitOfWork, IEmailSender emailSender
            , UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _uow = unitOfWork;
            _salesRepo = unitOfWork.SalesPersonRepository;
            _accountManager = new AccountManager(userManager, roleManager, signInManager, emailSender);
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_salesRepo.Get(), loadOptions);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new SalesPerson();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var user = new ApplicationUser() { UserName = model.EMail, Email = model.EMail };

            IdentityResult result = await _accountManager.CreateAccountAsync(user, Enum.EnumApplicationRole.Partner);

            if (result.Succeeded)
            {
                await _accountManager.SendEmailConfirmationAsync(user, this.Request, this.Url);

                model.ApplicationUserId = user.Id;
                _salesRepo.Add(model);

                return _uow.Commit() ? Ok() : StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
            else
            {
                _accountManager.AddModelStateErrors(this.ModelState, result);
                return BadRequest(GetFullErrorMessage(this.ModelState));
            }
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

            return _uow.Commit() ? Ok() : StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var model = _salesRepo.GetByUid(key);
            _salesRepo.Remove(model);

            _uow.Commit();
        }

        [HttpGet]
        public object GetSalesPeopleByBranch(Guid branchId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_salesRepo.GetSalesPeopleByBranch(branchId), loadOptions);
        }
    }
}