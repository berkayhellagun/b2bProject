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
        IUserService _userService;
        ITokenHelper _tokenHelper;
        IUserOperationClaimService _userOperation;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserOperationClaimService userOperation)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userOperation = userOperation;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var operationList = _userService.GetClaim(user);
            var result = await _tokenHelper.AsyncCreateToken(user, operationList.Data);
            return result != null
                ? new SuccessDataResult<AccessToken>(result)
                : new ErrorDataResult<AccessToken>("Not created.");
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var result = VerifyPassword(userForLoginDto);
            return result.Success
                ? new SuccessDataResult<User>(result.Data)
                : new ErrorDataResult<User>("Check your password!");
        }

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto)
        {
            var user = RegisterModule(userForRegisterDto);
            if (user.Data == null)
            {
                return new ErrorDataResult<User>(user.Message);
            }
            await _userService.AsyncAdd(user.Data);
            var dbuser = await _userService.AsyncGetByMail(user.Data.Email);
            var operation = new UserOperationClaim { UserId = dbuser.Data.Id, OperationId = 2 }; // 2 is default user
            var result = await _userOperation.AsyncAdd(operation);
            return result.Success
                ? new SuccessDataResult<User>(user.Data)
                : new ErrorDataResult<User>();
        }

        private IDataResult<User> RegisterModule(UserForRegisterDto userDto)
        {
            var conclusion = CheckEmail(userDto.Email);
            if (conclusion.Data != null)
            {
                // user exist
                return new ErrorDataResult<User>("This email cannot be used!");
            }

            byte[] passwordHash, passwordSalt;
            //create hash and salt
            HashHelper.CreateHash(userDto.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            return new SuccessDataResult<User>(user);
        }

        private IDataResult<User> CheckEmail(string email)
        {
            var user = _userService.AsyncGetByMail(email);
            return user == null
                ? new ErrorDataResult<User>()
                : new SuccessDataResult<User>(user.Result.Data);
        }

        private IDataResult<User> VerifyPassword(UserForLoginDto userForLoginDto)
        {
            var userCheck = CheckEmail(userForLoginDto.Email);
            if (userCheck.Data == null)
            {
                // user not exist
                return new ErrorDataResult<User>("E-mail not found!");
            }
            if (!userCheck.Data.Status)
            {
                // user status false
                return new ErrorDataResult<User>("This user status is inactive!");
            }
            var result = HashHelper.VerifyPassword(userForLoginDto.Password,
                userCheck.Data.PasswordHash, userCheck.Data.PasswordSalt);

            return result
                ? new SuccessDataResult<User>(userCheck.Data)
                : new ErrorDataResult<User>();
        }
    }
}
