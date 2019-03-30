using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REERP.Models.ReportModel;

namespace REERP.ReportData.Service
{
    public interface IStockReportService: IDisposable
    {
         List<StockViewModel> GetReportStatus();
    }
}
