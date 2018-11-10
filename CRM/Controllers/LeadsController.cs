using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CRM.Helpers;
using System.Web;
using CRM.Enum;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    [Authorize(Roles = nameof(EnumApplicationRole.Admin) 
        + "," + nameof(EnumApplicationRole.Manager) 
        + "," + nameof(EnumApplicationRole.Agent))]
    public class LeadsController : BaseController
    {
        private IUnitOfWork _uow;
        private LeadRepository _leadRepo;
        private IPartnerRepository _partnerRepo;
        private ActionRepository _actionRepo;

        public LeadsController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _leadRepo = unitOfWork.LeadRepository;
            _partnerRepo = unitOfWork.PartnerRepository;
            _actionRepo = unitOfWork.ActionRepository;
        }

        [HttpGet("{leadId}")]
        public IActionResult Assignments(Guid leadId)
        {
            var lead = _leadRepo.GetByUid(leadId);
            return View(lead);
        }

        // Using "Partner/Portal" instead
        [HttpGet("{partnerId}")]
        public IActionResult Partner(Guid partnerId)
        {
            partnerId = new Guid("5DD63725-0737-4D40-5853-08D62779DCF3");

            var partner = _partnerRepo.GetByUid(partnerId);
            return View(partner);
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_leadRepo.Get(), loadOptions);
        }

        [HttpGet]
        public object GetLeadsByCustomer(Guid customerId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_leadRepo.GetLeadsByCustomer(customerId), loadOptions);
        }

        [HttpGet]
        public object GetViewModel(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(this.GetLeadViewModels(_leadRepo.Get()), loadOptions);
        }

        [HttpGet]
        public object GetViewModelByCustomer(Guid id, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(this.GetLeadViewModels(_leadRepo.GetLeadsByCustomer(id)), loadOptions);
        }

        [HttpGet]
        public object GetGroupActions(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_actionRepo.GetGroupActions(), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new Lead();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _leadRepo.Add(model, User.Identity.Name);

            return _uow.Commit() ? Ok() : StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public IActionResult Put(Guid key, string values)
        {
            var model = _leadRepo.GetByUid(key);
            if (model == null)
                return StatusCode(409, "Lead not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _leadRepo.Update(model);
            _uow.Commit();

            return _uow.Commit() ? Ok() : StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public IActionResult Delete(Guid key)
        {
            var model = _leadRepo.GetByUid(key);

            _leadRepo.Remove(model);

            if (_uow.Commit(this.ModelState))
                return Ok();
            else
                return BadRequest(GetFullErrorMessage(this.ModelState));
        }

        public LeadVM GetLeadViewModel(Lead item)
        {
            var itemVM = new LeadVM();
            itemVM.Id = item.Id;
            itemVM.Details = item.Details;

            itemVM.LeadTypeId = item.LeadType.Id;
            itemVM.LeadTypeName = item.LeadType.Name;
            itemVM.LeadTypeImage = ImageHelper.PATH_CLIENT_LEAD_TYPE + item.LeadType.Image;

            itemVM.CustomerId = item.Customer.Id;
            itemVM.CustomerUnique = String.Format("{0} ({1})", item.Customer.ContactName, item.Customer.EMail);

            itemVM.CustomerName = item.Customer.ContactName;
            itemVM.CustomerBusinessName = item.Customer.BusinessName;
            itemVM.CustomerContactNumber = item.Customer.ContactNumber;
            itemVM.CustomerEmail = item.Customer.EMail;

            itemVM.CustomerAddress = AddressHelper.MergeAddress(item.Customer.Address.StreetAddress, item.Customer.Address.Suburb, item.Customer.Address.State, item.Customer.Address.PostCode);
            itemVM.CustomerDetails = String.Format("Business: <b>{0}</b><br>Tel: <b>{1}</b><br>Address: <b>{2}</b>", itemVM.CustomerBusinessName, itemVM.CustomerContactNumber, itemVM.CustomerAddress);

            // Current status
            var currentStatus = item.LeadStates.Where(w => w.StateId != nameof(EnumState.S0)).OrderByDescending(o => o.ActionTimestamp).FirstOrDefault();
            itemVM.StatusId = currentStatus.State.Id;
            itemVM.StatusName = currentStatus.State.Name;
            itemVM.StatusTag = StatusHelper.GetHtmlBadge(currentStatus.State.Id, currentStatus.State.Name);

            // Actions of current status
            var actions = currentStatus.State.StateActions.Select(s => new ActionLeadVM
            {
                Id = s.ActionId,
                CustomerId = itemVM.CustomerId,
                CustomerEmail = itemVM.CustomerEmail,
                LeadId = itemVM.Id,
                ControllerName = s.Action.ControllerName,
                ActionName = s.Action.ActionName,
                ActionTarget = s.Action.ActionTarget,
                RequestType = s.Action.RequestType,
                DisplayName = s.Action.DisplayName,
                Icon = s.Action.Icon,
                NextStateId = s.Action.NextStateId
            }).ToList();

            itemVM.Actions = actions;

            // CreatedWhen
            var createdStatus = item.LeadStates.OrderBy(o => o.ActionTimestamp).FirstOrDefault();
            itemVM.CreatedOn = DateHelper.ConvertFromUtc(createdStatus.ActionTimestamp).Date;

            // Histories
            var histories = item.LeadStates
                .OrderByDescending(o => o.ActionTimestamp)
                //.Where(w => w.StateId != currentStatus.StateId) // *show all
                .Select(s => HistoryHelper.GetHtmlHistoryLine(DateHelper.ConvertFromUtc(s.ActionTimestamp), s.Action.ToLower(), s.Actor));

            itemVM.History = HistoryHelper.GetHtmlHistoryTag(histories.ToList());

            return itemVM;
        }

        public List<LeadVM> GetLeadViewModels(IEnumerable<Lead> leads)
        {
            List<LeadVM> leadVMs = new List<LeadVM>();

            foreach (var item in leads)
            {
                leadVMs.Add(this.GetLeadViewModel(item));
            }

            return leadVMs.OrderByDescending(o => o.CreatedOn).ToList();
        }
    }
}
