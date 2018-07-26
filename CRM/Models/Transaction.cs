using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Message { get; set; }

        public Guid LeadId { get; set; }
        public Lead Lead { get; set; }

        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }

    }
}
