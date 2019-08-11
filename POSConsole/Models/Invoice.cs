using System;
using System.Collections.Generic;

namespace POSConsole.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceDetail> DetailsInvoice { get; set; }
    }
}
