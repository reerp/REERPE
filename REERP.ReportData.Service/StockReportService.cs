using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REERP.Models.ReportModel;
using REERP.DAL;

namespace REERP.ReportData.Service
{
    public class StockReportService: IStockReportService
    {
        private readonly REERPContext context = new REERPContext();
        public List<StockViewModel> GetReportStatus()
        {
            var stockStatus = new List<StockViewModel>();
            stockStatus = (from s in context.Stocks
                           where s.Quantity > 0
                           select new StockViewModel
                           {
                               CategoryName = s.Productc.Category.CategoryName,
                               ProductId = s.ProductcId,
                               ProductName = s.Productc.ProductName,
                               UnitOfMeasure = s.Productc.UnitOfMeasure,
                               UnitCost = s.Productc.UnitCost,
                               QuantityOnHand = s.Quantity,
                               TotalCost = s.Quantity * s.Productc.UnitCost,
                           }
                           ).ToList();

            stockStatus.GroupBy(g=>g.ProductId)
                               .Select (x=> new StockViewModel
                               {
                                   CategoryName=x.First().CategoryName,
                                   ProductName=x.First().ProductName,
                                   UnitOfMeasure=x.First().UnitOfMeasure,
                                   UnitCost=x.First().UnitCost,
                                   QuantityOnHand=x.Sum(g => g.QuantityOnHand),
                                   TotalCost=x.Sum(g => g.TotalCost),
                               }).ToList();

            return stockStatus;
        }

        public List<StockViewModel> GetReportStatusByBranch(int branchId)
        {
            var stockStatus = new List<StockViewModel>();
            using (var context = new REERPContext())
            {
                stockStatus = (from s in context.Stocks
                               where s.Quantity > 0 && s.BranchId==branchId
                               select new StockViewModel
                               {
                                   BranchName = s.Branch.BranchName,
                                   CategoryName = s.Productc.Category.CategoryName,
                                   ProductId = s.ProductcId,
                                   ProductName = s.Productc.ProductName,
                                   UnitOfMeasure = s.Productc.UnitOfMeasure,
                                   UnitCost = s.Productc.UnitCost,
                                   TotalCost = s.Quantity * s.Productc.UnitCost,
                               }).ToList<StockViewModel>();

            }
            return stockStatus;
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}
