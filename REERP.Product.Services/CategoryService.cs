using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using REERP.DAL.UnitOfWork;
using REERP.Models;

namespace REERP.Product.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitofWork)
        {
            this._unitOfWork = unitofWork;
        }

        public bool AddCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.CategoryRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.CategoryRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteCategory(Category category)
        {
            if (category == null) return false;
            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Save();
            return true;
        }

        public bool EditCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Edit(category);
            _unitOfWork.Save();
            return true;
        }

        public List<Category> FindBy(Expression<Func<Category, bool>> predicate)
        {
            return _unitOfWork.CategoryRepository.FindBy(predicate);
        }

        public Category FindById(int id)
        {
            return _unitOfWork.CategoryRepository.FindById(id);
        }

        public IEnumerable<Category> Get(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.CategoryRepository.Get(filter, orderBy, includeProperties);
        }

        public List<Category> GetAllCategories()
        {
            return _unitOfWork.CategoryRepository.GetAll();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
