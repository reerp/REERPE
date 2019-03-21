using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using REERP.Models;
using REERP.Models.ViewModels;
using REERP.Product.Services;
using REERP.Security;
using REERP.Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace REERP.Controllers
{
    public class ProductTransfersController : Controller
    {
        private readonly IProductTransferService _productTransferService;
        private readonly IBranchService _branchService;
        private readonly IProductService _productService;
        private UserManager<MyIdentityUser> userManager;
        private RoleManager<MyIdentityRole> roleManager;

        public ProductTransfersController(IProductTransferService productTransferService,
            IBranchService branchService, IProductService productService)
        {
            _productTransferService = productTransferService;
            _branchService = branchService;
            _productService = productService;

            MyIdentityDbContext db = new MyIdentityDbContext();

            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);

            RoleStore<MyIdentityRole> roleStore = new RoleStore<MyIdentityRole>(db);
            roleManager = new RoleManager<MyIdentityRole>(roleStore);
        }
        // GET: ProductTransfer
        public ActionResult Index()
        {
            var productTransfers = _productTransferService.GetAllProductTransfer();
            var producttransferViewModels = new List<ProductTransferViewModel>();
            foreach (var productTransfer in productTransfers)
            {
                var productTransferViewModel = new ProductTransferViewModel()
                {
                    FromBranchId = productTransfer.FromBranchId,
                    ToBranchId = productTransfer.ToBranchId,
                    ProductTransferId = productTransfer.ProductTransferId,
                    DateTransfered = productTransfer.DateTransfered,
                    UserId = productTransfer.UserId,
                    UserName = userManager.FindById(productTransfer.UserId).FullName
                };
                productTransferViewModel.FromBranchName = _branchService.FindById(productTransferViewModel.FromBranchId).BranchName;
                productTransferViewModel.ToBranchName = _branchService.FindById(productTransferViewModel.ToBranchId).BranchName;

                producttransferViewModels.Add(productTransferViewModel);

            }
            return View(producttransferViewModels);
        }

        // GET: ProductTransfer/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTransfer productTransfer = _productTransferService.FindById(id);
            if (productTransfer == null)
            {
                return HttpNotFound();
            }
            return View(productTransfer);
        }

        // GET: ProductTransfer/Create
        public ActionResult Create(int? id)
        {
            MyIdentityDbContext db = new MyIdentityDbContext();
            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            UserManager<MyIdentityUser> userManager = new UserManager<MyIdentityUser>(userStore);
            MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);

            if (id != null && id != 0)
            {
                var productTransfer = _productTransferService.Get(t => t.ProductTransferId == id, null, "ProductTransferLineItems").FirstOrDefault();
                var productTransferViewModel = new ProductTransferViewModel()
                {
                    FromBranchId = productTransfer.FromBranchId,
                    ToBranchId = productTransfer.ToBranchId,
                    ProductTransferId = productTransfer.ProductTransferId,
                    DateTransfered = productTransfer.DateTransfered,
                    UserId = productTransfer.UserId,
                    UserName = userManager.FindById(productTransfer.UserId).FullName
                };
                //string FromBranchName = _branchService.FindById(productTransferViewModel.FromBranchId).BranchName;
                //string ToBranchName = _branchService.FindById(productTransferViewModel.ToBranchId).BranchName;
                //productTransferViewModel.FromBranchName = FromBranchName;
                //productTransferViewModel.ToBranchName = ToBranchName;
                //ViewBag.FromBranchName = FromBranchName;
                //ViewBag.ToBranchName = ToBranchName;

                string FromBranchName = _branchService.FindById(productTransferViewModel.FromBranchId).BranchName;
                string ToBranchName = _branchService.FindById(productTransferViewModel.ToBranchId).BranchName;
                productTransferViewModel.FromBranchName = FromBranchName;
                productTransferViewModel.ToBranchName = ToBranchName;
                ViewBag.FromBranchList = new SelectList(_branchService.GetAllBranches(), "BranchId", "BranchName", productTransfer.FromBranchId);
                ViewBag.ToBranchList = new SelectList(_branchService.GetAllBranches(), "BranchId", "BranchName", productTransfer.ToBranchId);


                ViewBag.UserName = user.FullName;
                ViewBag.ProductTransferId = productTransfer.ProductTransferId;
                var productTransferLineItemViewModels = new List<ProductTransferLineItemViewModel>();
                foreach (var productTransferLineItem in productTransfer.ProductTransferLineItems)
                {
                    var productTransferLineItemviewModel = new ProductTransferLineItemViewModel()
                    {
                        ProductTransferLineItemId = productTransferLineItem.ProductTransferLineItemId,
                        ProductId = productTransferLineItem.ProductId,
                        Productname = _productService.FindBy(s => s.ProductcId == productTransferLineItem.ProductId).First().ProductName,
                        Quantity = productTransferLineItem.Quantity,
                    };
                    productTransferLineItemViewModels.Add(productTransferLineItemviewModel);
                }
                ViewData["ProductList"] = new SelectList(_productService.GetAllProducts(), "ProductcId", "ProductName");
                ViewBag.ProductTransferLineItemViewModels = productTransferLineItemViewModels;
                //ViewBag.UserName = user.FullName;
                //ViewBag.BranchName = _branchService.FindById(user.BranchId).BranchName;
                return View(productTransferViewModel);
            }


            ViewBag.UserName = user.FullName;
            //ViewData["BranchList"] = new SelectList(_branchService.GetAllBranches(), "BranchId", "BranchName");
            //ViewBag.FromBranchName = _branchService.FindById(1).BranchName; ;
            //ViewBag.ToBranchName = _branchService.FindById(2).BranchName; ;
            ViewBag.FromBranchList = new SelectList(_branchService.GetAllBranches(), "BranchId", "BranchName");
            ViewBag.ToBranchList = new SelectList(_branchService.GetAllBranches(), "BranchId", "BranchName");

            return View();
        }

        // POST: ProductTransfer/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "DateTransfered,FromBranchId,ToBranchId,UserId")] ProductTransferViewModel productTransferViewModel)
        {
            if (ModelState.IsValid)
            {
                MyIdentityDbContext db = new MyIdentityDbContext();
                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
                UserManager<MyIdentityUser> userManager = new UserManager<MyIdentityUser>(userStore);
                MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);

                var productTransfer = new ProductTransfer()
                {
                    //FromBranchId =1, //productTransferViewModel.FromBranchId,
                    //ToBranchId= 2, //productTransferViewModel.ToBranchId,
                    FromBranchId = productTransferViewModel.FromBranchId,
                    ToBranchId = productTransferViewModel.ToBranchId,
                    UserId = user.Id,
                    DateTransfered = DateTime.Now
                };

                _productTransferService.AddProductTransfer(productTransfer);

                return RedirectToAction("Create", "ProductTransfers", new { id = productTransfer.ProductTransferId });
            }

            return View(productTransferViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLineItem([Bind(Include = "ProductTransferId,Quantity,ProductId")] ProductTransferLineItemViewModel productTransferLineItemViewModel)
        {
            var productTransfer = _productTransferService.Get(t => t.ProductTransferId == productTransferLineItemViewModel.ProductTransferId, null, "ProductTransferLineItems").FirstOrDefault();
            int exists = productTransfer.ProductTransferLineItems.Where(t => t.ProductId == productTransferLineItemViewModel.ProductId).ToList().Count;
            bool available = _productTransferService.IsAvailable(productTransferLineItemViewModel.ProductId, productTransfer.FromBranchId, productTransferLineItemViewModel.Quantity);
            if (exists > 0 || !available)
            {
                return RedirectToAction("Create", "ProductTransfers", new { id = productTransfer.ProductTransferId });
            }
            var productTransferLineItem = new ProductTransferLineItem()
            {
                ProductId = productTransferLineItemViewModel.ProductId,
                Quantity = productTransferLineItemViewModel.Quantity,
                ProductTransferId = productTransferLineItemViewModel.ProductTransferId,
            };
            productTransfer.ProductTransferLineItems.Add(productTransferLineItem);
            _productTransferService.AddProductTransferLineItem(productTransfer, productTransferLineItem);
            return RedirectToAction("Create", "ProductTransfers", new { id = productTransfer.ProductTransferId });
        }

        public ActionResult DeleteLineItem(int id)
        {
            var lineItem = _productTransferService.FindLineItemById(id);
            var productTransfer = _productTransferService.FindById(lineItem.ProductTransferId);
            //productTransfer.ProductTransferLineItems.Remove(lineItem);
            //_productTransferService.DeleteProductTransferLineItem(productTransfer, lineItem);
            _productTransferService.DeleteProductTransferLineItem(productTransfer, lineItem);
            return RedirectToAction("Create", "ProductTransfers", new { id = productTransfer.ProductTransferId });

        }


        // GET: ProductTransfer/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTransfer productTransfer = _productTransferService.FindById(id);
            if (productTransfer == null)
            {
                return HttpNotFound();
            }
            return View(productTransfer);
        }

        // POST: ProductTransfer/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ProductTransferId,DateTransfered,FromBranchId,ToBranchId,UserId")] ProductTransfer productTransfer)
        {
            if (ModelState.IsValid)
            {
                _productTransferService.EditProductTransfer(productTransfer);
                return RedirectToAction("Index");
            }
            return View(productTransfer);
        }

        // GET: ProductTransfer/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTransfer productTransfer = _productTransferService.FindById(id);
            if (productTransfer == null)
            {
                return HttpNotFound();
            }
            return View(productTransfer);
        }

        // POST: ProductTransfer/Delete/5
        // POST: ProductReceives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductTransfer productTransfer = _productTransferService.FindById(id);
            _productTransferService.DeleteProductTransfer(productTransfer);
            return RedirectToAction("Index", "ProductTransfers");
        }

        public ActionResult DeleteTransfer(int id)
        {
            ProductTransfer productTransfer = _productTransferService.FindById(id);
            if (productTransfer.ProductTransferLineItems == null || productTransfer.ProductTransferLineItems.Count == 0)
                _productTransferService.DeleteProductTransfer(productTransfer);
            return RedirectToAction("Index", "ProductTransfers");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productTransferService.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult SaveTransfer(ProductTransfer O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                MyIdentityDbContext db = new MyIdentityDbContext();
                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
                UserManager<MyIdentityUser> userManager = new UserManager<MyIdentityUser>(userStore);
                MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);

                O.DateTransfered = DateTime.Now;
                O.UserId = user.Id;

                _productTransferService.AddProductTransfer(O);
                status = true;
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }


    }
}
