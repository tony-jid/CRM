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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    public class LeadsController : BaseController
    {
        private IUnitOfWork _uow;
        private ILeadRepository _leadRepo;

        public LeadsController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _leadRepo = unitOfWork.LeadRepository;
        }

        [HttpGet("{leadId}")]
        public IActionResult Assignments(Guid leadId)
        {
            var lead = _leadRepo.GetByUid(leadId);
            return View(lead);
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
            return DataSourceLoader.Load(this.GetLeadViewModels(), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new Lead();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _leadRepo.Add(model);

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
        public void Delete(Guid key)
        {
            var model = _leadRepo.GetByUid(key);

            _leadRepo.Remove(model);
            _uow.Commit();
        }

        protected LeadViewModel GetLeadViewModel(Lead item)
        {
            var itemVM = new LeadViewModel();
            itemVM.Id = item.Id;
            itemVM.Details = item.Details;

            itemVM.LeadTypeId = item.LeadType.Id;
            itemVM.LeadTypeName = item.LeadType.Name;
            itemVM.LeadTypeImage = ImageHelper.PATH_LEAD_TYPE + item.LeadType.Image;

            itemVM.CustomerId = item.Customer.Id;
            itemVM.CustomerName = item.Customer.ContactName;
            itemVM.CustomerBusinessName = item.Customer.BusinessName;
            itemVM.CustomerContactNumber = item.Customer.ContactNumber;
            itemVM.CustomerEmail = item.Customer.EMail;

            // Current status
            var currentStatus = item.LeadStates.OrderByDescending(o => o.ActionTimestamp).FirstOrDefault();
            itemVM.StatusId = currentStatus.State.Id;
            itemVM.StatusName = currentStatus.State.Name;
            itemVM.StatusTag = StatusHelper.GetHtmlSmallBadge(currentStatus.State.Id);

            // Actions of current status
            var actions = currentStatus.State.StateActions.Select(s => new ActionLeadViewModel
            {
                CustomerId = itemVM.CustomerId,
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

            // Histories
            var histories = item.LeadStates
                //.Where(w => w.StateId != currentStatus.StateId) // *show all
                .Select(s => HistoryHelper.GetHtmlHistoryLine(s.ActionTimestamp, s.Action.ToLower(), s.Actor));

            itemVM.History = HistoryHelper.GetHtmlHistoryTag(histories.ToList());

            return itemVM;
        }

        protected List<LeadViewModel> GetLeadViewModels()
        {
            List<LeadViewModel> leadVMs = new List<LeadViewModel>();

            var leads = _leadRepo.Get();
            foreach (var item in leads)
            {
                leadVMs.Add(this.GetLeadViewModel(item));
            }

            return leadVMs;
        }
    }
}
