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
            var operationList = await _userService.AsyncGetClaim(user);
            var result = await _tokenHelper.AsyncCreateToken(user, operationList);
            return result != null
                ? new SuccessDataResult<AccessToken>(result)
                : new ErrorDataResult<AccessToken>("Not created.");
        }

        public async Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto)
        {
            var process = BusinessRules.Process(VerifyPassword(userForLoginDto).Result);
            return process.Success
                ? new SuccessDataResult<User>("success")
                : new ErrorDataResult<User>("pls try again.");
        }

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            var user = RegisterModule(userForRegisterDto, password);
            if (user.Result == null)
            {
                new ErrorDataResult<User>();
            }
            var result = await _userService.AsyncAdd(user.Result);
            return result.Success
                ? new SuccessDataResult<User>(user.Result)
                : new ErrorDataResult<User>();
        }

        private async Task<User> RegisterModule(UserForRegisterDto userDto, string password)
        {
            var conclusion = await CheckEmail(userDto.Email);
            if (conclusion.Data != null)
            {
                // user exist
                return null;
            }

            byte[] passwordHash, passwordSalt;
            //create hash and salt
            HashHelper.CreateHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            return user;
        }

        private async Task<IDataResult<User>> CheckEmail(string email)
        {
            var user = await _userService.AsyncGetByMail(email);
            return user.Data == null
                ? new ErrorDataResult<User>()
                : new SuccessDataResult<User>(user.Data);
        }

        private async Task<IResult> VerifyPassword(UserForLoginDto userForLoginDto)
        {
            var userCheck = await CheckEmail(userForLoginDto.Email);
            if (userCheck.Data == null)
            {
                // user not exist
                return null;
            }
            if (!userCheck.Data.Status)
            {
                // user status false
                return null;
            }
            var result = HashHelper.VerifyPassword(userForLoginDto.Password,
                userCheck.Data.PasswordHash, userCheck.Data.PasswordSalt);

            return new SuccessResult();
        }
    }
}
