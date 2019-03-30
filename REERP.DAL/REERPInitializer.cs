using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using REERP.Models;

namespace REERP.DAL
{
    public class REERPInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<REERPContext>
    {
        protected override void Seed(REERPContext context)
        {
            var addresses = new List<Address>
            {
                new Address{AddressId=1,LandLineNo="+251111123456",CellPhoneNo="+251911123456",State="Afar",City="Semera",StreetNo="12345",HouseNo="123"},
                new Address{AddressId=2,LandLineNo="+555555555555",CellPhoneNo="+666666666666",State="Amhara",City="Bahirdar",StreetNo="44444",HouseNo="555"},
            };
            addresses.ForEach(s => context.Addresses.Add(s));
            context.SaveChanges();
            var branches = new List<Branch>
            {
                new Branch { BranchName="Main",BranchLocation="Addis Ababa", BranchDescription="Country Main Branch"},
                new Branch {BranchName="South Branch",BranchLocation="Awassa", BranchDescription="SNNP Branch in Awassa"}
            };
            branches.ForEach(s => context.Branches.Add(s));
            context.SaveChanges();
            var customers = new List<Customer>
            {
                new Customer { FirstName="Tewodros",LastName="Afro", Address="Arat Killo", TelephoneNo="123456789",TIN="123456789",VATNumber="123456789",Trusted=true},
                new Customer { FirstName="Tilahun",LastName="Gessesse", Address="Arat Killo", TelephoneNo="123456789",TIN="123456789",VATNumber="123456789",Trusted=true}
            };
            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();
            var salesInvoices = new List<SalesInvoice>
            {
                new SalesInvoice { SalesInvoiceId=1,CustomerId=1,BranchId=1,SalesType=0,UserId="98f74499-a614-4373-aacd-5eec5b46ef95",DateSold=DateTime.Parse("2016-10-09")},
                new SalesInvoice { SalesInvoiceId=2,CustomerId=2,BranchId=2,SalesType=1,UserId="98f74499-a614-4373-aacd-5eec5b46ef95",DateSold=DateTime.Parse("2016-10-09")}
            };
            salesInvoices.ForEach(s => context.SalesInvoices.Add(s));
            context.SaveChanges();

            var categories = new List<Category>
            {
                new Category { CategoryName="Car",CategoryDescription="Toyota Cars."},
                new Category {CategoryName="Tyres",CategoryDescription="Tyre for diffrent model Toyota Cars."}
            };
            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();

           

            var productcs = new List<Productc>
            {
                new Productc { ProductcId="001", ProductName="Toyota Vitz",ProductDescription="Toyota Vitz 2014", Model="Vitz 2014",UnitOfMeasure="Pcs",UnitCost=200000.00M,UnitPrice=250000.00M,CategoryId=1},
                new Productc {ProductcId="002",ProductName="Toyota Yaris",ProductDescription="Toyota Yaris 2012", Model="Yaris 2012",UnitOfMeasure="Pcs",UnitCost=350000.00M,UnitPrice=400000.00M,CategoryId=1}
            };
            productcs.ForEach(s => context.Productcs.Add(s));
            context.SaveChanges();

            var stocks = new List<Stock>
            {
                new Stock { BranchId=1,ProductcId="001", Quantity=5},
                new Stock {BranchId=1,ProductcId="002", Quantity=3}
            };
            stocks.ForEach(s => context.Stocks.Add(s));
            context.SaveChanges();

            var receiveInvoice = new ProductReceive { UserId = "98f74499-a614-4373-aacd-5eec5b46ef95", BranchId = 1, DateReceived = DateTime.Parse("2017-12-09") };
            var receiveLineItems = new List<ProductReceiveLineItem>
            {
                new ProductReceiveLineItem { ProductId="001",Quantity=1, UnitCost=200000.00M},
                new ProductReceiveLineItem { ProductId="002",Quantity=1, UnitCost=350000.00M}
            };

            receiveInvoice.ProductReceiveLineItems = receiveLineItems;
            context.ProductReceives.Add(receiveInvoice);
            context.SaveChanges();

            var transferInvoice = new ProductTransfer { UserId = "98f74499-a614-4373-aacd-5eec5b46ef95", FromBranchId = 1, ToBranchId = 2, DateTransfered = DateTime.Parse("2017-12-09") };
            var transferLineItems = new List<ProductTransferLineItem>
            {
                new ProductTransferLineItem { ProductId="001",Quantity=1},
                new ProductTransferLineItem { ProductId="002",Quantity=1}
            };

            transferInvoice.ProductTransferLineItems = transferLineItems;
            context.ProductTransfers.Add(transferInvoice);
            context.SaveChanges();


        }
    }
}