using CRM.Enum;
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
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountManager(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<IdentityResult> CreateAccountAsync(ApplicationUser user, EnumApplicationRole role)
        {
            var result = await _userManager.CreateAsync(user, user.Email);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, role.ToString());

            return result;
        }

        public async Task<IdentityResult> DeleteAccountAsync(string email)
        {
            IdentityResult result;
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                // User roles will be automaticcally removed when the ApplicationUser is removed
                /*var roleNames = await _userManager.GetRolesAsync(user);
                foreach (var roleName in roleNames)
                {
                    result = await _userManager.RemoveFromRoleAsync(user, roleName);

                    if (!result.Succeeded)
                        return result;
                        //throw new Exception($"Failed removing {roleName} of user [{email}].");
                }*/

                result = await _userManager.DeleteAsync(user);

                //if (!result.Succeeded)
                //    throw new Exception($"Failed deleting user [{email}].");
                return result;
            }
            else
                throw new Exception($"Not found user [{email}].");
        }

        public async Task<IdentityResult> DeleteAccountAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result;
        }

        public async Task<IdentityResult> ChangeEmailAsync(string oldEmail, string newEmail, HttpRequest httpRequest, IUrlHelper url)
        {
            var user = await _userManager.FindByEmailAsync(oldEmail);

            var result = await ChangeEmailAsync(user, newEmail);
            
            if (result.Succeeded)
            {
                result = await ChangeUserNameAsync(user, newEmail);

                if (result.Succeeded)
                {
                    await SendEmailConfirmationAsync(user, httpRequest, url);
                }
                else
                {
                    // If changing UserName fails, then rollback the Email
                    await ChangeEmailAsync(user, oldEmail);
                }
            }

            return result;
        }

        public async Task<IdentityResult> ChangeEmailAsync(ApplicationUser user, string newEmail)
        {
            string token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
            
            return result;
        }

        public async Task<IdentityResult> ChangeUserNameAsync(ApplicationUser user, string newEmail)
        {
            var result = await _userManager.SetUserNameAsync(user, newEmail);

            return result;
        }

        public async Task<ApplicationUser> GetUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
                return user;
            else
                throw new Exception($"Not found application user of {email}.");
        }

        public async Task SendEmailConfirmationAsync(ApplicationUser user, HttpRequest request, IUrlHelper url)
        {
            user.EmailConfirmed = false;

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
