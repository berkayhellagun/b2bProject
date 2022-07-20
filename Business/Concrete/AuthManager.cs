using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Core.Utilities.Security.JWT.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
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

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto)
        {
            var user = RegisterModule(userForRegisterDto);
            if (user.Data == null)
            {
                return new ErrorDataResult<User>(user.Message);
            }
            var result = await _userService.AsyncAdd(user.Data);
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
