using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Enum;
using CRM.Helpers;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Repositories;
using CRM.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    public class LeadAssignmentsController : BaseController
    {
        private IUnitOfWork _uow;
        private ILeadRepository _leadRepo;
        private ISalesPersonRepository _salesRepo;
        private ActionPermissionRepository _actionPermissionRepo;
        private ILeadAssignmentRepository _leadAssRepo;
        private IEmailSender _emailSender;
        private MessageController _messageController;
        private AccountManager _accountManager;

        private string _userRoleName;
        private IEnumerable<ActionPermission> _actionPermissions;

        public LeadAssignmentsController(IUnitOfWork unitOfWork, IEmailSender emailSender
            , MessageController messageController
            , UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _uow = unitOfWork;
            _leadAssRepo = unitOfWork.LeadAssignmentRepository;
            _leadRepo = unitOfWork.LeadRepository;
            _salesRepo = unitOfWork.SalesPersonRepository;
            _actionPermissionRepo = unitOfWork.ActionPermissionRepository;

            _emailSender = emailSender;
            _messageController = messageController;
            _accountManager = new AccountManager(userManager, roleManager, signInManager, emailSender);
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(_leadAssRepo.Get());
        }

        [HttpGet]
        public object GetByLead(DataSourceLoadOptions loadOptions, Guid id)
        {
            var leadAssignments = _leadAssRepo.GetByLead(id);

            return DataSourceLoader.Load(this.GetLeadAssignmentViewModels(leadAssignments), loadOptions);
        }

        [HttpGet]
        public object GetByPartner(DataSourceLoadOptions loadOptions, Guid id)
        {
            var leadAssignments = _leadAssRepo.GetByPartner(id);
            
            return DataSourceLoader.Load(this.GetLeadAssignmentViewModels(leadAssignments), loadOptions);
        }

        [HttpDelete]
        public void Delete(int key)
        {
            var model = _leadAssRepo.Get(key);

            _leadAssRepo.Remove(model);
            _uow.Commit();
        }

        [HttpPost]
        public async Task<JsonResult> AjaxPostToAssignPartners([FromBody]LeadAssignmentSelectedPartnerViewModel data)
        {
            _leadAssRepo.AddByViewModel(data, User.Identity.Name);
            _leadRepo.SetLeadAssignedState(data.LeadId, User.Identity.Name);
            
            if (_uow.Commit())
            {
                await _messageController.SendPartnerLeadAssigned(data.PartnerBranchIds, this.Url, this.Request);

                return Json(Ok());
            }
            else
            {
                return Json(StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError));
            }
        }

        [HttpPost]
        public JsonResult CommentLead([FromBody]LeadAssignmentRatingVM data)
        {
            _leadAssRepo.CommentLeadAssignment(data.LeadAssignmentId, data.Comment, User.Identity.Name);

            if (_uow.Commit())
            {
                return Json(Ok(data.LeadId));
            }
            else
            {
                return Json(StatusCode(StatusCodes.Status500InternalServerError));
            }
        }

        [HttpPut]
        public async Task<JsonResult> Accept([FromBody]LeadAssignmentResponseVM data) // can process "int id" too
        {
            _leadAssRepo.AcceptAssignment(data, User.Identity.Name);

            if (_uow.Commit())
            {
                await _messageController.SendCompanyPartnerResponse(data.LeadId, data.LeadAssignmentId, "accepted", this.Url, this.Request);

                return Json(Ok(data.LeadId));
            }
            else
            {
                return Json(StatusCode(StatusCodes.Status500InternalServerError));
            }
        }

        [HttpPut]
        public async Task<JsonResult> Reject([FromBody]LeadAssignmentResponseVM data)
        {
            _leadAssRepo.RejectAssignment(data, User.Identity.Name);

            if (_uow.Commit())
            {
                await _messageController.SendCompanyPartnerResponse(data.LeadId, data.LeadAssignmentId, "rejected", this.Url, this.Request);

                return Json(Ok(data.LeadId));
            }
            else
            {
                return Json(StatusCode(StatusCodes.Status500InternalServerError));
            }
        }

        protected LeadAssignmentVM GetLeadAssignmentViewModel(LeadAssignment item)
        {
            var itemVM = new LeadAssignmentVM();
            itemVM.Id = item.Id;
            itemVM.LeadId = item.LeadId;

            if (item.Lead != null)
            {
                itemVM.LeadDetails = item.Lead.Details;

                if (item.Lead.LeadType != null)
                {
                    itemVM.LeadTypeName = item.Lead.LeadType.Name;
                    itemVM.LeadTypeImage = ImageHelper.PATH_CLIENT_LEAD_TYPE + item.Lead.LeadType.Image;
                }

                if (item.Lead.Customer != null)
                {
                    itemVM.CustomerId = item.Lead.Customer.Id;
                    itemVM.CustomerUnique = String.Format("{0} ({1})", item.Lead.Customer.ContactName, item.Lead.Customer.EMail);

                    itemVM.CustomerName = item.Lead.Customer.ContactName;
                    itemVM.CustomerBusinessName = item.Lead.Customer.BusinessName;
                    itemVM.CustomerEMail = item.Lead.Customer.EMail;
                    itemVM.CustomerContactNumber = item.Lead.Customer.ContactNumber;
                    itemVM.CustomerStreetAddress = item.Lead.Customer.Address.StreetAddress;
                    itemVM.CustomerSuburb = item.Lead.Customer.Address.Suburb;
                    itemVM.CustomerState = item.Lead.Customer.Address.State;
                    itemVM.CustomerPostCode = item.Lead.Customer.Address.PostCode;
                    itemVM.CustomerAddress = AddressHelper.MergeAddress(itemVM.CustomerStreetAddress, itemVM.CustomerSuburb, itemVM.CustomerState, itemVM.CustomerPostCode);

                    itemVM.CustomerDetails = String.Format("Business: <b>{0}</b><br>Tel: <b>{1}</b><br>Address: <b>{2}</b>", itemVM.CustomerBusinessName, itemVM.CustomerContactNumber, itemVM.CustomerAddress);
                }
            }

            itemVM.PartnerId = item.PartnerBranch.Partner.Id;
            itemVM.PartnerName = item.PartnerBranch.Partner.Name;
            itemVM.PartnerLogo = ImageHelper.PATH_CLIENT_PARTNER + item.PartnerBranch.Partner.Logo;

            itemVM.PartnerBranchId = item.PartnerBranch.Id;
            itemVM.PartnerBranchStreetAddress = item.PartnerBranch.Address.StreetAddress;
            itemVM.PartnerBranchSuburb = item.PartnerBranch.Address.Suburb;
            itemVM.PartnerBranchState = item.PartnerBranch.Address.State;
            itemVM.PartnerBranchPostCode = item.PartnerBranch.Address.PostCode;
            itemVM.PartnerBranchAddress = AddressHelper.MergeAddress(itemVM.PartnerBranchStreetAddress, itemVM.PartnerBranchSuburb, itemVM.PartnerBranchState, itemVM.PartnerBranchPostCode);

            // Current status
            var currentStatus = item.LeadAssignmentStates.Where(w => w.StateId != nameof(EnumState.S0)).OrderByDescending(o => o.ActionTimestamp).FirstOrDefault();
            itemVM.StatusId = currentStatus.State.Id;
            itemVM.StatusName = currentStatus.State.Name;
            itemVM.StatusTag = StatusHelper.GetHtmlBadge(currentStatus.State.Id, currentStatus.State.Name);
            itemVM.RatingTag = RatingHelper.GetHtmlRatingTag(item.Comment, item.CommentedBy, item.CommentedOn);

            if (string.IsNullOrEmpty(_userRoleName))
                _userRoleName = _accountManager.GetRoleAsync(_accountManager.GetUserAsync(User.Identity.Name).Result).Result;

            // Actions of current status
            var actions = currentStatus.State.StateActions.Where(w => this.IsActionAllowed(w.Action.Id, _userRoleName)).Select(s => new ActionLeadAssignmentVM
            {
                LeadId = itemVM.LeadId,
                PartnerBranchId = itemVM.PartnerBranchId,
                LeadAssignmentId = itemVM.Id,
                CustomerEmail = itemVM.CustomerEMail,
                PartnerEmails = _salesRepo.GetByPartner(itemVM.PartnerId).Select(person => person.EMail).ToList(),
                ControllerName = s.Action.ControllerName,
                ActionName = s.Action.ActionName,
                ActionTarget = s.Action.ActionTarget,
                RequestType = s.Action.RequestType,
                DisplayName = s.Action.DisplayName,
                Icon = s.Action.Icon,
                NextStateId = s.Action.NextStateId,
                Rating = new Rating() { Rate = item.Rate, Comment = item.Comment ?? string.Empty, CommentedOn = item.CommentedOn, CommentedBy = item.CommentedBy }
            }).ToList();

            itemVM.Actions = actions;

            // CreatedWhen
            var firstSssignedOn = item.LeadAssignmentStates.OrderBy(o => o.ActionTimestamp).FirstOrDefault();
            itemVM.AssignedOn = firstSssignedOn.ActionTimestamp;

            // History
            var histories = item.LeadAssignmentStates
                .OrderByDescending(o => o.ActionTimestamp)
                //.Where(w => w.StateId != currentStatus.StateId) // *show all
                .Select(s => HistoryHelper.GetHtmlHistoryLine(s.ActionTimestamp, s.Action.ToLower(), s.Actor));

            itemVM.History = HistoryHelper.GetHtmlHistoryTag(histories.ToList());

            return itemVM;
        }

        protected List<LeadAssignmentVM> GetLeadAssignmentViewModels(IEnumerable<LeadAssignment> leadAssignments)
        {
            List<LeadAssignmentVM> leadVMs = new List<LeadAssignmentVM>();
            
            foreach (var item in leadAssignments)
            {
                leadVMs.Add(this.GetLeadAssignmentViewModel(item));
            }

            return leadVMs.OrderByDescending(o => o.AssignedOn).ToList();
        }

        protected bool IsActionAllowed(string actionId, string userRoleName)
        {
            if (_actionPermissions == null)
                _actionPermissions = _actionPermissionRepo.Get();

            var actionPermission = _actionPermissions.Where(w => w.ActionId == actionId && w.ApplicationRoleName == userRoleName).SingleOrDefault();

            return actionPermission != null ? true : false;
        }
    }
}