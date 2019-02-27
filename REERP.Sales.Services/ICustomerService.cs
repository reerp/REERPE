using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using REERP.Models;

namespace REERP.Sales.Services
{
    public interface ICustomerService : IDisposable
    {
        bool AddCustomer(Customer customer);
        bool DeleteCustomer(Customer customer);
        bool DeleteById(int id);
        bool EditCustomer(Customer customer);
        Customer FindById(int id);
        List<Customer> GetAllCustomers();
        List<Customer> FindBy(Expression<Func<Customer, bool>> predicate);
        IEnumerable<Customer> Get(
                   Expression<Func<Customer, bool>> filter = null,
                   Func<IQueryable<Customer>, IOrderedQueryable<Customer>> orderBy = null,
                   string includeProperties = "");
    }
}
