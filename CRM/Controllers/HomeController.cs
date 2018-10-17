using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using CRM.Enum;

namespace CRM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole(nameof(EnumApplicationRole.Partner)))
                return RedirectToAction(nameof(PartnersController.Portal), nameof(EnumController.Partners));
            else
                return View("Dashboard");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
