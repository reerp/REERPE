using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using REERP.Models;

namespace REERP.DAL
{
    public class REERPContext : DbContext
    {
        public REERPContext() : base("REERPContext")
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<SalesLineItem> SalesLineItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Productc> Productcs { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductReceive> ProductReceives { get; set; }
        public DbSet<ProductReceiveLineItem> ProductReceiveLineItems { get; set; }
        public DbSet<ProductTransfer> ProductTransfers { get; set; }
        public DbSet<ProductTransferLineItem> ProductTransferLineItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}