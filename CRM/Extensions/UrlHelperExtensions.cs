using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Controllers;
using CRM.Enum;

namespace Microsoft.AspNetCore.Mvc
{
    public static class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ConfirmEmail),
                controller: nameof(EnumController.Account),
                values: new { userId, code },
                protocol: scheme);
        }

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ResetPassword),
                controller: nameof(EnumController.Account),
                values: new { code },
                protocol: scheme);
        }

        public static string LeadHookedCallbackLink(this IUrlHelper urlHelper, Guid leadId, string scheme)
        {
            return urlHelper.Action(
                action: nameof(LeadsController.Assignments),
                controller: nameof(EnumController.Leads),
                values: new { leadId },
                protocol: scheme);
        }

        public static string PartnerLeadAssignedCallbackLink(this IUrlHelper urlHelper, string scheme)
        {
            return urlHelper.Action(
                action: nameof(PartnersController.Portal),
                controller: nameof(EnumController.Partners),
                values: new { },
                protocol: scheme);
        }
    }
}
