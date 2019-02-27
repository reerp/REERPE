using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace REERP.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [StringLength(150)]
        [DisplayName("Category Description")]
        public string CategoryDescription { get; set; }

        public virtual ICollection<Productc> Products { get; set; }
    }
}
