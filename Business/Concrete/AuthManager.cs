using Business.Abstract;
using Core.Entities.Concrete;
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
        public AuthManager(IUserService userService)
        {
            _userService = userService;
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
            var result = await VerifyPassword(userForLoginDto);
            return result
                ? new SuccessDataResult<User>("login")
                : new ErrorDataResult<User>("pls try again.");
        }

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            var user = RegisterModule(userForRegisterDto, password);
            if (user == null)
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
            if (conclusion)
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

        private async Task<bool> CheckEmail(string email)
        {
            var user = await _userService.AsyncGetByMail(email);
            return user != null
                ? false
                : true;
        }

        private async Task<bool> VerifyPassword(UserForLoginDto userForLoginDto)
        {
            var userCheck = await _userService.AsyncGetByMail(userForLoginDto.Email);
            if (!userCheck.Success)
            {
                // user not exist
                return false;
            }
            var result = HashHelper.VerifyPassword(userForLoginDto.Password,
                userCheck.Data.PasswordHash, userCheck.Data.PasswordSalt);

            return result;
        }
    }
}
