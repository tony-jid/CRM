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
    [Authorize(Roles = nameof(EnumApplicationRole.Admin) + "," + nameof(EnumApplicationRole.Manager))]
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
            var agentVMs = this.GetAgentVMs(_agentRepo.Get());
            agentVMs = this.FilterAgentsByActiveUser(agentVMs);

            return DataSourceLoader.Load(agentVMs, loadOptions);
        }

        [HttpGet]
        public object GetAgentsByOffice(int officeId, DataSourceLoadOptions loadOptions)
        {
            var agentVMs = this.GetAgentVMs(_agentRepo.GetAgentsByOffice(officeId));
            agentVMs = this.FilterAgentsByActiveUser(agentVMs);

            return DataSourceLoader.Load(agentVMs, loadOptions);
        }

        [HttpGet]
        public object GetAgentRoles()
        {
            var user = _accountManager.GetUserAsync(User.Identity.Name).Result;
            var roleNames = _accountManager.GetAgentRolesAsync(user).Result;

            return roleNames;
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new Agent();
            JsonConvert.PopulateObject(values, model);

            var modelVM = new AgentVM();
            JsonConvert.PopulateObject(values, modelVM);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var user = new ApplicationUser() { UserName = model.EMail, Email = model.EMail };

            //IdentityResult result = await _accountManager.CreateAccountAsync(user, EnumApplicationRole.Agent);
            IdentityResult result = _accountManager.CreateAccountAsync(user, modelVM.RoleName != null ? modelVM.RoleName : nameof(EnumApplicationRole.Agent)).Result;

            if (result.Succeeded)
            {
                _accountManager.SendEmailConfirmationAsync(user, this.Request, this.Url).Wait();

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
        public IActionResult Put(Guid key, string values)
        {
            var newModel = new AgentVM();
            JsonConvert.PopulateObject(values, newModel);

            var oldModel = _agentRepo.GetByUid(key);
            if (oldModel == null)
                return StatusCode(409, "Agent not found");

            if (ChangeEmailAsync(oldModel, newModel).Result) {
                JsonConvert.PopulateObject(values, oldModel);

                if (!TryValidateModel(oldModel))
                    return BadRequest(GetFullErrorMessage(ModelState));

                var user = _accountManager.GetUserAsync(oldModel.EMail).Result;
                var succeeded = true;

                if (newModel.RoleName != null)
                    succeeded = _accountManager.ChangeRoleAsync(user, newModel.RoleName).Result;

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
        public IActionResult Delete(Guid key)
        {
            var model = _agentRepo.GetByUid(key);

            var result = _accountManager.DeleteAccountAsync(model.EMail).Result;

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

        public List<AgentVM> FilterAgentsByActiveUser(List<AgentVM> agentVMs)
        {
            var activeUser = _accountManager.GetUserAsync(User.Identity.Name).Result;
            var activeUserRoleName = _accountManager.GetRoleAsync(activeUser).Result;

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

        private List<AgentVM> GetAgentVMs(IEnumerable<Agent> agents)
        {
            var agentVMs = new List<AgentVM>();
            
            foreach (var agent in agents)
            {
                agentVMs.Add(this.GetAgentVM(agent));
            }

            return agentVMs;
        }

        private AgentVM GetAgentVM(Agent agent)
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
                RoleName = _accountManager.GetRoleAsync(agent.ApplicationUser).Result
            };
        }
    }
}
