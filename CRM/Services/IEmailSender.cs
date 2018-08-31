﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string[] emails, string subject, string message);

        Task SendEmailAsync(string email, string subject, string message);
    }
}
