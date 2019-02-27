using System;
using System.Text;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using REERP.Sales.Services;
using REERP.Controllers;
using REERP.Models;
using REERP.Product.Services;

namespace REERP.Tests.ControllersTests
{
    [TestFixture]
    public class SalesInvoicesControllerTest
    {

        private ISalesInvoiceService MockSalesInvoiceService;
        private ICustomerService MockCustomerService;
        private IBranchService MockBranchService;
        private IProductService MockProductService;
        private SalesInvoicesController _salesInvoicesController;

        [SetUp]
        public void SetUp()
        {
            List<SalesInvoice> salesInvoiceTest = new List<SalesInvoice>();
            {
                new SalesInvoice { SalesInvoiceId = 1, CustomerId = 1, BranchId = 1, SalesType = 0, UserId = "98f74499-a614-4373-aacd-5eec5b46ef95", DateSold = DateTime.Parse("2016-10-09") };
                new SalesInvoice { SalesInvoiceId = 2, CustomerId = 2, BranchId = 2, SalesType = 1, UserId = "98f74499-a614-4373-aacd-5eec5b46ef95", DateSold = DateTime.Parse("2016-10-09") };
            }

            List<Customer> customerTest = new List<Customer>();
            {
                new Customer { FirstName = "Tewodros", LastName = "Afro", Address = "Arat Killo", TelephoneNo = "123456789", TIN = "123456789", VATNumber = "123456789", Trusted = true };
                new Customer { FirstName = "Tilahun", LastName = "Gessesse", Address = "Arat Killo", TelephoneNo = "123456789", TIN = "123456789", VATNumber = "123456789", Trusted = true };
            }

            List<Branch> branchTest = new List<Branch>();
            {
                new Branch { BranchName = "Main", BranchLocation = "Addis Ababa", BranchDescription = "Country Main Branch" };
                new Branch { BranchName = "South Branch", BranchLocation = "Awassa", BranchDescription = "SNNP Branch in Awassa" };
            }

            List<Productc> productcTest = new List<Productc>();
            {
                new Productc { ProductName = "Toyota Vitz", ProductDescription = "Toyota Vitz 2014", Model = "Vitz 2014", UnitOfMeasure = "Pcs", UnitCost = 200000.00M, UnitPrice = 250000.00M, CategoryId = 1 };
                new Productc { ProductName = "Toyota Yaris", ProductDescription = "Toyota Yaris 2012", Model = "Yaris 2012", UnitOfMeasure = "Pcs", UnitCost = 350000.00M, UnitPrice = 400000.00M, CategoryId = 1 };
            }

            Mock<ISalesInvoiceService> mockSalesInvoiceService = new Mock<ISalesInvoiceService>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IBranchService> mockBranchService = new Mock<IBranchService>();
            Mock<IProductService> mockProductService = new Mock<IProductService>();

            mockSalesInvoiceService.Setup(m => m.GetAllSalesInvoices()).Returns(salesInvoiceTest);
            mockCustomerService.Setup(m => m.GetAllCustomers()).Returns(customerTest);
            mockBranchService.Setup(m => m.GetAllBranches()).Returns(branchTest);
            mockProductService.Setup(m => m.GetAllProducts()).Returns(productcTest);

            MockSalesInvoiceService = mockSalesInvoiceService.Object;
            MockCustomerService = mockCustomerService.Object;
            MockBranchService = mockBranchService.Object;
            MockProductService = mockProductService.Object;

            _salesInvoicesController = new SalesInvoicesController(MockSalesInvoiceService, MockCustomerService, MockBranchService, MockProductService);
        }

        [Test]
        public void Can_fetch_all_Sales_Invoice_Lists()
        {
            List<SalesInvoice> expected = new List<SalesInvoice>();
            {
                new SalesInvoice { SalesInvoiceId = 1, CustomerId = 1, BranchId = 1, SalesType = 0, UserId = "98f74499-a614-4373-aacd-5eec5b46ef95", DateSold = DateTime.Parse("2016-10-09") };
                new SalesInvoice { SalesInvoiceId = 2, CustomerId = 2, BranchId = 2, SalesType = 1, UserId = "98f74499-a614-4373-aacd-5eec5b46ef95", DateSold = DateTime.Parse("2016-10-09") };
            }

            List<SalesInvoice> actual = MockSalesInvoiceService.GetAllSalesInvoices();
            Assert.AreEqual(actual.Count, expected.Count);
        }

    }
}
