using CRM.Data;
using CRM.Enum;
using CRM.Helpers;
using CRM.Models;
using CRM.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class ReportRepository
    {
        private ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ReportInvoiceVM> GetInvoicesByLead(Guid leadId)
        {
            return this.GetInvoicesByLead(new List<Guid>() { leadId });
        }

        public List<ReportInvoiceVM> GetInvoicesByLead(List<Guid> leadIds)
        {
            List<ReportInvoiceVM> reportInvoiceVMs = new List<ReportInvoiceVM>();

            var genInvoiceActions = new string[] { nameof(EnumStateAction.ALA4), nameof(EnumStateAction.ALA5) };

            var leadsWithAllStates = _context.Leads
                .Where(w => leadIds.Contains(w.Id))
                .Include(i => i.LeadType)
                .Include(i => i.Customer).ThenInclude(i => i.Address)
                .Include(i => i.LeadStates)
                .Include(i => i.LeadAssignments).ThenInclude(i => i.PartnerBranch).ThenInclude(i => i.Address)
                .Include(i => i.LeadAssignments).ThenInclude(i => i.PartnerBranch).ThenInclude(i => i.Partner)
                .Include(i => i.LeadAssignments).ThenInclude(i => i.LeadAssignmentStates).ThenInclude(i => i.State).ThenInclude(i => i.StateActions).ThenInclude(i => i.Action);

            foreach (var lead in leadsWithAllStates)
            {
                var submittedState = lead.LeadStates.OrderBy(o => o.ActionTimestamp).FirstOrDefault();

                List<LeadAssignment> invoicableAssignments = new List<LeadAssignment>();
                LeadAssignmentState assignedState = null, acceptedState = null, assignmentCurrentState = null;
                StateAction invoiceAction = null;

                foreach (var assignment in lead.LeadAssignments)
                {
                    assignedState = assignment.LeadAssignmentStates
                        .OrderBy(o => o.ActionTimestamp).FirstOrDefault();

                    acceptedState = assignment.LeadAssignmentStates
                        .Where(w => w.StateId == nameof(EnumState.SLA2))
                        .OrderByDescending(o => o.ActionTimestamp).FirstOrDefault();

                    assignmentCurrentState = assignment.LeadAssignmentStates.Where(w => w.StateId != nameof(EnumState.S0))
                        .OrderByDescending(o => o.ActionTimestamp).FirstOrDefault();

                    invoiceAction = assignmentCurrentState.State.StateActions.Where(w => genInvoiceActions.Contains(w.ActionId)).FirstOrDefault();

                    if (invoiceAction != null)
                    {
                        //assignment.LeadAssignmentStates = new List<LeadAssignmentState>() { assignmentCurrentState };
                        //invoicableAssignments.Add(assignment);
                        var reportInvoiceVM = this.GetReportInvoiceVM(lead, assignment, submittedState, assignedState, acceptedState, assignmentCurrentState, invoiceAction);
                        reportInvoiceVMs.Add(reportInvoiceVM);
                    }
                }

                //lead.LeadAssignments = invoicableAssignments;
            }

            return reportInvoiceVMs;
        }

        public ReportInvoiceVM GetReportInvoiceVM(Lead lead, LeadAssignment assignment
            , LeadState submittedState, LeadAssignmentState assignedState, LeadAssignmentState acceptedState
            , LeadAssignmentState currentState, StateAction currentAction)
        {
                return new ReportInvoiceVM()
                {
                    LeadId = lead.Id,
                    LeadDetails = lead.Details,
                    SubmittedDateTime = submittedState.ActionTimestamp,
                    SubmittedDateTimeString = submittedState.ActionTimestamp.ToShortDateString() + " " + submittedState.ActionTimestamp.ToShortTimeString(),
                    LeadTypeId = lead.LeadType.Id,
                    LeadTypeName = lead.LeadType.Name,
                    LeadTypeImage = ImageHelper.PATH_CLIENT_LEAD_TYPE + lead.LeadType.Image,
                    LeadTypePrice = lead.LeadType.Price,
                    CustomerId = lead.Customer.Id,
                    CustomerUnique = lead.Customer.ContactName + " (" + lead.Customer.EMail + ")",
                    CustomerName = lead.Customer.ContactName,
                    CustomerContactNumber = lead.Customer.ContactNumber,
                    CustomerEmail = lead.Customer.EMail,
                    CustomerBusinessName = lead.Customer.BusinessName,
                    CustomerState = lead.Customer.Address.State,
                    CustomerAddress = AddressHelper.MergeAddress(lead.Customer.Address),
                    PartnerId = assignment.PartnerBranch.Partner.Id,
                    PartnerName = assignment.PartnerBranch.Partner.Name,
                    PartnerLogo = ImageHelper.PATH_CLIENT_PARTNER + assignment.PartnerBranch.Partner.Logo,
                    PartnerAddress = AddressHelper.MergeAddress(assignment.PartnerBranch.Address),
                    LeadAssignmentId = assignment.Id,
                    AssignedDateTime = assignedState.ActionTimestamp,
                    AssignedDateTimeString = assignedState.ActionTimestamp.ToShortDateString() + " " + assignedState.ActionTimestamp.ToShortTimeString(),
                    AcceptedDateTime = acceptedState.ActionTimestamp,
                    AcceptedDateTimeString = acceptedState.ActionTimestamp.ToShortDateString() + " " + acceptedState.ActionTimestamp.ToShortTimeString(),
                    CurrentStateId = currentState.State.Id,
                    CurrentStateName = currentState.State.Name,
                    CurrentStateTag = StatusHelper.GetHtmlBadge(currentState.State.Id, currentState.State.Name),
                    CurrentActionId = currentAction.Action.Id,
                    CurrentActionName = currentAction.Action.ActionName
                };
        }
    }
}
