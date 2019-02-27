using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using REERP.Models;

namespace REERP.Product.Services
{
    public interface ICategoryService:IDisposable
    {
        bool AddCategory(Category category);
        bool DeleteCategory(Category category);
        bool DeleteById(int id);
        bool EditCategory(Category category);
        Category FindById(int id);
        List<Category> GetAllCategories();
        List<Category> FindBy(Expression<Func<Category, bool>> predicate);
        IEnumerable<Category> Get(
                   Expression<Func<Category, bool>> filter = null,
                   Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
                   string includeProperties = "");

    }
}
