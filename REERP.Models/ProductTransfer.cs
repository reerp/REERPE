using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REERP.Models
{
    public class ProductTransfer
    {
        public int ProductTransferId { get; set; }
        public DateTime DateTransfered { get; set; }
        public int FromBranchId { get; set; }
        public int ToBranchId { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<ProductTransferLineItem> ProductTransferLineItems { get; set; }
        public virtual Branch FromBranch { get; set; }
        public virtual Branch ToBranch { get; set; }
    }
}
