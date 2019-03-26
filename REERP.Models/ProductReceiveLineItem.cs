using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        //        [Range(1, 10000000)]
        //        [DisplayName("Unit Price")]
        //        public decimal UnitPrice { get; set; }

        public int ProductReceiveId { get; set; }
        public ProductReceive ProductReceive { get; set; }
    }
}
