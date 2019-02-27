
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REERP.Models
{
    public class SalesInvoice
    {
        public int SalesInvoiceId { get; set; }
        public string ReferenceNo { get; set; }
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        public int SalesType { get; set; }
        public string UserId { get; set; }
        public DateTime DateSold { get; set; }
        public string Status { get; set; }

        public virtual ICollection<SalesLineItem> SalesLineItems { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
