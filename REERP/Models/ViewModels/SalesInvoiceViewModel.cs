using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REERP.Models.ViewModels
{
    public class SalesInvoiceViewModel
    {
        public int SalesInvoiceId { get; set; }
        [Display(Name = "Reference Number")]
        public string ReferenceNo { get; set; }
        public int CustomerId { get; set; }
        [Display(Name = "Customer")]
        public string CustomerName { get; set; }
        public int BranchId { get; set; }
        [Display(Name = "Branch")]
        public string BranchName { get; set; }
        [Display(Name = "Credit/Cash")]
        public int SalesType { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Sales Person")]
        public string UserName { get; set; }
        public DateTime DateSold { get; set; }
        public string Status { get; set; }

        public List<SalesInvoiceLineItemViewModel> SalesInvoiceLineItemViewModels { get; set; }
    }
}