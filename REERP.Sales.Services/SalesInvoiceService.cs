using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using REERP.DAL.UnitOfWork;
using REERP.Models;

namespace REERP.Sales.Services
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalesInvoiceService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool AddSalesInvoice(SalesInvoice salesInvoice)
        {
            _unitOfWork.SalesInvoiceRepository.Add(salesInvoice);
            if(salesInvoice.SalesLineItems!=null)
            {
                foreach(var item in salesInvoice.SalesLineItems)
                {
                    SubtractLineItemStock(item, salesInvoice.BranchId);
                }
            }
            _unitOfWork.Save();
            return true;
        }

        public bool AddInvoiceLineItem(SalesInvoice salesInvoice, SalesLineItem item)
        {
            _unitOfWork.SalesInvoiceRepository.Edit(salesInvoice);
            SubtractLineItemStock(item, salesInvoice.BranchId);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteInvoiceLineItem(SalesInvoice salesInvoice, SalesLineItem item)
        {
            _unitOfWork.SalesInvoiceRepository.Edit(salesInvoice);
            AddLineItemStock(item, salesInvoice.BranchId);
            _unitOfWork.Save();
            return true;
        }

        void AddLineItemStock(SalesLineItem item, int branchId)
        {
            var stock = _unitOfWork.StockRepository.FindBy(x => x.BranchId == branchId
                                                                && x.ProductId == item.ProductId).SingleOrDefault();

                stock.Quantity = stock.Quantity + item.Quantity;
                _unitOfWork.StockRepository.Edit(stock);

        }

        void SubtractLineItemStock(SalesLineItem item, int branchId)
        {
            var stock = _unitOfWork.StockRepository.FindBy(x => x.BranchId == branchId
                                                                && x.ProductId == item.ProductId).SingleOrDefault();
            stock.Quantity = stock.Quantity - item.Quantity;
            _unitOfWork.StockRepository.Edit(stock);

        }

        public bool DeleteSalesInvoice(SalesInvoice salesInvoice)
        {
            if (salesInvoice == null) return false;
            _unitOfWork.SalesInvoiceRepository.Delete(salesInvoice);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.SalesInvoiceRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.SalesInvoiceRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteLineItemById(int id)
        {
            var entity = _unitOfWork.SalesLineItemRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.SalesLineItemRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditSalesInvoice(SalesInvoice salesInvoice)
        {
            _unitOfWork.SalesInvoiceRepository.Edit(salesInvoice);
            _unitOfWork.Save();
            return true;
        }

        public SalesInvoice FindById(int id)
        {
            return _unitOfWork.SalesInvoiceRepository.FindById(id);
        }

        public SalesLineItem FindLineItemById(int id)
        {
            return _unitOfWork.SalesLineItemRepository.FindById(id);
        }

        public List<SalesInvoice> GetAllSalesInvoices()
        {
            return _unitOfWork.SalesInvoiceRepository.GetAll();
        }

        public List<SalesInvoice> FindBy(Expression<Func<SalesInvoice, bool>> predicate)
        {
            return _unitOfWork.SalesInvoiceRepository.FindBy(predicate);
        }

        public IEnumerable<SalesInvoice> Get(Expression<Func<SalesInvoice, bool>> filter = null, 
            Func<IQueryable<SalesInvoice>, IOrderedQueryable<SalesInvoice>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.SalesInvoiceRepository.Get(filter, orderBy, includeProperties);
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

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
