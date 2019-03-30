using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using REERP.Models;
using REERP.DAL.UnitOfWork;

namespace REERP.Store.Services
{
    public class ProductReceiveService : IProductReceiveService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductReceiveService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool AddProductReceive(ProductReceive productReceive)
        {
            _unitOfWork.ProductReceiveRepository.Add(productReceive);
            if (productReceive.ProductReceiveLineItems != null)
            {
                foreach (var item in productReceive.ProductReceiveLineItems)
                {
                    AddLineItemStock(item, productReceive.BranchId);
                }
            }
            _unitOfWork.Save();
            return true;
        }

        public bool AddProductReceiveLineItem(ProductReceive productReceive, ProductReceiveLineItem item)
        {
            _unitOfWork.ProductReceiveRepository.Edit(productReceive);
            AddLineItemStock(item, productReceive.BranchId);
            _unitOfWork.Save();
            return true;
        }

        void AddLineItemStock(ProductReceiveLineItem item, int branchId)
        {
            var stock = _unitOfWork.StockRepository.FindBy(x => x.BranchId == branchId
                                                                && x.ProductcId == item.ProductId).SingleOrDefault();
            if (stock == null)
            {
                stock = new Stock()
                {
                    BranchId = branchId,
                    ProductcId = item.ProductId,
                    Quantity = item.Quantity,
                };
                _unitOfWork.StockRepository.Add(stock);
            }
            else
            {
                stock.Quantity = stock.Quantity + item.Quantity;
                _unitOfWork.StockRepository.Edit(stock);
            }

            var product = _unitOfWork.ProductcRepository.FindBy(s=>s.ProductcId==item.ProductId).First();
            product.UnitCost = item.UnitCost;
            _unitOfWork.ProductcRepository.Edit(product);
        }

        public bool DeleteProductReceiveLineItem(ProductReceive productReceive, ProductReceiveLineItem lineItem)
        {
            //_unitOfWork.ProductReceiveRepository.Edit(productReceive);
            _unitOfWork.ProductReceiveLineItemRepository.Delete(lineItem);
            SubtractLineItemStock(lineItem, productReceive.BranchId);
            _unitOfWork.Save();
            return true;
        }

        void SubtractLineItemStock(ProductReceiveLineItem item, int branchId)
        {
            var stock = _unitOfWork.StockRepository.FindBy(x => x.BranchId == branchId
                                                                && x.ProductcId == item.ProductId).SingleOrDefault();
            stock.Quantity = stock.Quantity - item.Quantity;
            _unitOfWork.StockRepository.Edit(stock);

        }

        private void ResetProductCost(ProductReceiveLineItem item)
        {
            var product = _unitOfWork.ProductcRepository.FindBy(s=>s.ProductcId==item.ProductId).First();
            product.UnitCost = _unitOfWork.ProductReceiveLineItemRepository.FindBy(x => x.ProductId == item.ProductId).LastOrDefault().UnitCost;
            _unitOfWork.ProductcRepository.Edit(product);
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ProductReceiveRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ProductReceiveRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteProductReceive(ProductReceive productReceive)
        {
            if (productReceive == null) return false;
            _unitOfWork.ProductReceiveRepository.Delete(productReceive);
            _unitOfWork.Save();
            return true;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public bool EditProductReceive(ProductReceive productReceive)
        {
            _unitOfWork.ProductReceiveRepository.Edit(productReceive);
            _unitOfWork.Save();
            return true;
        }

        public List<ProductReceive> FindBy(Expression<Func<ProductReceive, bool>> predicate)
        {
            return _unitOfWork.ProductReceiveRepository.FindBy(predicate);
        }

        public ProductReceive FindById(int id)
        {
            return _unitOfWork.ProductReceiveRepository.FindById(id);
        }

        public ProductReceiveLineItem FindLineItemById(int id)
        {
            return _unitOfWork.ProductReceiveLineItemRepository.FindById(id);
        }


        public IEnumerable<ProductReceive> Get(Expression<Func<ProductReceive, bool>> filter = null, Func<IQueryable<ProductReceive>, IOrderedQueryable<ProductReceive>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.ProductReceiveRepository.Get(filter, orderBy, includeProperties);
        }

        public List<ProductReceive> GetAllProductReceive()
        {
            return _unitOfWork.ProductReceiveRepository.GetAll();
        }
    }
}
