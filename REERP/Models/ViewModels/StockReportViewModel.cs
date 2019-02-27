using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REERP.Models.ViewModels
{
    public class StockReportViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitCost { get; set; }
        public decimal QuantityOnHand { get; set; }

    }
}