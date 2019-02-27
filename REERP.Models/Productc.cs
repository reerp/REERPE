using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace REERP.Models
{
    public class Productc
    {
        [Required]
        [StringLength(30)]
        [DisplayName("Product Code")]
        public string ProductcId { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [StringLength(150)]
        [DisplayName("Product Description")]
        public string ProductDescription { get; set; }
        [StringLength(50)]
        public string Model { get; set; }
        [StringLength(10)]
        [DisplayName("Unit Of Measure")]
        public string UnitOfMeasure { get; set; }
        [Range(1,10000000)]
        [DisplayName("Unit Cost")]
        public decimal UnitCost { get; set; }
        [Range(1, 10000000)]
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } 
    }
}
