using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoencoPOS.DAL.UnitOfWork;
using MoencoPOS.Models;

namespace MoencoPOS.Services
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
            _unitOfWork.Save();
            return true;
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

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
