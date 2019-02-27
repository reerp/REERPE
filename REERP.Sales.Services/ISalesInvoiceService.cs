using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using REERP.Models;

namespace REERP.Sales.Services
{
    public interface ISalesInvoiceService : IDisposable
    {
        bool AddSalesInvoice(SalesInvoice salesInvoice);
        bool DeleteSalesInvoice(SalesInvoice salesInvoice);
        bool DeleteById(int id);
        bool DeleteLineItemById(int id);
        bool EditSalesInvoice(SalesInvoice salesInvoice);
        bool AddInvoiceLineItem(SalesInvoice salesInvoice, SalesLineItem item);
        bool DeleteInvoiceLineItem(SalesInvoice salesInvoice, SalesLineItem item);
        SalesInvoice FindById(int id);
        SalesLineItem FindLineItemById(int id);
        List<SalesInvoice> GetAllSalesInvoices();
        List<SalesInvoice> FindBy(Expression<Func<SalesInvoice, bool>> predicate);
        IEnumerable<SalesInvoice> Get(
                   Expression<Func<SalesInvoice, bool>> filter = null,
                   Func<IQueryable<SalesInvoice>, IOrderedQueryable<SalesInvoice>> orderBy = null,
                   string includeProperties = "");

        bool IsAvailable(string productId, int branchId, decimal quantity);

    }
}
