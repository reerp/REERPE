using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using REERP.Models;

namespace REERP.Store.Services
{
    public interface IProductReceiveService : IDisposable
    {
        bool AddProductReceive(ProductReceive productReceive);
        bool DeleteProductReceive(ProductReceive productReceive);
        bool DeleteById(int id);
        bool EditProductReceive(ProductReceive productReceive);
        bool AddProductReceiveLineItem(ProductReceive productReceive, ProductReceiveLineItem item);
        bool DeleteProductReceiveLineItem(ProductReceive productReceive, ProductReceiveLineItem lineItem);
        ProductReceiveLineItem FindLineItemById(int Id);
        ProductReceive FindById(int id);
        List<ProductReceive> GetAllProductReceive();
        List<ProductReceive> FindBy(Expression<Func<ProductReceive, bool>> predicate);
        IEnumerable<ProductReceive> Get(
                   Expression<Func<ProductReceive, bool>> filter = null,
                   Func<IQueryable<ProductReceive>, IOrderedQueryable<ProductReceive>> orderBy = null,
                   string includeProperties = "");
    }
}
