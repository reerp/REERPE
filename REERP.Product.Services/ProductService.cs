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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitofWork)
        {
            this._unitOfWork = unitofWork;
        }

        public bool AddProduct(Productc product)
        {
            _unitOfWork.ProductcRepository.Add(product);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ProductcRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ProductcRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteProduct(Productc product)
        {
            if (product == null) return false;
            _unitOfWork.ProductcRepository.Delete(product);
            _unitOfWork.Save();
            return true;
        }

        public bool EditProduct(Productc product)
        {
            _unitOfWork.ProductcRepository.Edit(product);
            _unitOfWork.Save();
            return true;
        }

        public List<Productc> FindBy(Expression<Func<Productc, bool>> predicate)
        {
            return _unitOfWork.ProductcRepository.FindBy(predicate);
        }

        public Productc FindById(int id)
        {
            return _unitOfWork.ProductcRepository.FindById(id);
        }

        public IEnumerable<Productc> Get(Expression<Func<Productc, bool>> filter = null, Func<IQueryable<Productc>, IOrderedQueryable<Productc>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.ProductcRepository.Get(filter, orderBy, includeProperties);
        }

        public List<Productc> GetAllProducts()
        {
            return _unitOfWork.ProductcRepository.GetAll();
        }

        public List<Stock> GetStock()
        {
            return _unitOfWork.StockRepository.GetAll();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
