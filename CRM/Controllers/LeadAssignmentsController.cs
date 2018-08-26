using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    public class LeadAssignmentsController : BaseController
    {
        private IUnitOfWork _uow;
        private ILeadAssignmentRepository _leadAssRepo;

        public LeadAssignmentsController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _leadAssRepo = unitOfWork.LeadAssignmentRepository;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions, Guid leadId)
        {
            var leadAssignments = this.GetLeadAssignmentViewModels(leadId);

            return DataSourceLoader.Load(leadAssignments, loadOptions);
        }

        [HttpDelete]
        public void Delete(int key)
        {
            var model = _leadAssRepo.Get(key);

            _leadAssRepo.Remove(model);
        }

        [HttpPost]
        public JsonResult AjaxPostToAssignPartners([FromBody]LeadAssignmentSelectedPartnerViewModel data)
        {
            _leadAssRepo.AddByViewModel(data);
            
            return Json(Ok());
        }

        protected LeadAssignmentViewModel GetLeadAssignmentViewModel(LeadAssignment item)
        {
            var itemVM = new LeadAssignmentViewModel();
            itemVM.Id = item.Id;

            itemVM.PartnerId = item.PartnerBranch.Partner.Id;
            itemVM.PartnerName = item.PartnerBranch.Partner.Name;
            itemVM.PartnerLogo = ImageHelper.PATH_PARTNER + item.PartnerBranch.Partner.Logo;

            itemVM.PartnerBranchId = item.PartnerBranch.Id;
            itemVM.PartnerBranchStreetAddress = item.PartnerBranch.Address.StreetAddress;
            itemVM.PartnerBranchSuburb = item.PartnerBranch.Address.Suburb;
            itemVM.PartnerBranchState = item.PartnerBranch.Address.State;
            itemVM.PartnerBranchPostCode = item.PartnerBranch.Address.PostCode;
            itemVM.PartnerBranchAddress = AddressHelper.MergeAddress(itemVM.PartnerBranchStreetAddress, itemVM.PartnerBranchSuburb, itemVM.PartnerBranchState, itemVM.PartnerBranchPostCode);

            // Current status
            var currentStatus = item.LeadAssignmentStates.OrderByDescending(o => o.ActionTimestamp).FirstOrDefault();
            itemVM.StatusId = currentStatus.State.Id;
            itemVM.StatusName = currentStatus.State.Name;
            itemVM.StatusTag = StatusHelper.GetHtmlSmallBadge(currentStatus.State.Id);

            // Actions of current status
            var actions = currentStatus.State.StateActions.Select(s => new ActionViewModel
            {
                PartnerBranchId = itemVM.PartnerBranchId,
                LeadAssignmentId = itemVM.Id,
                ControllerName = s.Action.ControllerName,
                ActionName = s.Action.ActionName,
                DisplayName = s.Action.DisplayName,
                Icon = s.Action.Icon,
                NextStateId = s.Action.NextStateId
            }).ToList();

            itemVM.Actions = actions;

            var histories = item.LeadAssignmentStates
                //.Where(w => w.StateId != currentStatus.StateId) // *show all
                .Select(s => HistoryHelper.GetHtmlHistoryLine(s.ActionTimestamp, s.Action.ToLower(), s.Actor));

            itemVM.History = HistoryHelper.GetHtmlHistoryTag(histories.ToList());

            return itemVM;
        }

        protected List<LeadAssignmentViewModel> GetLeadAssignmentViewModels(Guid leadId)
        {
            List<LeadAssignmentViewModel> leadVMs = new List<LeadAssignmentViewModel>();

            var leadAssignments = _leadAssRepo.GetByLead(leadId);
            foreach (var item in leadAssignments)
            {
                leadVMs.Add(this.GetLeadAssignmentViewModel(item));
            }

            return leadVMs;
        }
    }
}