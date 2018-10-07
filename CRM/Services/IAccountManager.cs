using CRM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services
{
    public interface IAccountManager
    {
        Task<IdentityResult> CreateAccountAsync(ApplicationUser user);

        Task SendEmailConfirmationAsync(ApplicationUser user, HttpRequest request, IUrlHelper url);

        Task SendResetPasswordAsync(ApplicationUser user, HttpRequest request, IUrlHelper url);

        void AddModelStateErrors(ModelStateDictionary modelState, IdentityResult result);
    }
}
