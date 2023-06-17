using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<Person>> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<Person> Login(UserForLoginDto userForLoginDto);
        Task<IDataResult<AccessToken>> CreateAccessToken(Person user);
    }
}