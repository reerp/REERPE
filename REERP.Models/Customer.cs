using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REERP.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        
        [StringLength(50)]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }        
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Telephone No.")]
        public string TelephoneNo { get; set; }
        [DisplayName("TIN")]
        public string TIN { get; set; }
        [DisplayName("VAT Number")]
        public string VATNumber { get; set; }
        [DisplayName("Trusted")]
        public bool Trusted { get; set; }
    }
}
