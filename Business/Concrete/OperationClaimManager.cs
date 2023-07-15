using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        public async Task<IResult> AsyncAdd(OperationClaim t)
        {
            var result = await _operationClaimDal.AsyncAddDB(t);
            return result
                ? new SuccessResult()
                : new ErrorResult();
        }

        public async Task<IDataResult<List<OperationClaim>>> AsyncGetAll()
        {
            var result = await _operationClaimDal.AsyncGetAllDB();
            return result != null
                ? new SuccessDataResult<List<OperationClaim>>(result)
                : new ErrorDataResult<List<OperationClaim>>();
        }

        public async Task<IDataResult<OperationClaim>> AsyncGetById(int id)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>
            {
                { "Id", id },
            };
            var result = await _operationClaimDal.AsyncGetDB(filter);
            return result != null
                ? new SuccessDataResult<OperationClaim>(result)
                : new ErrorDataResult<OperationClaim>();
        }

        public async Task<IResult> AsyncRemove(OperationClaim t)
        {
            var result = await _operationClaimDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult()
                : new ErrorResult();
        }

        public async Task<IResult> AsyncUpdate(OperationClaim t)
        {
            var result = await _operationClaimDal.AsyncUpdateDB(t);
            return result
                ? new SuccessResult()
                : new ErrorResult();
        }

        public async Task<IResult> RemoveById(int id)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>
            {
                { "Id", id },
            };
            var operation = await _operationClaimDal.AsyncGetDB(filter);
            var result = await AsyncRemove(operation);
            return result.Success
                ? new SuccessResult()
                : new ErrorResult();
        }
    }
}
