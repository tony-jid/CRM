using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Enum;
using CRM.Models.ViewModels;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize(Roles = nameof(EnumApplicationRole.Admin)
        + "," + nameof(EnumApplicationRole.Manager)
        + "," + nameof(EnumApplicationRole.Agent))]
    public class ReportsController : BaseController
    {
        private IUnitOfWork _uow;
        private ReportRepository _reportRepo;
        private ActionRepository _actionRepo;
        private LeadAssignmentRepository _leadAssRepo;

        public ReportsController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _reportRepo = unitOfWork.ReportRepository;
            _actionRepo = unitOfWork.ActionRepository;
            _leadAssRepo = unitOfWork.LeadAssignmentRepository;
        }
        
        [HttpGet]
        public IActionResult Invoice()
        {
            //Model = ReportInvoiceOptionsVM
            return View(new ReportInvoiceOptionVM());
        }
        
        [HttpGet]
        public IActionResult InvoiceByLeads([FromQuery(Name = "ids")] Guid[] ids)
        {
            var reportOption = new ReportInvoiceOptionVM() { StringLeadIds = String.Join(";", ids) };
            return View(nameof(ReportsController.Invoice), reportOption);
        }
        
        [HttpGet]
        public IActionResult InvoiceByAssignments([FromQuery(Name = "ids")] int[] ids)
        {
            var reportOption = new ReportInvoiceOptionVM() { StringLeadAssignmentIds = String.Join(";", ids) };
            return View(nameof(ReportsController.Invoice), reportOption);
        }
        
        [HttpGet]
        public object GetInvoices(ReportInvoiceOptionVM reportOption, DataSourceLoadOptions loadOptions)
        {
            List<ReportInvoiceVM> reportInvoices;

            if (reportOption.LeadIds != null)
                reportInvoices = _reportRepo.GetInvoicesByLead(reportOption.LeadIds);
            else if (reportOption.StringLeadAssignmentIds != null)
                reportInvoices = _reportRepo.GetInvoicesByAssignment(reportOption.LeadAssignmentIds);
            else
                return BadRequest();

            return DataSourceLoader.Load(reportInvoices, loadOptions);
        }

        [HttpPut]
        public JsonResult UpdateInvoicedLeadAssignmentStatus([FromBody]List<ReportInvoiceVM> reportInvoiceVMs)
        {
            var userName = User.Identity.Name;

            foreach (var reportInvoice in reportInvoiceVMs)
            {
                var action = _actionRepo.GetAction(reportInvoice.CurrentActionId);
                var nextState = _actionRepo.GetState(action.NextStateId);

                _leadAssRepo.SetState(reportInvoice.LeadAssignmentId, nextState.Id, nextState.Name, userName);
            }

            return _uow.Commit() ? Json(Ok()) : Json(StatusCode(StatusCodes.Status500InternalServerError));
        }
    }
}