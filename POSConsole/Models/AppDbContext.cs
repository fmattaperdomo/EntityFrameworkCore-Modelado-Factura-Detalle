using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace POSConsole.Models
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var products = new List<Product>()
            {
                new Product(){Id=1, Name = "Lámpara", Description = "Para iluminar tu vida", Price = 80000},
                new Product(){Id=2, Name = "Tableta Inteligente", Description = "Para que sus hijos conozca de tecnologia", Price = 500000}
            };

            modelBuilder.Entity<Product>().HasData(products);

            modelBuilder.Entity<InvoiceDetail>().Property(x => x.Total).HasComputedColumnSql("[Quantity] * [Price]");

            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(11,2)");
            modelBuilder.Entity<InvoiceDetail>().Property(x => x.Price).HasColumnType("decimal(11,2)");
            modelBuilder.Entity<InvoiceDetail>().Property(x => x.Total).HasColumnType("decimal(12,2)");
            modelBuilder.Entity<Invoice>().Property(x => x.Subtotal).HasColumnType("decimal(11,2)");
            modelBuilder.Entity<Invoice>().Property(x => x.Tax).HasColumnType("decimal(4,2)");
            modelBuilder.Entity<Invoice>().Property(x => x.Total).HasColumnType("decimal(12,2)");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=POSInvoice;Integrated Security=True")
                // Esta opción solo debe ser usada en tiempo de desarrollo
                .EnableSensitiveDataLogging(true)
                .UseLoggerFactory(GetLoggerFactory());
        }

        // El uso del Logger Factory es nuevo!
        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
