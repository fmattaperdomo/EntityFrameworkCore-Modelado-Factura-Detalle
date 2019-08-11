using POSConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POSConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            using (var context = new AppDbContext())
            {
                List<Product> products = context.Products.ToList();

                var invoiceDetail1 = new InvoiceDetail();
                invoiceDetail1.ProductId = products[0].Id;
                invoiceDetail1.Price = products[0].Price;
                invoiceDetail1.Quantity = 5;

                var invoiceDetail2 = new InvoiceDetail();
                invoiceDetail2.ProductId = products[1].Id;
                invoiceDetail2.Price = products[1].Price;
                invoiceDetail2.Quantity = 5;

                List<InvoiceDetail> details = new List<InvoiceDetail>() { invoiceDetail1, invoiceDetail2 };

                var invoice = new Invoice();
                invoice.RegisterDate = DateTime.Now;
                invoice.DetailsInvoice = details;
                invoice.Subtotal = details.Sum(x => x.Price * x.Quantity);
                invoice.Tax = 0;
                invoice.Total = invoice.Subtotal + invoice.Tax;

                context.Add(invoice);
                context.SaveChanges();
            }
            
        }
    }
}
