using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REERP.Models.ReportModel;
using REERP.ReportData.Service;

namespace REERP.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IStockReportService _stockReportService;

        public ReportsController(IStockReportService stockReportService)
        {
            this._stockReportService = stockReportService;
        }
        // GET: Reports
        public ActionResult StockStatusAll()
        {
            var stockStatusData=this._stockReportService.GetReportStatus();
            return View(stockStatusData);
        }
    }
}