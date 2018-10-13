using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Enum;
using CRM.Models;
using CRM.Models.ViewModels;
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
        public async Task<object> Get(DataSourceLoadOptions loadOptions)
        {
            var agentVMs = await GetAgentVMs(_agentRepo.Get());
            agentVMs = await FilterAgentsByActiveUser(agentVMs);

            return DataSourceLoader.Load(agentVMs, loadOptions);
        }

        [HttpGet]
        public async Task<object> GetAgentsByOffice(int officeId, DataSourceLoadOptions loadOptions)
        {
            var agentVMs = await GetAgentVMs(_agentRepo.GetAgentsByOffice(officeId));
            agentVMs = await FilterAgentsByActiveUser(agentVMs);

            return DataSourceLoader.Load(agentVMs, loadOptions);
        }

        [HttpGet]
        public async Task<object> GetAgentRoles()
        {
            //var user = await _accountManager.GetUserAsync("mala@mail.com"); // for testing
            var user = await _accountManager.GetUserAsync(User.Identity.Name);
            var roleNames = await _accountManager.GetAgentRolesAsync(user);

            return roleNames;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new Agent();
            JsonConvert.PopulateObject(values, model);

            var modelVM = new AgentVM();
            JsonConvert.PopulateObject(values, modelVM);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var user = new ApplicationUser() { UserName = model.EMail, Email = model.EMail };

            //IdentityResult result = await _accountManager.CreateAccountAsync(user, EnumApplicationRole.Agent);
            IdentityResult result = await _accountManager.CreateAccountAsync(user, modelVM.RoleName);

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
            var newModel = new AgentVM();
            JsonConvert.PopulateObject(values, newModel);

            var oldModel = _agentRepo.GetByUid(key);
            if (oldModel == null)
                return StatusCode(409, "Agent not found");

            if (await ChangeEmailAsync(oldModel, newModel)) {
                JsonConvert.PopulateObject(values, oldModel);

                if (!TryValidateModel(oldModel))
                    return BadRequest(GetFullErrorMessage(ModelState));

                var user = await _accountManager.GetUserAsync(oldModel.EMail);
                var succeeded = await _accountManager.ChangeRoleAsync(user, newModel.RoleName);

                if (succeeded)
                {
                    // *** Updating agent role around here
                    _agentRepo.Update(oldModel);

                    return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
                }
                else
                {
                    throw new ApplicationException("Failed changing user role.");
                }
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
                    var result = await _accountManager.ChangeEmailAsync(oldModel.EMail, newModel.EMail, this.Request, this.Url);

                    if (!result.Succeeded)
                    {
                        _accountManager.AddModelStateErrors(this.ModelState, result);
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<List<AgentVM>> FilterAgentsByActiveUser(List<AgentVM> agentVMs)
        {
            var activeUser = await _accountManager.GetUserAsync(User.Identity.Name);
            var activeUserRoleName = await _accountManager.GetRoleAsync(activeUser);

            List<AgentVM> beingRemovedAgents = new List<AgentVM>();
            if (activeUserRoleName.Equals(nameof(EnumApplicationRole.Admin)))
            {

            }
            else if (activeUserRoleName.Equals(nameof(EnumApplicationRole.Manager)))
            {
                foreach (var agent in agentVMs)
                {
                    if (agent.RoleName.Equals(nameof(EnumApplicationRole.Manager)))
                        beingRemovedAgents.Add(agent);
                }
            }
            
            if (beingRemovedAgents.Count() > 0)
            {
                var listAgentVMs = agentVMs.ToList();
                foreach (var item in beingRemovedAgents)
                {
                    listAgentVMs.Remove(item);
                }

                return listAgentVMs;
            }
            else
            {
                return agentVMs;
            }

            
        }

        private async Task<List<AgentVM>> GetAgentVMs(IEnumerable<Agent> agents)
        {
            var agentVMs = new List<AgentVM>();
            
            foreach (var agent in agents)
            {
                agentVMs.Add(await GetAgentVM(agent));
            }

            return agentVMs;
        }

        private async Task<AgentVM> GetAgentVM(Agent agent)
        {
            return new AgentVM() {
                ApplicationUserId = agent.ApplicationUserId,
                ApplicationUser = agent.ApplicationUser,
                Id = agent.Id,
                EMail = agent.EMail,
                ContactName = agent.ContactName,
                ContactNumber = agent.ContactNumber,
                Office = agent.Office,
                OfficeId = agent.OfficeId,
                RoleName = await _accountManager.GetRoleAsync(agent.ApplicationUser)
            };
        }
    }
}
