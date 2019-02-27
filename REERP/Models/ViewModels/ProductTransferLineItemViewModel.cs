using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REERP.Models.ViewModels
{
    public class ProductTransferLineItemViewModel
    {
        public int ProductTransferLineItemId { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Display(Name = "Product name")]
        public string Productname { get; set; }
        [Required]
        public decimal Quantity { get; set; }

        public int ProductTransferId { get; set; }
        public ProductTransfer ProductTransfer { get; set; }

    }
}