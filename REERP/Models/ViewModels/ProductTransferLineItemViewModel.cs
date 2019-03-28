using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [DisplayName("Unit Cost")]
        public decimal UnitCost { get; set; }

        public int ProductTransferId { get; set; }
        public ProductTransfer ProductTransfer { get; set; }

    }
}