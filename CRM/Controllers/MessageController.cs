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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    [Authorize(Roles = nameof(EnumApplicationRole.Admin)
        + "," + nameof(EnumApplicationRole.Manager)
        + "," + nameof(EnumApplicationRole.Agent)
        + "," + nameof(EnumApplicationRole.Partner))]
    public class MessageController : BaseController
    {
        private IUnitOfWork _uow;
        private ICompanyRepository _companyRepo;
        private IMessageRepository _msgRepo;
        private IPartnerRepository _partnerRepo;
        private ILeadRepository _leadRepo;
        private ILeadAssignmentRepository _leadAssignmentRepo;
        private ILeadTypeRepository _leadTypeRepo;
        private IEmailSender _emailSender;
        private AccountManager _accountManager;

        public MessageController(IUnitOfWork unitOfWork, IEmailSender emailSender
            , UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _uow = unitOfWork;
            _companyRepo = unitOfWork.CompanyRepository;
            _msgRepo = unitOfWork.MessageRepository;
            _partnerRepo = unitOfWork.PartnerRepository;
            _leadRepo = unitOfWork.LeadRepository;
            _leadAssignmentRepo = unitOfWork.LeadAssignmentRepository;
            _leadTypeRepo = unitOfWork.LeadTypeRepository;

            _emailSender = emailSender;
            _accountManager = new AccountManager(userManager, roleManager, signInManager, emailSender);
        }

        [Authorize(Roles = nameof(EnumApplicationRole.Admin) + "," + nameof(EnumApplicationRole.Manager) + "," + nameof(EnumApplicationRole.Agent))]
        public virtual IActionResult Templates()
        {
            return View();
        }

        [HttpGet]
        public object GetRecipients(DataSourceLoadOptions loadOptions)
        {
            IEnumerable<RecipientViewModel> recipients;

            if (!User.IsInRole(nameof(EnumApplicationRole.Partner)))
            {
                recipients = _msgRepo.GetRecipients();
            }
            else
            {
                var user = _accountManager.GetUserAsync(User.Identity.Name).Result;
                var salesPersonId = user.Id;
                var partner = _partnerRepo.GetBySalesPerson(salesPersonId);

                recipients = _msgRepo.GetRecipientsForPartner(partner.Id);
            }

            return DataSourceLoader.Load(recipients, loadOptions);
        }

        [HttpPost]
        public JsonResult Send([FromBody]MessageViewModel data)
        {
            _emailSender.SendEmailAsync(data.Recipients.Select(s => s.Email).ToArray(), data.Subject, data.Message);

            return Json(data);
        }

        [HttpPost]
        public JsonResult SendLeadMessage([FromBody]MessageViewModel data)
        {
            _leadRepo.SetState(new Guid(data.LeadId), EnumState.S0, EnumStateActionTaken.Messaged, User.Identity.Name);
            _uow.Commit();
            _emailSender.SendEmailAsync(data.Recipients.Select(s => s.Email).ToArray(), data.Subject, data.Message);

            return Json(data);
        }

        [HttpPost]
        public JsonResult SendLeadRequestInfo([FromBody]MessageViewModel data)
        {
            _leadRepo.SetState(new Guid(data.LeadId), EnumState.SL4, EnumStateActionTaken.Requested_Info, User.Identity.Name);
            _uow.Commit();
            _emailSender.SendEmailAsync(data.Recipients.Select(s => s.Email).ToArray(), data.Subject, data.Message);

            return Json(data);
        }

        [HttpPost]
        public JsonResult SendAssignmentMessage([FromBody]MessageViewModel data)
        {
            _leadAssignmentRepo.SetState(data.LeadAssignmentId, EnumState.S0, EnumStateActionTaken.Messaged, User.Identity.Name);
            _uow.Commit();
            _emailSender.SendEmailAsync(data.Recipients.Select(s => s.Email).ToArray(), data.Subject, data.Message);

            return Json(data);
        }

        //[HttpGet]
        //public object GetTemplates()
        //{
        //    return _msgRepo.GetTemplates();
        //}
        [HttpGet]
        [Authorize(Roles = nameof(EnumApplicationRole.Admin) + "," + nameof(EnumApplicationRole.Manager) + "," + nameof(EnumApplicationRole.Agent))]
        public object GetTemplates(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_msgRepo.GetTemplates(), loadOptions);
        }

        [HttpPost]
        public IActionResult AddTemplate(string values)
        {
            var model = new MessageTemplate();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _msgRepo.AddTemplate(model);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public IActionResult UpdateTemplate(int key, string values)
        {
            var model = _msgRepo.GetTemplate(key);
            if (model == null)
                return StatusCode(409, "Message Template not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _msgRepo.UpdateTemplate(model);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public IActionResult RemoveTemplate(int key)
        {
            var model = _msgRepo.GetTemplate(key);
            if (model == null)
                return StatusCode(409, "Message Template not found");

            _msgRepo.RemoveTemplate(model);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task SendCompanyLeadHooked(Guid leadId, IUrlHelper url, HttpRequest httpRequest)
        {
            var company = _uow.CompanyRepository.GetFirst();

            if (url == null)
                if (this.Url == null)
                    throw new ApplicationException("IUrlHelper in the context is null.");

            if (httpRequest == null)
                if (this.Request == null)
                    throw new ApplicationException("HttpRequest in the context is null.");

            var callbackLink = (url ?? this.Url).LeadHookedCallbackLink(leadId, (httpRequest != null) ? httpRequest.Scheme : this.Request.Scheme);

            var lead = _leadRepo.GetByUid(leadId);
            string leadTypeName = _leadTypeRepo.Get(lead.LeadTypeId)?.Name.ToLower();
            string subject = $"New {leadTypeName} lead link";

            await _emailSender.SendCompanyLeadHookedAsync(subject, company.Email, callbackLink);
        }

        [HttpPost]
        public async Task SendCompanyPartnerResponse(Guid leadId, int leadAssignmentId, string responseText, IUrlHelper url, HttpRequest httpRequest)
        {
            var company = _uow.CompanyRepository.GetFirst();

            List<string> emails = new List<string>();
            
            var partner = _uow.PartnerRepository.GetByLeadAssignment(leadAssignmentId);

            if (url == null)
                if (this.Url == null)
                    throw new ApplicationException("IUrlHelper in the context is null.");

            if (httpRequest == null)
                if (this.Request == null)
                    throw new ApplicationException("HttpRequest in the context is null.");

            var callbackLink = (url ?? this.Url).LeadHookedCallbackLink(leadId, (httpRequest != null) ? httpRequest.Scheme : this.Request.Scheme);

            var lead = _leadRepo.GetByUid(leadId);
            string leadTypeName = _leadTypeRepo.Get(lead.LeadTypeId)?.Name.ToLower();
            string subject = $"Lead {leadTypeName} {responseText} by {partner.Name}";

            await _emailSender.SendCompanyPartnerResponseAsync(subject, company.Email, callbackLink);
        }

        [HttpPost]
        public async Task SendPartnerLeadAssigned(List<Guid> branchIds, IUrlHelper url, HttpRequest httpRequest, string leadDetails, string leadTypeName)
        {
            List<string> emails = new List<string>();

            foreach (var branchId in branchIds)
            {
                var partner = _uow.PartnerRepository.GetByBranch(branchId);
                var salesPeople = _uow.SalesPersonRepository.GetByPartner(partner.Id);
                emails.AddRange(salesPeople.Select(s => s.EMail));
            }

            if (url == null)
                if (this.Url == null)
                    throw new ApplicationException("IUrlHelper in the context is null.");

            if (httpRequest == null)
                if (this.Request == null)
                    throw new ApplicationException("HttpRequest in the context is null.");

            var callbackLink = (url ?? this.Url).PartnerLeadAssignedCallbackLink((httpRequest != null) ? httpRequest.Scheme : this.Request.Scheme);

            string subject = $"Lead {leadTypeName.ToLower()} from {_companyRepo.GetFirst()?.Name}";

            await _emailSender.SendPartnerLeadAssignedAsync(subject, emails.ToArray(), callbackLink, leadDetails);
        }
    }
}