using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REERP.Models.ViewModels
{
    public class ProductReceiveViewModel
    {
        public int ProductReceiveId { get; set; }
        public DateTime DateReceived { get; set; }
        public int BranchId { get; set; }
        [Display(Name = "Branch")]
        public string BranchName { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Store Keeper")]
        public string UserName { get; set; }
        public virtual ICollection<ProductReceiveLineItem> ProductReceiveLineItems { get; set; }
    }
}