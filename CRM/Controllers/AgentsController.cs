using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Repositories;
using CRM.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    public class AgentsController : BaseController
    {
        private IUnitOfWork _uow;
        private IAgentRepository _agentRepo;
        private AccountManager _accountManager;

        public AgentsController(IUnitOfWork unitOfWork, IEmailSender emailSender
            , UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _uow = unitOfWork;
            _agentRepo = unitOfWork.AgentRepository;
            _accountManager = new AccountManager(userManager, signInManager, emailSender);
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_agentRepo.Get(), loadOptions);
        }

        [HttpGet]
        public object GetAgentsByOffice(int officeId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_agentRepo.GetAgentsByOffice(officeId), loadOptions);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new Agent();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var user = new ApplicationUser() { UserName = model.EMail, Email = model.EMail };

            IdentityResult result = await _accountManager.CreateAccountAsync(user);
            
            if (result.Succeeded)
            {
                await _accountManager.SendEmailConfirmationAsync(user, this.Request, this.Url);

                model.ApplicationUserId = user.Id;
                _agentRepo.Add(model);

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
            var model = _agentRepo.GetByUid(key);
            if (model == null)
                return StatusCode(409, "Agent not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _agentRepo.Update(model);

            return _uow.Commit() ? Ok() : StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var model = _agentRepo.GetByUid(key);

            _agentRepo.Remove(model);

            _uow.Commit();
        }
    }
}
