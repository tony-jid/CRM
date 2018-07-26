using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class TransactionType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ControllerName { get; set; }

        [Required]
        public string ActionName { get; set; }

        public int NextTransactionTypeId { get; set; }
        public TransactionType NextTransactionType { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }

    }
}
