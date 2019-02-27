using REERP.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using REERP.Models;

namespace REERP.Sales.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool AddCustomer(Customer customer)
        {
            _unitOfWork.CustomerRepository.Add(customer);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.CustomerRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.CustomerRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteCustomer(Customer customer)
        {
            if (customer == null) return false;
            _unitOfWork.CustomerRepository.Delete(customer);
            _unitOfWork.Save();
            return true;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public bool EditCustomer(Customer customer)
        {
            _unitOfWork.CustomerRepository.Edit(customer);
            _unitOfWork.Save();
            return true;
        }

        public List<Customer> FindBy(Expression<Func<Customer, bool>> predicate)
        {
            return _unitOfWork.CustomerRepository.FindBy(predicate);
        }

        public Customer FindById(int id)
        {
            return _unitOfWork.CustomerRepository.FindById(id);
        }

        public IEnumerable<Customer> Get(Expression<Func<Customer, bool>> filter = null, Func<IQueryable<Customer>, IOrderedQueryable<Customer>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.CustomerRepository.Get(filter, orderBy, includeProperties);
        }

        public List<Customer> GetAllCustomers()
        {
            return _unitOfWork.CustomerRepository.GetAll();
        }
        
        
    }
}
