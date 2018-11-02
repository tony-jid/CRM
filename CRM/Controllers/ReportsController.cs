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
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize(Roles = nameof(EnumApplicationRole.Admin)
        + "," + nameof(EnumApplicationRole.Manager)
        + "," + nameof(EnumApplicationRole.Agent))]
    public class ReportsController : BaseController
    {
        private ReportRepository _reportRepo;

        public ReportsController(IUnitOfWork unitOfWork)
        {
            _reportRepo = unitOfWork.ReportRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Invoice()
        {
            //Model = ReportInvoiceOptionsVM
            return View(new ReportInvoiceOptionVM());
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult InvoiceByLeads([FromQuery(Name = "ids")] Guid[] ids)
        {
            var reportOption = new ReportInvoiceOptionVM() { StringLeadIds = String.Join(";", ids) };
            return View(nameof(ReportsController.Invoice), reportOption);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult InvoiceByAssignments([FromQuery(Name = "ids")] int[] ids)
        {
            var reportOption = new ReportInvoiceOptionVM() { StringLeadAssignmentIds = String.Join(";", ids) };
            return View(nameof(ReportsController.Invoice), reportOption);
        }

        [AllowAnonymous]
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
    }
}