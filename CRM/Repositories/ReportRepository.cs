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
        private string[] INVOICABLE_ACTIONS = new string[] { nameof(EnumStateAction.ALA4), nameof(EnumStateAction.ALA5) };

        private ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ReportInvoiceVM> GetInvoicesByLead(Guid leadId)
        {
            return this.GetInvoicesByLead(new List<Guid>() { leadId });
        }

        public List<ReportInvoiceVM> GetInvoicesByLead(List<Guid> leadIds, List<int> leadAssignmentIds = null)
        {
            var leadsWithAllStates = _context.Leads
                .Where(w => leadIds.Contains(w.Id))
                .Include(i => i.LeadType)
                .Include(i => i.Customer).ThenInclude(i => i.Address)
                .Include(i => i.LeadStates)
                .Include(i => i.LeadAssignments).ThenInclude(i => i.PartnerBranch).ThenInclude(i => i.Address)
                .Include(i => i.LeadAssignments).ThenInclude(i => i.PartnerBranch).ThenInclude(i => i.Partner)
                .Include(i => i.LeadAssignments).ThenInclude(i => i.LeadAssignmentStates).ThenInclude(i => i.State).ThenInclude(i => i.StateActions).ThenInclude(i => i.Action);

            // SelectMany combines sequences returned from SelectMany into one
            //var assignmentIds = leadsWithAllStates.SelectMany(s => s.LeadAssignments.Select(s1 => s1.Id)).ToList();
            //this.GetInvoicesByAssignment(assignmentIds);

            return this.GetReportInvoiceVMs(leadsWithAllStates, leadAssignmentIds);
        }

        public List<ReportInvoiceVM> GetInvoicesByAssignment(int leadAssignmentId)
        {
            return this.GetInvoicesByAssignment(new List<int>() { leadAssignmentId });
        }

        public List<ReportInvoiceVM> GetInvoicesByAssignment(List<int> leadAssignmentIds)
        {
            var assignmentsWithAllStates = _context.LeadAssignments
                .Where(w => leadAssignmentIds.Contains(w.Id));

            var leadIds = assignmentsWithAllStates.Select(s => s.LeadId).Distinct().ToList();
            return this.GetInvoicesByLead(leadIds, leadAssignmentIds);
        }

        private List<ReportInvoiceVM> GetReportInvoiceVMs(IEnumerable<Lead> leads, List<int> leadAssignmentIds = null)
        {
            List<ReportInvoiceVM> reportInvoiceVMs = new List<ReportInvoiceVM>();

            if (leads != null)
            {
                foreach (var lead in leads)
                {
                    var submittedState = lead.LeadStates.OrderBy(o => o.ActionTimestamp).FirstOrDefault();

                    List<LeadAssignment> invoicableAssignments = new List<LeadAssignment>();
                    LeadAssignmentState assignedState = null, acceptedState = null, assignmentCurrentState = null;
                    StateAction invoiceAction = null;

                    var leadAssignments = leadAssignmentIds != null ? lead.LeadAssignments.Where(w => leadAssignmentIds.Contains(w.Id)) : lead.LeadAssignments;
                    foreach (var assignment in leadAssignments)
                    {
                        assignedState = assignment.LeadAssignmentStates
                            .OrderBy(o => o.ActionTimestamp).FirstOrDefault();

                        acceptedState = assignment.LeadAssignmentStates
                            .Where(w => w.StateId == nameof(EnumState.SLA2))
                            .OrderByDescending(o => o.ActionTimestamp).FirstOrDefault();

                        assignmentCurrentState = assignment.LeadAssignmentStates.Where(w => w.StateId != nameof(EnumState.S0))
                            .OrderByDescending(o => o.ActionTimestamp).FirstOrDefault();

                        invoiceAction = assignmentCurrentState.State.StateActions.Where(w => INVOICABLE_ACTIONS.Contains(w.ActionId)).FirstOrDefault();

                        if (invoiceAction != null)
                        {
                            var reportInvoiceVM = this.GetReportInvoiceVM(lead, assignment, submittedState, assignedState, acceptedState, assignmentCurrentState, invoiceAction);
                            reportInvoiceVMs.Add(reportInvoiceVM);
                        }
                    }
                }
            }

            return reportInvoiceVMs;
        }

        private ReportInvoiceVM GetReportInvoiceVM(Lead lead, LeadAssignment assignment
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
