using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class PartnerService
    {
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }

        public int LeadTypeId { get; set; }
        public LeadType LeadType { get; set; }
    }
}
