using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CRM.Services;

namespace CRM.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>.");
        }

        public static Task SendResetPasswordAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Reset Password",
                $"Please reset your password by clicking this <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>.");
        }

        public static Task SendCompanyLeadHookedAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "New lead submission link",
                $"New lead has been submitted. Please click <a href='{HtmlEncoder.Default.Encode(link)}'>here for details</a>.");
        }

        public static Task SendCompanyPartnerResponseAsync(this IEmailSender emailSender, string email, string link, string partnerName, string partnerReponseText)
        {
            return emailSender.SendEmailAsync(email, $"Lead {partnerReponseText} by {partnerName}",
                $"The lead is {partnerReponseText} by {partnerName}. Please click <a href='{HtmlEncoder.Default.Encode(link)}'>here for details</a>.");
        }

        public static Task SendPartnerLeadAssignedAsync(this IEmailSender emailSender, string[] email, string link)
        {
            return emailSender.SendEmailAsync(email, "Lead assignment notification",
                $"Lead has been assigned to you. Please click <a href='{HtmlEncoder.Default.Encode(link)}'>here for details</a>.");
        }
    }
}
