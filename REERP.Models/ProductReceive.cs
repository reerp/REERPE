using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REERP.Models
{
    public class ProductReceive
    {
        public int ProductReceiveId { get; set; }
        public DateTime DateReceived { get; set; }
        public int BranchId { get; set; }
        public string InvoiceNO { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<ProductReceiveLineItem> ProductReceiveLineItems { get; set; }
        public virtual Branch Branch { get; set; }

    }
}
