using REERP.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REERP.Models;
using REERP.Product.Services;

namespace REERP.Controllers
{
    public class StocksController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBranchService _branchService;


        public StocksController(IProductService productService,
            IBranchService branchService)
        {
            _productService = productService;
            _branchService = branchService;
        }

        // GET: Stocks
        public ActionResult ReportStockAllBranch()
        {
            List<StockReportViewModel> stocks = new List<StockReportViewModel>();
            foreach(var product in _productService.GetAllProducts())
            {
                var reportModel = new StockReportViewModel();
                reportModel.ProductId = product.ProductcId;
                reportModel.ProductName = product.ProductName;
                reportModel.UnitCost = product.UnitCost;
                reportModel.QuantityOnHand = _productService.GetStock().Where(e => e.ProductId == product.ProductcId).Sum(e=>e.Quantity);
                stocks.Add(reportModel);                            
            }
            return View(stocks);
        }

        public ActionResult ReportStockByBranch(int? branchId)
        {
            var branches = _branchService.GetAllBranches();

            if (branchId == null || branchId == 0)
            {
                branchId = branches.FirstOrDefault().BranchId;
            }

            List<StockReportViewModel> stocks = new List<StockReportViewModel>();
            foreach (var product in _productService.GetAllProducts())
            {
                var reportModel = new StockReportViewModel();
                reportModel.ProductId = product.ProductcId;
                reportModel.ProductName = product.ProductName;
                reportModel.UnitCost = product.UnitCost;
                 Stock s=_productService.GetStock().Where(e => e.ProductId == product.ProductcId
                                                && e.BranchId == branchId).FirstOrDefault();
                if (s == null)
                    reportModel.QuantityOnHand = 0;
                else
                    reportModel.QuantityOnHand = s.Quantity;
                stocks.Add(reportModel);
            }

            ViewBag.BranchList = new SelectList(branches, "BranchId", "BranchName", branchId);

            return View(stocks);
        }


    }
}