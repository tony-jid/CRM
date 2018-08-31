using CRM.Services.AuthOptions;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSenderSendGrid : IEmailSender
    {
        private EmailSenderAuthOptions _options;

        public EmailSenderSendGrid(IOptions<EmailSenderAuthOptions> options)
        {
            _options = options.Value;
        }

        public Task SendEmailAsync(string[] emails, string subject, string message)
        {
            return this.Send(emails, subject, message);
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return this.Send(new string[] { email }, subject, message);
        }

        private Task Send(string[] emails, string subject, string message)
        {
            var client = new SendGridClient(_options.ApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("thawatchai.j14@gmail.com", "Developer Tony"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            foreach (var email in emails)
            {
                msg.AddTo(new EmailAddress(email));
            }

            return client.SendEmailAsync(msg);
        }
    }
}
