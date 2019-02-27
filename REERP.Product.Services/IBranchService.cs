using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using REERP.Models;

namespace REERP.Product.Services
{
    public interface IBranchService:IDisposable
    {
        bool AddBranch(Branch branch);
        bool DeleteBranch(Branch branch);
        bool DeleteById(int id);
        bool EditBranch(Branch branch);
        Branch FindById(int id);
        List<Branch> GetAllBranches();
        List<Branch> FindBy(Expression<Func<Branch, bool>> predicate);
        IEnumerable<Branch> Get(
                   Expression<Func<Branch, bool>> filter = null,
                   Func<IQueryable<Branch>, IOrderedQueryable<Branch>> orderBy = null,
                   string includeProperties = "");

    }
}
