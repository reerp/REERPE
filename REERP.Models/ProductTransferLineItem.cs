using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REERP.Models
{
    public class ProductTransferLineItem
    {
        public int ProductTransferLineItemId { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public decimal Quantity { get; set; }

        public int ProductTransferId { get; set; }
        public ProductTransfer ProductTransfer { get; set; }
    }
}
