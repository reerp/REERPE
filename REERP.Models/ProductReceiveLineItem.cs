using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REERP.Models
{
    public class ProductReceiveLineItem
    {
        public int ProductReceiveLineItemId { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        [Range(1, 10000000)]
        [DisplayName("Unit Cost")]
        public decimal UnitCost { get; set; }

        public int ProductReceiveId { get; set; }
        public ProductReceive ProductReceive { get; set; }
    }
}
