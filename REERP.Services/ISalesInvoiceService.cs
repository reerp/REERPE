using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoencoPOS.Models;

namespace MoencoPOS.Services
{
    public interface ISalesInvoiceService : IDisposable
    {
        bool AddSalesInvoice(SalesInvoice salesInvoice);
        bool DeleteSalesInvoice(SalesInvoice salesInvoice);
        bool DeleteById(int id);
        bool EditSalesInvoice(SalesInvoice salesInvoice);
        SalesInvoice FindById(int id);
        List<SalesInvoice> GetAllSalesInvoices();
        List<SalesInvoice> FindBy(Expression<Func<SalesInvoice, bool>> predicate);
        IEnumerable<SalesInvoice> Get(
                   Expression<Func<SalesInvoice, bool>> filter = null,
                   Func<IQueryable<SalesInvoice>, IOrderedQueryable<SalesInvoice>> orderBy = null,
                   string includeProperties = "");
    }
}
