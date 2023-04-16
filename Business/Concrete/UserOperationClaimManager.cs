using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        public async Task<IResult> AsyncAdd(UserOperationClaim t)
        {
            var result = await _userOperationClaimDal.AsyncAddDB(t);
            return result
                ? new SuccessResult()
                : new ErrorResult();
        }

        public async Task<IDataResult<List<UserOperationClaim>>> AsyncGetAll()
        {
            var result = await _userOperationClaimDal.AsyncGetAllDB();
            return result != null
                ? new SuccessDataResult<List<UserOperationClaim>>(result)
                : new ErrorDataResult<List<UserOperationClaim>>();
        }

        public async Task<IDataResult<UserOperationClaim>> AsyncGetById(int id)
        {
            var result = await _userOperationClaimDal.AsyncGetDB(o => o.OperationId == id);
            return result != null
                ? new SuccessDataResult<UserOperationClaim>(result)
                : new ErrorDataResult<UserOperationClaim>();
        }

        public async Task<IResult> AsyncRemove(UserOperationClaim t)
        {
            var result = await _userOperationClaimDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult()
                : new ErrorResult();
        }

        public async Task<IResult> AsyncUpdate(UserOperationClaim t)
        {
            var result = await _userOperationClaimDal.AsyncUpdateDB(t);
            return result
                ? new SuccessResult()
                : new ErrorResult();
        }

        public IDataResult<List<UserOperationClaimDetail>> GetDetail()
        {
            var result = _userOperationClaimDal.GetDetailDB();
            return result != null
                ? new SuccessDataResult<List<UserOperationClaimDetail>>(result)
                : new ErrorDataResult<List<UserOperationClaimDetail>>();
        }

        public IDataResult<List<UserOperationClaimDetail>> GetDetailById(int userOperationId)
        {
            var result = _userOperationClaimDal.GetDetailByIdDB(userOperationId);
            return result != null
                ? new SuccessDataResult<List<UserOperationClaimDetail>>(result)
                : new ErrorDataResult<List<UserOperationClaimDetail>>();
        }

        public async Task<IResult> RemoveById(int id)
        {
            var userOperation = await _userOperationClaimDal.AsyncGetDB(o => o.UserOperationClaimId == id);
            var result = await AsyncRemove(userOperation);
            return result.Success
                ? new SuccessResult()
                : new ErrorResult();
        }
    }
}
