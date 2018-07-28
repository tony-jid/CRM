using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Invoice : IDataTimestamp
    {
        [Key]
        public int No { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDateTime { get; set; }

        public string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double SubTotal { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double GST { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Total { get; set; }

        public IEnumerable<InvoiceItem> InvoiceItems { get; set; }
    }
}
