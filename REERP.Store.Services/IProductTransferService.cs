using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REERP.Models;

namespace REERP.Store.Services
{
    public interface IProductTransferService : IDisposable
    {
        bool AddProductTransfer(ProductTransfer productTransfer);
        bool DeleteProductTransfer(ProductTransfer productTransfer);
        bool DeleteById(int id);
        bool EditProductTransfer(ProductTransfer productTransfer);
        bool AddProductTransferLineItem(ProductTransfer productTransfer, ProductTransferLineItem item);
        bool DeleteProductTransferLineItem(ProductTransfer productTransfer, ProductTransferLineItem lineItem);
        ProductTransferLineItem FindLineItemById(int Id);
        ProductTransfer FindById(int id);
        List<ProductTransfer> GetAllProductTransfer();
        List<ProductTransfer> FindBy(Expression<Func<ProductTransfer, bool>> predicate);
        IEnumerable<ProductTransfer> Get(
                   Expression<Func<ProductTransfer, bool>> filter = null,
                   Func<IQueryable<ProductTransfer>, IOrderedQueryable<ProductTransfer>> orderBy = null,
                   string includeProperties = "");
        bool IsAvailable(string productId, int branchId, decimal quantity);
    }
}
