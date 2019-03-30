using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REERP.Models
{
    public class Stock
    {
        public int StockId { get; set; }
        public int BranchId { get; set; }
        public string ProductcId { get; set; }
        public Decimal Quantity { get; set; }

        public virtual Productc Productc { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
