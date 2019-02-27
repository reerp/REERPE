using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REERP.Models.ViewModels
{
    public class ProductTransferViewModel
    {
        public int ProductTransferId { get; set; }
        public DateTime DateTransfered { get; set; }
        public int FromBranchId { get; set; }
        [Display(Name = "From Branch")]
        public string FromBranchName { get; set; }
        public int ToBranchId { get; set; }
        [Display(Name = "To Branch")]
        public string ToBranchName { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Store Keeper")]
        public string UserName { get; set; }
        public virtual ICollection<ProductTransferLineItem> ProductTransferLineItems { get; set; }
    }
}