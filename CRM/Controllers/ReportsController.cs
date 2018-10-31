using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Enum;
using CRM.Models.ViewModels;
using CRM.Repositories;
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
            //Model = ReportInvoiceVM
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetInvoices()
        {
            var result = _reportRepo.GetInvoicesByLead(new Guid("8E1455FF-9DD2-41F1-3F47-08D6362CC4FA"));

            return Json(result);
        }
    }
}