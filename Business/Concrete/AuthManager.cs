using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Core.Utilities.Security.JWT.Abstract;
using Entities.DTOs;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IPersonService _userService;
        ITokenHelper _tokenHelper;
        IUserOperationClaimService _userOperation;
        public AuthManager(IPersonService userService, ITokenHelper tokenHelper, IUserOperationClaimService userOperation)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userOperation = userOperation;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(Person user)
        {
            var operationList = _userService.GetClaim(user);
            var result = await _tokenHelper.AsyncCreateToken(user, operationList.Data);
            return result != null
                ? new SuccessDataResult<AccessToken>(result)
                : new ErrorDataResult<AccessToken>("Not created.");
        }

        public IDataResult<Person> Login(UserForLoginDto userForLoginDto)
        {
            var result = VerifyPassword(userForLoginDto);
            return result.Success
                ? new SuccessDataResult<Person>(result.Data)
                : new ErrorDataResult<Person>("Check your password!");
        }

        public async Task<IDataResult<Person>> Register(UserForRegisterDto userForRegisterDto)
        {
            var user = RegisterModule(userForRegisterDto);
            if (user.Data == null)
            {
                return new ErrorDataResult<Person>(user.Message);
            }
            await _userService.AsyncAdd(user.Data);
            var dbuser = await _userService.AsyncGetByMail(user.Data.eMail);
            var operation = new UserOperationClaim { UserId = dbuser.Data.Id, OperationId = 2 }; // 2 is default user
            var result = await _userOperation.AsyncAdd(operation);
            return result.Success
                ? new SuccessDataResult<Person>(user.Data)
                : new ErrorDataResult<Person>();
        }

        private IDataResult<Person> RegisterModule(UserForRegisterDto userDto)
        {
            var conclusion = CheckEmail(userDto.Email);
            if (conclusion.Data != null)
            {
                // user exist
                return new ErrorDataResult<Person>("This email cannot be used!");
            }

            byte[] passwordHash, passwordSalt;
            //create hash and salt
            HashHelper.CreateHash(userDto.Password, out passwordHash, out passwordSalt);

            var user = new Person
            {
                eMail = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = userDto.Password,
                Status = 1
            };
            return new SuccessDataResult<Person>(user);
        }

        private IDataResult<Person> CheckEmail(string email)
        {
            var user = _userService.AsyncGetByMail(email);
            return user == null
                ? new ErrorDataResult<Person>()
                : new SuccessDataResult<Person>(user.Result.Data);
        }

        private IDataResult<Person> VerifyPassword(UserForLoginDto userForLoginDto)
        {
            var userCheck = CheckEmail(userForLoginDto.Email);
            if (userCheck.Data == null)
            {
                // user not exist
                return new ErrorDataResult<Person>("E-mail not found!");
            }
            if (userCheck.Data.Status == 0)
            {
                // user status false
                return new ErrorDataResult<Person>("This user status is inactive!");
            }
            var result = HashHelper.VerifyPassword(userForLoginDto.Password,userCheck.Data.Password);

            return result
                ? new SuccessDataResult<Person>(userCheck.Data)
                : new ErrorDataResult<Person>();
        }
    }
}
