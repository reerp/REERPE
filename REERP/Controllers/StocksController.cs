using REERP.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REERP.Models;
using REERP.Product.Services;
using Microsoft.Reporting.WebForms;
using REERP.DAL;
using REERP.Views.Reports;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace REERP.Controllers
{
    public class StocksController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBranchService _branchService;
        private readonly ICategoryService _categoryService;

        StockReportData ds = new StockReportData();

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

        public ActionResult ReportStockStatus()
        {
            //List<StockReportViewModel> stocks = new List<StockReportViewModel>();

            //using (var ctx = new REERPContext())
            //{
            string stocksSql = @"Select Branch.BranchId,  
                                        Branch.BranchName,
                                        Category.CategoryId,
                                        Category.CategoryName,
                                        Productc.ProductcId,
                                        Productc.ProductName,
                                        Productc.UnitOfMeasure,
                                        Productc.UnitCost,
                                        Stock.Quantity,
                                        Productc.UnitCost * Stock.Quantity As TotalCost
                                        from Productc, Stock, Branch, Category
                                        where Productc.ProductcId = Stock.ProductId and
                                        Stock.BranchId = Branch.BranchId and
                                        Productc.CategoryId = Category.CategoryId and Stock.Quantity>0";

            //Warning[] warnings;
            //string mimeType;
            //string[] streamids;
            //string encoding;
            //string filenameExtension;

            //var viewer = new ReportViewer();
            //viewer.LocalReport.ReportPath = @"Reports\ProductStatus.rdlc";

            //viewer.LocalReport.DataSources.Add(new ReportDataSource("ReportData", stocks));
            //viewer.LocalReport.Refresh();

            //var bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);

            var connectionString = ConfigurationManager.ConnectionStrings["REERPContext"].ConnectionString;


            SqlConnection conx = new SqlConnection(connectionString); SqlDataAdapter adp = new SqlDataAdapter(stocksSql, conx);

            adp.Fill(ds, ds.ProductStatus.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Views\Reports\StockStatus.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));


            ViewBag.ReportViewer = reportViewer;

            return View();
        }

        public ActionResult ReportStockStatusByBranch()
        {
            string stocksSql = @"Select Branch.BranchId,  
                                        Branch.BranchName,
                                        Category.CategoryId,
                                        Category.CategoryName,
                                        Productc.ProductcId,
                                        Productc.ProductName,
                                        Productc.UnitOfMeasure,
                                        Productc.UnitCost,
                                        Stock.Quantity,
                                        Productc.UnitCost * Stock.Quantity As TotalCost
                                        from Productc, Stock, Branch, Category
                                        where Productc.ProductcId = Stock.ProductId and
                                        Stock.BranchId = Branch.BranchId and
                                        Productc.CategoryId = Category.CategoryId and Stock.Quantity>0";


            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);

            var connectionString = ConfigurationManager.ConnectionStrings["REERPContext"].ConnectionString;


            SqlConnection conx = new SqlConnection(connectionString); SqlDataAdapter adp = new SqlDataAdapter(stocksSql, conx);

            adp.Fill(ds, ds.ProductStatus.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Views\Reports\StockStatusByBranch.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));


            ViewBag.ReportViewer = reportViewer;

            return View();
        }

        public ActionResult RStockByBranch()
        {
            string stocksSql = @"Select Branch.BranchId,  
                                        Branch.BranchName,
                                        Category.CategoryId,
                                        Category.CategoryName,
                                        Productc.ProductcId,
                                        Productc.ProductName,
                                        Productc.UnitOfMeasure,
                                        Productc.UnitCost,
                                        Stock.Quantity,
                                        Productc.UnitCost * Stock.Quantity As TotalCost
                                        from Productc, Stock, Branch, Category
                                        where Productc.ProductcId = Stock.ProductId and
                                        Stock.BranchId = Branch.BranchId and
                                        Productc.CategoryId = Category.CategoryId and Stock.Quantity>0";


            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);

            var connectionString = ConfigurationManager.ConnectionStrings["REERPContext"].ConnectionString;


            SqlConnection conx = new SqlConnection(connectionString); SqlDataAdapter adp = new SqlDataAdapter(stocksSql, conx);

            adp.Fill(ds, ds.ProductStatus.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Views\Reports\StockStatusByBranch.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));


            ViewBag.ReportViewer = reportViewer;

            return View();
        }

    }
}