using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.AuthOptions
{
    public class EmailSenderAuthOptions
    {
        public string ApiName { get; set; }
        public string ApiKey { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
