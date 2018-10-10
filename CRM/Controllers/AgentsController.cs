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
using Microsoft.AspNetCore.Http;
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
            , UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _uow = unitOfWork;
            _agentRepo = unitOfWork.AgentRepository;
            _accountManager = new AccountManager(userManager, roleManager, signInManager, emailSender);
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

            IdentityResult result = await _accountManager.CreateAccountAsync(user, Enum.EnumApplicationRole.Agent);
            
            if (result.Succeeded)
            {
                await _accountManager.SendEmailConfirmationAsync(user, this.Request, this.Url);

                model.ApplicationUserId = user.Id;
                _agentRepo.Add(model);

                return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                _accountManager.AddModelStateErrors(this.ModelState, result);
                return BadRequest(GetFullErrorMessage(this.ModelState));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values)
        {
            var newModel = new Agent();
            JsonConvert.PopulateObject(values, newModel);

            var oldModel = _agentRepo.GetByUid(key);
            if (oldModel == null)
                return StatusCode(409, "Agent not found");

            if (await ChangeEmailAsync(oldModel, newModel)) {
                JsonConvert.PopulateObject(values, oldModel);

                if (!TryValidateModel(oldModel))
                    return BadRequest(GetFullErrorMessage(ModelState));

                _agentRepo.Update(oldModel);

                return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return BadRequest(GetFullErrorMessage(this.ModelState));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var model = _agentRepo.GetByUid(key);

            var result = await _accountManager.DeleteAccountAsync(model.EMail);

            if (result.Succeeded)
            {
                // Do not have to remove data and commit manually, because Identity Service will do the job
                //_agentRepo.Remove(model);
                //return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);

                return Ok();
            }
            else
            {
                return BadRequest(GetFullErrorMessage(this.ModelState));
            }
        }

        private async Task<bool> ChangeEmailAsync(Agent oldModel, Agent newModel)
        {
            if (!String.IsNullOrEmpty(newModel.EMail))
            {
                if (!newModel.EMail.Equals(oldModel.EMail))
                {
                    var user = await _accountManager.GetUserAsync(oldModel.EMail);

                    var result = await _accountManager.ChangeEmailAsync(user, newModel.EMail);

                    if (result.Succeeded)
                    {
                        result = await _accountManager.ChangeUserNameAsync(user, newModel.EMail);

                        if (result.Succeeded)
                        {
                            await _accountManager.SendEmailConfirmationAsync(user, this.Request, this.Url);
                        }
                        else
                        {
                            // If changing UserName fails, then rollback the Email
                            await _accountManager.ChangeEmailAsync(user, oldModel.EMail);
                            _accountManager.AddModelStateErrors(this.ModelState, result);
                            return false;
                        }
                    }
                    else
                    {
                        _accountManager.AddModelStateErrors(this.ModelState, result);
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
