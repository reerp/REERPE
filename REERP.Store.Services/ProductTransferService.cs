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
    public class ProductTransferService : IProductTransferService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductTransferService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool AddProductTransfer(ProductTransfer productTransfer)
        {
            _unitOfWork.ProductTransferRepository.Add(productTransfer);
            if (productTransfer.ProductTransferLineItems != null)
            {
                foreach (var item in productTransfer.ProductTransferLineItems)
                {
                    AddLineItemStock(item, productTransfer.ToBranchId);
                    SubtractLineItemStock(item, productTransfer.FromBranchId);
                }
            }
            _unitOfWork.Save();
            return true;
        }
        void AddLineItemStock(ProductTransferLineItem item, int branchId)
        {
            var stock = _unitOfWork.StockRepository.FindBy(x => x.BranchId == branchId
                                                                && x.ProductId == item.ProductId).SingleOrDefault();
            if (stock == null)
            {
                stock = new Stock()
                {
                    BranchId = branchId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                };
                _unitOfWork.StockRepository.Add(stock);
            }
            else
            {
                stock.Quantity = stock.Quantity + item.Quantity;
                _unitOfWork.StockRepository.Edit(stock);
            }

        }

        void SubtractLineItemStock(ProductTransferLineItem item, int branchId)
        {
            var stock = _unitOfWork.StockRepository.FindBy(x => x.BranchId == branchId
                                                                && x.ProductId == item.ProductId).SingleOrDefault();
            stock.Quantity = stock.Quantity - item.Quantity;
            _unitOfWork.StockRepository.Edit(stock);

        }

        public bool AddProductTransferLineItem(ProductTransfer productTransfer, ProductTransferLineItem item)
        {
            _unitOfWork.ProductTransferRepository.Edit(productTransfer);
            AddLineItemStock(item, productTransfer.ToBranchId);
            SubtractLineItemStock(item, productTransfer.FromBranchId);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ProductTransferRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ProductTransferRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteProductTransfer(ProductTransfer productTransfer)
        {
            if (productTransfer == null) return false;
            _unitOfWork.ProductTransferRepository.Delete(productTransfer);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteProductTransferLineItem(ProductTransfer productTransfer, ProductTransferLineItem lineItem)
        {
            //_unitOfWork.ProductTransferRepository.Edit(productTransfer);
            //SubtractLineItemStock(lineItem, productTransfer.FromBranchId);
            //_unitOfWork.Save();
            //return true;

            _unitOfWork.ProductTransferLineItemRepository.Delete(lineItem);
            SubtractLineItemStock(lineItem, productTransfer.ToBranchId);
            AddLineItemStock(lineItem, productTransfer.FromBranchId);
            _unitOfWork.Save();
            return true;

        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public bool EditProductTransfer(ProductTransfer productTransfer)
        {
            _unitOfWork.ProductTransferRepository.Edit(productTransfer);
            _unitOfWork.Save();
            return true;
        }

        public List<ProductTransfer> FindBy(Expression<Func<ProductTransfer, bool>> predicate)
        {
            return _unitOfWork.ProductTransferRepository.FindBy(predicate);
        }

        public ProductTransfer FindById(int id)
        {
            return _unitOfWork.ProductTransferRepository.FindById(id);
        }

        public ProductTransferLineItem FindLineItemById(int Id)
        {
            return _unitOfWork.ProductTransferLineItemRepository.FindById(Id);
        }

        public IEnumerable<ProductTransfer> Get(Expression<Func<ProductTransfer, bool>> filter = null, Func<IQueryable<ProductTransfer>, IOrderedQueryable<ProductTransfer>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.ProductTransferRepository.Get(filter, orderBy, includeProperties);
        }

        public List<ProductTransfer> GetAllProductTransfer()
        {
            return _unitOfWork.ProductTransferRepository.GetAll();
        }

        public bool IsAvailable(string productId, int branchId, decimal quantity)
        {
            var stock = _unitOfWork.StockRepository.FindBy(x => x.BranchId == branchId
                                                                && x.ProductId == productId).SingleOrDefault();
            if (stock == null) return false;

            if (quantity > stock.Quantity)
                return false;
            else
                return true;
        }
    }
}
