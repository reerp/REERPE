using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace REERP.Models
{
    public class Branch
    {
        public int BranchId { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Branch Name")]
        public string BranchName { get; set; }
        [StringLength(50)]
        [DisplayName("Branch Location")]
        public string BranchLocation { get; set; }
        [StringLength(150)]
        [DisplayName("Branch Description")]
        public string BranchDescription { get; set; }
    }
}
