using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using REERP.Models;
using REERP.Sales.Services;
using REERP.Models.ViewModels;
using REERP.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using REERP.Product.Services;

namespace REERP.Controllers
{
    public class SalesInvoicesController : Controller
    {
        private readonly ISalesInvoiceService _salesInvoiceService;
        private readonly ICustomerService _customerService;
        private readonly IBranchService _branchService;
        private readonly IProductService _productService;
        private UserManager<MyIdentityUser> userManager;
        private RoleManager<MyIdentityRole> roleManager;

        public SalesInvoicesController(ISalesInvoiceService salesInvoiceService, ICustomerService customerService,
            IBranchService branchService, IProductService productService)
        {
            _salesInvoiceService = salesInvoiceService;
            _customerService = customerService;
            _branchService = branchService;
            _productService = productService;

            MyIdentityDbContext db = new MyIdentityDbContext();

            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);

            RoleStore<MyIdentityRole> roleStore = new RoleStore<MyIdentityRole>(db);
            roleManager = new RoleManager<MyIdentityRole>(roleStore);
        }

        // GET: SalesInvoices
        public ActionResult Index()
        {
            var salesInvoices = _salesInvoiceService.GetAllSalesInvoices();
            var salesInvoiceViewModels = new List<SalesInvoiceViewModel>();
            foreach (var salesInvoice in salesInvoices)
            {
                var salesInvoiceViewModel = new SalesInvoiceViewModel()
                {
                    BranchId = salesInvoice.BranchId,
                    CustomerId = salesInvoice.CustomerId,
                    CustomerName = salesInvoice.Customer.FirstName + " " + salesInvoice.Customer.LastName,
                    BranchName = salesInvoice.Branch.BranchName,
                    DateSold = salesInvoice.DateSold,
                    ReferenceNo = salesInvoice.ReferenceNo,
                    SalesInvoiceId = salesInvoice.SalesInvoiceId,
                    SalesType = salesInvoice.SalesType,
                    UserId = salesInvoice.UserId,
                    UserName = userManager.FindById(salesInvoice.UserId).FullName,
                    Status = salesInvoice.Status
                };
                salesInvoiceViewModels.Add(salesInvoiceViewModel);
            }
            ViewData["SalesList"] = new SelectList(_salesInvoiceService.GetAllSalesInvoices(), "SalesInvoiceId", "ReferenceNo");
            return View(salesInvoiceViewModels);
        }

        // GET: SalesInvoices/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesInvoice salesInvoice = _salesInvoiceService.FindById(id);
            if (salesInvoice == null)
            {
                return HttpNotFound();
            }
            return View(salesInvoice);
        }

        // GET: SalesInvoices/Create
        public ActionResult Create(int? id, int? salesType)
        {
            MyIdentityDbContext db = new MyIdentityDbContext();
            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            UserManager<MyIdentityUser> userManager = new UserManager<MyIdentityUser>(userStore);
            MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);

            if (id!=null && id!=0)
            {
                var salesInvoice = _salesInvoiceService.Get(t => t.SalesInvoiceId == id, null, "SalesLineItems").FirstOrDefault();
                var salesInvoiceViewModel = new SalesInvoiceViewModel()
                {
                    BranchId = salesInvoice.BranchId,
                    CustomerId = salesInvoice.CustomerId,
                    CustomerName = salesInvoice.Customer.FirstName + " " + salesInvoice.Customer.LastName,
                    BranchName = salesInvoice.Branch.BranchName,
                    DateSold = salesInvoice.DateSold,
                    ReferenceNo = salesInvoice.ReferenceNo,
                    SalesInvoiceId = salesInvoice.SalesInvoiceId,
                    SalesType = salesInvoice.SalesType,
                    UserId = salesInvoice.UserId,
                    UserName = userManager.FindById(salesInvoice.UserId).FullName,
                    Status = salesInvoice.Status
                };
                ViewBag.UserName = user.FullName;
                ViewBag.BranchName = _branchService.FindById(user.BranchId).BranchName;
                ViewBag.CustomerList = new SelectList(_customerService.GetAllCustomers(), "CustomerId", "FirstName", salesInvoice.CustomerId);
                ViewBag.BranchList = new SelectList(_branchService.GetAllBranches(), "BranchId", "BranchName", salesInvoice.BranchId);
                ViewBag.SalesInvoiceId = salesInvoice.SalesInvoiceId;
                var salesLineItemviewModels = new List<SalesInvoiceLineItemViewModel>();
                foreach (var salesLineItem in salesInvoice.SalesLineItems)
                {
                    var salesLineItemviewModel = new SalesInvoiceLineItemViewModel()
                    {
                        SalesLineItemId = salesLineItem.SalesLineItemId,
                        ProductId = salesLineItem.ProductId,
                        Productname = _productService.FindBy(s=>s.ProductcId== salesLineItem.ProductId).First().ProductName,
                        Quantity = salesLineItem.Quantity,
                        UnitPrice = salesLineItem.UnitPrice
                    };
                    salesLineItemviewModels.Add(salesLineItemviewModel);
                }
                ViewData["CustomerList"] = new SelectList(_customerService.GetAllCustomers(), "CustomerId", "FirstName", salesInvoice.CustomerId);
                ViewData["ProductList"] = new SelectList(_productService.GetAllProducts(), "ProductcId", "ProductName", salesInvoice.CustomerId);
                
                ViewBag.SalesLineItemViewModels = salesLineItemviewModels;
                ViewBag.UserName = user.FullName;
                ViewBag.BranchName = _branchService.FindById(user.BranchId).BranchName;
                ViewBag.SalesType = salesInvoice.SalesType;
                return View(salesInvoiceViewModel);
            }
            
            ViewBag.UserName = user.FullName;
            ViewBag.BranchName = _branchService.FindById(user.BranchId).BranchName;
            if (salesType == 0)
            {                
                ViewBag.CustomerList = new SelectList(_customerService.Get(t => t.Trusted).ToList(), "CustomerId", "FirstName");                
            }
            else
            {
                ViewBag.CustomerList = new SelectList(_customerService.GetAllCustomers(), "CustomerId", "FirstName");
            }
            ViewBag.SalesType = salesType;
            ViewBag.BranchList = new SelectList(_branchService.GetAllBranches(), "BranchId", "BranchName");
            return View();
        }

        // POST: SalesInvoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,SalesType,ReferenceNo")] SalesInvoiceViewModel salesInvoiceViewModel, int CustomerList, 
            int BranchList, int? SalesTypeList)
        {
            if (ModelState.IsValid)
            {
                var exists = _salesInvoiceService.Get(t => t.ReferenceNo == salesInvoiceViewModel.ReferenceNo).FirstOrDefault();
                if (exists != null)
                    return View(salesInvoiceViewModel);

                MyIdentityDbContext db = new MyIdentityDbContext();
                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
                UserManager<MyIdentityUser> userManager = new UserManager<MyIdentityUser>(userStore);
                MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);

                var salesInvoice = new SalesInvoice() {
                    BranchId = BranchList,
                    CustomerId = CustomerList,
                    ReferenceNo = salesInvoiceViewModel.ReferenceNo,
                    SalesType = SalesTypeList?? 0,
                    UserId = user.Id,
                    DateSold = DateTime.Now,
                    Status = "Draft"
                };
                
                _salesInvoiceService.AddSalesInvoice(salesInvoice);
                
                return RedirectToAction("Create", "SalesInvoices", new { id = salesInvoice.SalesInvoiceId });
            }
            
            return View(salesInvoiceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLineItem([Bind(Include = "SalesInvoiceId,Quantity,ProductId")] SalesInvoiceLineItemViewModel salesInvoiceLineItemViewModel)
        {
            var salesInvoice = _salesInvoiceService.Get(t => t.SalesInvoiceId == salesInvoiceLineItemViewModel.SalesInvoiceId,null, "SalesLineItems").FirstOrDefault();
            int exists = salesInvoice.SalesLineItems.Where(t => t.ProductId == salesInvoiceLineItemViewModel.ProductId).ToList().Count;
            bool available = _salesInvoiceService.IsAvailable(salesInvoiceLineItemViewModel.ProductId, salesInvoice.BranchId, salesInvoiceLineItemViewModel.Quantity);
            if (exists > 0 || !available)
            {
                return RedirectToAction("Create", "SalesInvoices", new { id = salesInvoice.SalesInvoiceId });
            }
            var salesInvoiceLineItem = new SalesLineItem()
            {
                ProductId = salesInvoiceLineItemViewModel.ProductId,
                Quantity = salesInvoiceLineItemViewModel.Quantity,
                SalesInvoiceId = salesInvoiceLineItemViewModel.SalesInvoiceId,
                UnitPrice = _productService.FindBy(s=>s.ProductcId== salesInvoiceLineItemViewModel.ProductId).First().UnitPrice
            };
            salesInvoice.SalesLineItems.Add(salesInvoiceLineItem);
            //_salesInvoiceService.EditSalesInvoice(salesInvoice);
            _salesInvoiceService.AddInvoiceLineItem(salesInvoice, salesInvoiceLineItem);
            return RedirectToAction("Create", "SalesInvoices", new { id = salesInvoice.SalesInvoiceId });
        }

        // GET: SalesInvoices/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesInvoice salesInvoice = _salesInvoiceService.FindById(id);
            if (salesInvoice == null)
            {
                return HttpNotFound();
            }
            return View(salesInvoice);
        }

        // POST: SalesInvoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesInvoiceId,CustomerId,BranchId,SalesType,UserId,DateSold")] SalesInvoice salesInvoice)
        {
            if (ModelState.IsValid)
            {
                _salesInvoiceService.EditSalesInvoice(salesInvoice);
                return RedirectToAction("Index");
            }
            return View(salesInvoice);
        }

        // POST: SalesInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesInvoice salesInvoice = _salesInvoiceService.FindById(id);
            _salesInvoiceService.DeleteSalesInvoice(salesInvoice);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Checkout(int id)
        {
            var salesInvoice = _salesInvoiceService.FindById(id);
            salesInvoice.Status = "Checkedout";
            _salesInvoiceService.EditSalesInvoice(salesInvoice);
            return RedirectToAction("Index", "SalesInvoices");
        }

        [Authorize]
        public ActionResult Paid(int id)
        {
            var salesInvoice = _salesInvoiceService.FindById(id);
            salesInvoice.Status = "Paid";
            _salesInvoiceService.EditSalesInvoice(salesInvoice);
            return RedirectToAction("Index", "SalesInvoices");
        }

        [Authorize]
        public ActionResult Proforma(int id)
        {
            var salesInvoice = _salesInvoiceService.Get(t => t.SalesInvoiceId == id, null, "SalesLineItems").FirstOrDefault();
            var salesInvoiceViewModel = new SalesInvoiceViewModel()
            {
                BranchId = salesInvoice.BranchId,
                CustomerId = salesInvoice.CustomerId,
                CustomerName = salesInvoice.Customer.FirstName + " " + salesInvoice.Customer.LastName,
                BranchName = salesInvoice.Branch.BranchName,
                DateSold = salesInvoice.DateSold,
                ReferenceNo = salesInvoice.ReferenceNo,
                SalesInvoiceId = salesInvoice.SalesInvoiceId,
                SalesType = salesInvoice.SalesType,
                UserId = salesInvoice.UserId,
                UserName = userManager.FindById(salesInvoice.UserId).FullName,
                Status = salesInvoice.Status
            };

            var salesLineItemviewModels = new List<SalesInvoiceLineItemViewModel>();
            foreach (var salesLineItem in salesInvoice.SalesLineItems)
            {
                var salesLineItemviewModel = new SalesInvoiceLineItemViewModel()
                {
                    ProductId = salesLineItem.ProductId,
                    Productname = _productService.FindBy(s=>s.ProductcId==salesLineItem.ProductId).First().ProductName,
                    Quantity = salesLineItem.Quantity,
                    UnitPrice = salesLineItem.UnitPrice,
                    TotalPrice = salesLineItem.Quantity * salesLineItem.UnitPrice
                };
                salesLineItemviewModels.Add(salesLineItemviewModel);
            }
            ViewBag.GrandTotal = salesLineItemviewModels.Sum(t => t.TotalPrice);
            ViewBag.SalesLineItemViewModels = salesLineItemviewModels;

            return View(salesInvoiceViewModel);
        }

        [Authorize]
        public ActionResult SalesAttachment(int id)
        {
            var salesInvoice = _salesInvoiceService.Get(t => t.SalesInvoiceId == id, null, "SalesLineItems").FirstOrDefault();
            var salesInvoiceViewModel = new SalesInvoiceViewModel()
            {
                BranchId = salesInvoice.BranchId,
                CustomerId = salesInvoice.CustomerId,
                CustomerName = salesInvoice.Customer.FirstName + " " + salesInvoice.Customer.LastName,
                BranchName = salesInvoice.Branch.BranchName,
                DateSold = salesInvoice.DateSold,
                ReferenceNo = salesInvoice.ReferenceNo,
                SalesInvoiceId = salesInvoice.SalesInvoiceId,
                SalesType = salesInvoice.SalesType,
                UserId = salesInvoice.UserId,
                UserName = userManager.FindById(salesInvoice.UserId).FullName,
                Status = salesInvoice.Status
            };

            var salesLineItemviewModels = new List<SalesInvoiceLineItemViewModel>();
            foreach (var salesLineItem in salesInvoice.SalesLineItems)
            {
                var salesLineItemviewModel = new SalesInvoiceLineItemViewModel()
                {
                    ProductId = salesLineItem.ProductId,
                    Productname = _productService.FindBy(s=>s.ProductcId==salesLineItem.ProductId).First().ProductName,
                    Quantity = salesLineItem.Quantity,
                    UnitPrice = salesLineItem.UnitPrice,
                    TotalPrice = salesLineItem.Quantity * salesLineItem.UnitPrice
                };
                salesLineItemviewModels.Add(salesLineItemviewModel);
            }
            ViewBag.GrandTotal = salesLineItemviewModels.Sum(t=>t.TotalPrice);
            ViewBag.SalesLineItemViewModels = salesLineItemviewModels;

            return View(salesInvoiceViewModel);
        }

        [Authorize]
        public ActionResult SalesReport(int? branchId, string fromDate, string toDate)
        {
            var branches = _branchService.GetAllBranches();

            if (branchId==null || branchId == 0)
            {
                branchId = branches.FirstOrDefault().BranchId;
            }
            var salesInvoices = new List<SalesInvoice>();
            if (!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate))
            {
                DateTime fromDatec = Convert.ToDateTime(fromDate);
                DateTime toDatec = Convert.ToDateTime(toDate);
                salesInvoices = _salesInvoiceService.Get(t => t.BranchId == branchId && t.DateSold >= fromDatec && t.DateSold <= toDatec && t.Status == "Paid", null, "SalesLineItems").ToList();
            }
            else if (!String.IsNullOrEmpty(fromDate))
            {
                DateTime fromDatec = Convert.ToDateTime(fromDate);
                salesInvoices = _salesInvoiceService.Get(t => t.BranchId == branchId && t.DateSold >= fromDatec && t.Status == "Paid", null, "SalesLineItems").ToList();
            }
            else
            {
                salesInvoices = _salesInvoiceService.Get(t => t.BranchId == branchId && t.Status == "Paid", null, "SalesLineItems").ToList();
            }
            ViewBag.BranchList = new SelectList(branches, "BranchId", "BranchName", branchId);
            
            var salesLineItems = new List<SalesLineItem>();
            foreach(var salesInvoice in salesInvoices)
            {
                foreach(var sLI in salesInvoice.SalesLineItems)
                {
                    salesLineItems.Add(sLI);
                }                    
            }
            var groupedSLIs = (from tr in salesLineItems
                               group tr by new { tr.ProductId }
                              into groupedSLI
                               select
                              new 
                              {
                                  ProductID = groupedSLI.Key.ProductId,
                                  ProductName = _productService.FindBy(s=>s.ProductcId==groupedSLI.Key.ProductId).First().ProductName,
                                  UnitPrice = _productService.FindBy(s=>s.ProductcId == groupedSLI.Key.ProductId).First().UnitPrice,
                                  QuantitySold = groupedSLI.Sum(p => p.Quantity)
                              });
            List<SalesReportViewModel> salesReportViewModels = (from groupedSLI in groupedSLIs let i = groupedSLI where i != null where i != null select new SalesReportViewModel() { ProductId = i.ProductID, ProductName = i.ProductName, QuantitySold = i.QuantitySold, UnitPrice = i.UnitPrice}).ToList();
            ViewBag.GroupedSLIs = salesReportViewModels;



            return View();
        }

        [Authorize]
        public ActionResult Return(int SalesInvoiceId)
        {
            var salesInvoice = _salesInvoiceService.FindById(SalesInvoiceId);
            salesInvoice.Status = "Returned";
            _salesInvoiceService.EditSalesInvoice(salesInvoice);
            return RedirectToAction("Index", "SalesInvoices");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var salesInvoice = _salesInvoiceService.FindById(id);
            _salesInvoiceService.DeleteSalesInvoice(salesInvoice);
            return RedirectToAction("Index", "SalesInvoices");
        }

        [Authorize]
        public ActionResult DeleteLineItem(int id)
        {
            //_salesInvoiceService.DeleteLineItemById(id);
            //return RedirectToAction("Index", "SalesInvoices");
            var lineItem = _salesInvoiceService.FindLineItemById(id);
            var salesInvoice = _salesInvoiceService.FindById(lineItem.SalesInvoiceId);
            //productReceive.ProductReceiveLineItems.Remove(lineItem);
            _salesInvoiceService.DeleteInvoiceLineItem(salesInvoice, lineItem);
            return RedirectToAction("Create", "SalesInvoices", new { id = salesInvoice.SalesInvoiceId });

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _salesInvoiceService.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
