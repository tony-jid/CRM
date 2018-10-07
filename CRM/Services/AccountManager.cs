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
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<IdentityResult> CreateAccountAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user, user.Email);

            return result;
        }

        public async Task SendEmailConfirmationAsync(ApplicationUser user, HttpRequest request, IUrlHelper url)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = url.EmailConfirmationLink(user.Id, code, request.Scheme);
            await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl);
        }

        public async Task SendResetPasswordAsync(ApplicationUser user, HttpRequest request, IUrlHelper url)
        {
            //var user = await _userManager.FindByEmailAsync(user.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                //return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = url.ResetPasswordCallbackLink(user.Id, code, request.Scheme);
            await _emailSender.SendResetPasswordAsync(user.Email, callbackUrl);
        }

        public void AddModelStateErrors(ModelStateDictionary modelState, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
