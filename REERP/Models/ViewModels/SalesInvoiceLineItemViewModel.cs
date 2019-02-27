using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REERP.Models.ViewModels
{
    public class SalesInvoiceLineItemViewModel
    {
        public int SalesLineItemId { get; set; }
        public int SalesInvoiceId { get; set; }
        [Display(Name = "Reference Number")]
        public string ReferenceNo { get; set; }
        public string ProductId { get; set; }
        [Display(Name = "Product name")]
        public string Productname { get; set; }
        [Display(Name = "Unit Price")]
        public Decimal UnitPrice { get; set; }
        public Decimal Quantity { get; set; }    
        public Decimal TotalPrice { get; set; }    
    }
}