using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [StringLength(256)]
        public string StreetAddress { get; set; }

        [StringLength(64)]
        public string Suburb { get; set; }

        [StringLength(4)]
        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        [StringLength(4)]
        public string PostCode { get; set; }

        public Office Office { get; set; }

        public PartnerBranch PartnerBranch { get; set; }

        public Customer Customer { get; set; }
    }
}
