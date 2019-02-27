using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using REERP.Models;

namespace REERP.Product.Services
{
    public interface IProductService : IDisposable
    {
        bool AddProduct(Productc product);
        bool DeleteProduct(Productc product);
        bool DeleteById(int id);
        bool EditProduct(Productc product);
        Productc FindById(int id);
        List<Productc> GetAllProducts();
        List<Productc> FindBy(Expression<Func<Productc, bool>> predicate);
        IEnumerable<Productc> Get(
                   Expression<Func<Productc, bool>> filter = null,
                   Func<IQueryable<Productc>, IOrderedQueryable<Productc>> orderBy = null,
                   string includeProperties = "");

        List<Stock> GetStock();

        }
}
