using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;
using System.Web;
using REERP.Models;
using REERP.DAL.Repository;
using System.Text;
using log4net;

namespace REERP.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly REERPContext _context;
        public UnitOfWork()
        {
            this._context = new REERPContext();
        }

        public UnitOfWork(ILog log)
        {
            this._context = new REERPContext();
            _log = log;
        }

        public Database Database { get { return _context.Database; } }

        public IGenericRepository<Address> addressRepository;
        public IGenericRepository<Address> AddressRepository
        {
            get { return this.addressRepository ?? (this.addressRepository = new GenericRepository<Address>(_context)); }
        }
        public IGenericRepository<SalesInvoice> salesInvoiceRepository;
        public IGenericRepository<SalesInvoice> SalesInvoiceRepository
        {
            get { return this.salesInvoiceRepository ?? (this.salesInvoiceRepository = new GenericRepository<SalesInvoice>(_context)); }
        }

        public IGenericRepository<SalesLineItem> salesLineItemRepository;
        public IGenericRepository<SalesLineItem> SalesLineItemRepository
        {
            get { return this.salesLineItemRepository ?? (this.salesLineItemRepository = new GenericRepository<SalesLineItem>(_context)); }
        }

        public IGenericRepository<Category> categoryRepository;
        public IGenericRepository<Category> CategoryRepository
        {
            get { return this.categoryRepository ?? (this.categoryRepository = new GenericRepository<Category>(_context)); }
        }

        public IGenericRepository<Branch> branchRepository;
        public IGenericRepository<Branch> BranchRepository
        {
            get { return this.branchRepository ?? (this.branchRepository = new GenericRepository<Branch>(_context)); }
        }

        public IGenericRepository<Productc> productcRepository;
        public IGenericRepository<Productc> ProductcRepository
        {
            get { return this.productcRepository ?? (this.productcRepository = new GenericRepository<Productc>(_context)); }
        }

        public IGenericRepository<Customer> customerRepository;
        public IGenericRepository<Customer> CustomerRepository
        {
            get { return this.customerRepository ?? (this.customerRepository = new GenericRepository<Customer>(_context)); }
        }

        public IGenericRepository<Stock> stockRepository;
        public IGenericRepository<Stock> StockRepository
        {
            get { return this.stockRepository ?? (this.stockRepository = new GenericRepository<Stock>(_context)); }
        }

        public IGenericRepository<ProductReceive> productReceiveInvoiceRepository;
        public IGenericRepository<ProductReceive> ProductReceiveRepository
        {
            get { return this.productReceiveInvoiceRepository ?? (this.productReceiveInvoiceRepository = new GenericRepository<ProductReceive>(_context)); }
        }

        public IGenericRepository<ProductReceiveLineItem> productReceiveLineItemRepository;
        public IGenericRepository<ProductReceiveLineItem> ProductReceiveLineItemRepository
        {
            get { return this.productReceiveLineItemRepository ?? (this.productReceiveLineItemRepository = new GenericRepository<ProductReceiveLineItem>(_context)); }
        }

        public IGenericRepository<ProductTransfer> productTransferRepository;
        public IGenericRepository<ProductTransfer> ProductTransferRepository
        {
            get { return this.productTransferRepository ?? (this.productTransferRepository = new GenericRepository<ProductTransfer>(_context)); }
        }

        public IGenericRepository<ProductTransferLineItem> productTransferLineItemRepository;
        public IGenericRepository<ProductTransferLineItem> ProductTransferLineItemRepository
        {
            get { return this.productTransferLineItemRepository ?? (this.productTransferLineItemRepository = new GenericRepository<ProductTransferLineItem>(_context)); }
        }


        private readonly ILog _log;
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                for (var eCurrent = e; eCurrent != null; eCurrent = (DbEntityValidationException)eCurrent.InnerException)
                {
                    foreach (var eve in eCurrent.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);

                        StringBuilder errorMsg = new StringBuilder(String.Empty);
                        var s = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        errorMsg.Append(s);

                        foreach (var ve in eve.ValidationErrors)
                        {
                            errorMsg.Append(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                            _log.Error(errorMsg, eCurrent.GetBaseException());
                        }
                    }
                }
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
