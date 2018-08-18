using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    //[Authorize]
    [Route("[controller]/[action]")]
    public abstract class BaseController : Controller
    {
        [Route("/[controller]")]
        [Route("/[controller]/[action]")]
        public virtual IActionResult Index()
        {
            this.SetViewDataModalTarget();
            return View();
        }

        protected void SetViewDataModalTarget()
        {
            //ViewData[nameof(EnumViewData.ModalTarget)] = @"$('#main-content')";
        }

        protected string GetFullErrorMessage(ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}
