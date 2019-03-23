using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using REERP.Models;
using REERP.Models.ViewModels;
using REERP.Product.Services;
using REERP.Security;
using REERP.Store.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;




namespace REERP.Controllers
{
    public class ProductReceivesController : Controller
    {
        private readonly IProductReceiveService _productReceiveService;
        private readonly IBranchService _branchService;
        private readonly IProductService _productService;
        private readonly UserManager<MyIdentityUser> userManager;
        private readonly RoleManager<MyIdentityRole> roleManager;

        public ProductReceivesController(IProductReceiveService productReceiveService,
            IBranchService branchService, IProductService productService)
        {
            _productReceiveService = productReceiveService;
            _branchService = branchService;
            _productService = productService;

            MyIdentityDbContext db = new MyIdentityDbContext();

            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);

            RoleStore<MyIdentityRole> roleStore = new RoleStore<MyIdentityRole>(db);
            roleManager = new RoleManager<MyIdentityRole>(roleStore);


        }
        // GET: ProductReceives
        public ActionResult Index()
        {
            var productReceives = _productReceiveService.GetAllProductReceive();
            var productReceivesViewModels = new List<ProductReceiveViewModel>();
            foreach (var productReceive in productReceives)
            {
                var productReceiveViewModel = new ProductReceiveViewModel()
                {
                    BranchId = productReceive.BranchId,
                    BranchName = productReceive.Branch.BranchName,
                    DateReceived = productReceive.DateReceived,
                    ProductReceiveId = productReceive.ProductReceiveId,

                    UserId = productReceive.UserId,

                    // var ProductReceiveLineItems = productReceive.ProductReceiveLineItems;

                    UserName = userManager.FindById(productReceive.UserId).FullName
                };
                productReceivesViewModels.Add(productReceiveViewModel);

            }
            return View(productReceivesViewModels);
        }



        // GET: ProductReceives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductReceive productReceive = _productReceiveService.FindById(id.Value);
            if (productReceive == null)
            {
                return HttpNotFound();
            }
            return View(productReceive);
        }

        // GET: ProductReceives/Create
        public ActionResult Create(int? id)
        {
            MyIdentityDbContext db = new MyIdentityDbContext();
            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            UserManager<MyIdentityUser> userManager = new UserManager<MyIdentityUser>(userStore);
            MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);

            if (id != null && id != 0)
            {
                var productReceive = _productReceiveService.Get(t => t.ProductReceiveId == id, null, "ProductReceiveLineItems").FirstOrDefault();
                var productReceiveViewModel = new ProductReceiveViewModel()
                {
                    BranchId = productReceive.BranchId,
                    BranchName = productReceive.Branch.BranchName,
                    DateReceived = productReceive.DateReceived,
                    ProductReceiveId = productReceive.ProductReceiveId,
                    UserId = productReceive.UserId,
                    UserName = userManager.FindById(productReceive.UserId).FullName
                };
                ViewBag.UserName = user.FullName;
                ViewBag.BranchName = _branchService.FindById(user.BranchId).BranchName;
                ViewBag.ProductReceiveId = productReceive.ProductReceiveId;
                var productReceiveLineItemViewModels = new List<ProductReceiveLineItemViewModel>();
                foreach (var productReceiveLineItem in productReceive.ProductReceiveLineItems)
                {
                    var productReceiveLineItemviewModel = new ProductReceiveLineItemViewModel()
                    {
                        ProductReceiveLineItemId = productReceiveLineItem.ProductReceiveLineItemId,
                        ProductId = productReceiveLineItem.ProductId,
                        Productname = _productService.FindBy(s => s.ProductcId == productReceiveLineItem.ProductId).First().ProductName,
                        Quantity = productReceiveLineItem.Quantity,
                        UnitCost = productReceiveLineItem.UnitCost
                    };
                    productReceiveLineItemViewModels.Add(productReceiveLineItemviewModel);
                }
                ViewData["ProductList"] = new SelectList(_productService.GetAllProducts(), "ProductcId", "ProductName");
                ViewData["BranchList"] = new SelectList(_branchService.GetAllBranches(), "BranchId", "BranchName");
                ViewBag.ProductReceiveLineItemViewModels = productReceiveLineItemViewModels;
                ViewBag.UserName = user.FullName;
                ViewBag.BranchName = _branchService.FindById(user.BranchId).BranchName;
                return View(productReceiveViewModel);
            }

            ViewData["ProductList"] = new SelectList(_productService.GetAllProducts(), "ProductcId", "ProductName");
            ViewData["BranchList"] = new SelectList(_branchService.GetAllBranches(), "BranchId", "BranchName");
            ViewBag.UserName = user.FullName;
            ViewBag.BranchName = _branchService.FindById(user.BranchId).BranchName;

            return View();
        }

        // POST: ProductReceives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductReceiveViewModel productReceiveViewModel)
        {
            if (ModelState.IsValid)
            {
                MyIdentityDbContext db = new MyIdentityDbContext();
                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
                UserManager<MyIdentityUser> userManager = new UserManager<MyIdentityUser>(userStore);
                MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);

                var productReceive = new ProductReceive()
                {
                    BranchId = user.BranchId,
                    UserId = user.Id,
                    DateReceived = DateTime.Now
                };

                _productReceiveService.AddProductReceive(productReceive);

                return RedirectToAction("Create", "ProductReceives", new { id = productReceive.ProductReceiveId });
            }

            return View(productReceiveViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLineItem([Bind(Include = "ProductReceiveId,Quantity,ProductId,UnitCost")] ProductReceiveLineItemViewModel productReceiveLineItemViewModel)
        {
            var productReceive = _productReceiveService.Get(t => t.ProductReceiveId == productReceiveLineItemViewModel.ProductReceiveId, null, "ProductReceiveLineItems").FirstOrDefault();
            int exists = productReceive.ProductReceiveLineItems.Where(t => t.ProductId == productReceiveLineItemViewModel.ProductId).ToList().Count;
            if (exists > 0)
            {
                return RedirectToAction("Create", "ProductReceives", new { id = productReceive.ProductReceiveId });
            }
            var productReceiveLineItem = new ProductReceiveLineItem()
            {
                ProductId = productReceiveLineItemViewModel.ProductId,
                Quantity = productReceiveLineItemViewModel.Quantity,
                ProductReceiveId = productReceiveLineItemViewModel.ProductReceiveId,
                UnitCost = productReceiveLineItemViewModel.UnitCost, //_productService.FindById(productReceiveLineItemViewModel.ProductId).UnitCost
            };
            productReceive.ProductReceiveLineItems.Add(productReceiveLineItem);
            _productReceiveService.AddProductReceiveLineItem(productReceive, productReceiveLineItem);
            return RedirectToAction("Create", "ProductReceives", new { id = productReceive.ProductReceiveId });
        }

        //Post action for Save data to database
        [HttpPost]
        public JsonResult SavePurchase(ProductReceive O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                MyIdentityDbContext db = new MyIdentityDbContext();
                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
                UserManager<MyIdentityUser> userManager = new UserManager<MyIdentityUser>(userStore);
                MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);

                O.DateReceived = DateTime.Now;
                O.UserId = user.Id;
                _productReceiveService.AddProductReceive(O);
                //Order order = new Order { OrderNo = O.OrderNo, OrderDate = O.OrderDate, Description = O.Description };
                //foreach (var i in O.OrderDetails)
                //{
                //    //
                //    // i.TotalAmount = 
                //    order.OrderDetails.Add(i);
                //}
                //dc.Orders.Add(order);
                //dc.SaveChanges();
                status = true;
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult DeleteLineItem(int id)
        {
            var lineItem = _productReceiveService.FindLineItemById(id);
            var productReceive = _productReceiveService.FindById(lineItem.ProductReceiveId);
            //productReceive.ProductReceiveLineItems.Remove(lineItem);
            _productReceiveService.DeleteProductReceiveLineItem(productReceive, lineItem);
            return RedirectToAction("Create", "ProductReceives", new { id = productReceive.ProductReceiveId });
        }


        // GET: ProductReceives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductReceive productReceive = _productReceiveService.FindById(id.Value);
            if (productReceive == null)
            {
                return HttpNotFound();
            }
            // var productReceivesViewModels = new List<ProductReceiveViewModel>();

            var productReceiveLineItemViewModels = new List<ProductReceiveLineItemViewModel>();
            foreach (var productReceiveLineItem in productReceive.ProductReceiveLineItems)
            {
                var productReceiveLineItemviewModel = new ProductReceiveLineItemViewModel()
                {
                    ProductReceiveLineItemId = productReceiveLineItem.ProductReceiveLineItemId,
                    ProductId = productReceiveLineItem.ProductId,
                    Productname = _productService.FindBy(s => s.ProductcId == productReceiveLineItem.ProductId).First().ProductName,
                    Quantity = productReceiveLineItem.Quantity,
                    UnitCost = productReceiveLineItem.UnitCost

                };
                productReceiveLineItemViewModels.Add(productReceiveLineItemviewModel);
            }



            ViewBag.LineItem = productReceiveLineItemViewModels;
            MyIdentityUser user = userManager.FindById(productReceive.UserId);
            ViewBag.UserName = user.FullName;
            return View(productReceive);
        }

        // POST: ProductReceives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductReceiveId,DateReceived,BranchId,UserId")] ProductReceive productReceive)
        {
            if (ModelState.IsValid)
            {
                _productReceiveService.EditProductReceive(productReceive);
                return RedirectToAction("Index");
            }
            return View(productReceive);
        }

        [Authorize]
        // GET: ProductReceives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductReceive productReceive = _productReceiveService.FindById(id.Value);
            if (productReceive == null)
            {
                return HttpNotFound();
            }
            return View(productReceive);
        }

        // POST: ProductReceives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductReceive productReceive = _productReceiveService.FindById(id);
            _productReceiveService.DeleteProductReceive(productReceive);
            return RedirectToAction("Index", "ProductReceives");
        }


        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var productList = (from product in _productService.GetAllProducts()
                               where product.ProductName.ToLower().Contains(prefix.ToLower())
                               select new
                               {
                                   label = product.ProductName,
                                   val = product.ProductcId
                               }).ToList();
            return Json(productList, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productReceiveService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
