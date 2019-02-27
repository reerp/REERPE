using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using REERP.Models;
using REERP.DAL.UnitOfWork;

namespace REERP.Product.Services
{
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BranchService(IUnitOfWork unitofWork)
        {
            this._unitOfWork = unitofWork;
        }


        public bool AddBranch(Branch branch)
        {
            _unitOfWork.BranchRepository.Add(branch);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteBranch(Branch branch)
        {
            if (branch == null) return false;
            _unitOfWork.BranchRepository.Delete(branch);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.BranchRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.BranchRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditBranch(Branch branch)
        {
            _unitOfWork.BranchRepository.Edit(branch);
            _unitOfWork.Save();
            return true;
        }

        public List<Branch> FindBy(Expression<Func<Branch, bool>> predicate)
        {
            return _unitOfWork.BranchRepository.FindBy(predicate);
        }

        public Branch FindById(int id)
        {
            return _unitOfWork.BranchRepository.FindById(id);
        }

        public IEnumerable<Branch> Get(Expression<Func<Branch, bool>> filter = null, Func<IQueryable<Branch>, IOrderedQueryable<Branch>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.BranchRepository.Get(filter, orderBy, includeProperties);
        }

        public List<Branch> GetAllBranches()
        {
            return _unitOfWork.BranchRepository.GetAll();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }


    }
}
