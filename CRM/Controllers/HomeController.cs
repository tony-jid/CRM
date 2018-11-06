using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using CRM.Enum;
using CRM.Repositories;
using CRM.Models.ViewModels;

namespace CRM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private LeadRepository _leadRepo;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _leadRepo = unitOfWork.LeadRepository;
        }

        public IActionResult Index()
        {
            if (User.IsInRole(nameof(EnumApplicationRole.Partner)))
                return RedirectToAction(nameof(PartnersController.Portal), nameof(EnumController.Partners));
            else
                return View("Dashboard", this.GetDashboardVM());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpGet]
        public JsonResult GetChartLeadOverview([FromQuery] string dateStart, [FromQuery] string dateEnd)
        {
            var leadOverview = this.GetDashboardLeadOverview(DateTime.Parse(dateStart), DateTime.Parse(dateEnd));
            //var leadOverview = this.GetDashboardLeadOverview(dateStart, dateEnd);
            return Json(leadOverview);
        }

        public DashboardVM GetDashboardVM()
        {
            return this.GetDashboardVM(DateTime.MinValue, DateTime.MaxValue);
        }

        public DashboardVM GetDashboardVM(DateTime dateStart, DateTime dateEnd)
        {
            var dashLeadVMs = _leadRepo.GetDashboardLeadVMs(dateStart, dateEnd);

            DashboardVM dashboardVM = new DashboardVM();
            //dashboardVM.TodayLeadsAmount = _leadRepo.GetTodayLeads().Count();
            dashboardVM.TodayLeadsAmount = dashLeadVMs.Where(w => w.CreatedOn.Date == DateTime.Now.Date).Count();
            dashboardVM.TodayAcceptedLeadsAmount = dashLeadVMs.Where(w => w.LeadStateType == nameof(EnumState.SLA2) && w.ActionTimestamp.Date == DateTime.Now.Date).Count();
            dashboardVM.TodayRejectedLeadsAmount = dashLeadVMs.Where(w => w.LeadStateType == nameof(EnumState.SLA3) && w.ActionTimestamp.Date == DateTime.Now.Date).Count();

            dashboardVM.DashboardLeadOverviewVMs = this.GetDashboardLeadOverview(dateStart, dateEnd);

            return dashboardVM;
        }

        public List<DashboardLeadOverviewVM> GetDashboardLeadOverview(DateTime dateStart, DateTime dateEnd)
        {
            var dashLeadVMs = _leadRepo.GetDashboardLeadVMs(dateStart, dateEnd);
            int diffDays = dateEnd.Subtract(dateStart).Days;

            // Days amount > 30 then grouped by months
            // Days amount > 6 then grouped by weeks
            // other wise grouped by days
            //

            int dateTypeRangeType;
            List<DashboardLeadOverviewVM> dashboardLeadOverviewVMs;

            if (diffDays > 30)
            {
                dateTypeRangeType = (int)EnumDateRangeType.Year;
                dashboardLeadOverviewVMs = dashLeadVMs.GroupBy(g => new { g.MonthCreated, g.LeadStateType })
                    .Select(s => new DashboardLeadOverviewVM()
                    {
                        ArgumentField = s.Key.MonthCreated
                        , ValueFieldName = s.Key.LeadStateType
                        , ValueField = s.Count()
                    }).ToList();
            }
            else if (diffDays > 6)
            {
                dateTypeRangeType = (int)EnumDateRangeType.Month;
                dashboardLeadOverviewVMs = dashLeadVMs.GroupBy(g => new { g.WeekCreated, g.LeadStateType })
                    .Select(s => new DashboardLeadOverviewVM()
                    {
                        ArgumentField = s.Key.WeekCreated
                        , ValueFieldName = s.Key.LeadStateType
                        , ValueField = s.Count()
                    }).ToList();
            }
            else
            {
                dateTypeRangeType = (int)EnumDateRangeType.Day;
                dashboardLeadOverviewVMs = dashLeadVMs.GroupBy(g => new { g.DayCreated, g.LeadStateType })
                    .Select(s => new DashboardLeadOverviewVM()
                    {
                        ArgumentField = s.Key.DayCreated
                        , ValueFieldName = s.Key.LeadStateType
                        , ValueField = s.Count()
                    }).ToList();
            }

            return dashboardLeadOverviewVMs;
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetDashboardTest()
        {
            var dateStart = new DateTime(2018, 10, 15);
            var dateEnd = new DateTime(2018, 10, 21);
            var result = this.GetDashboardVM(dateStart, dateEnd);

            int days = dateEnd.Subtract(dateStart).Days;

            return Json(result);
        }
    }
}
