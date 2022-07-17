using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userLoginStat = _authService.Login(userForLoginDto);
            if (userLoginStat.Data == null)
            {
                return BadRequest(userLoginStat.Message);
            }
            var token = _authService.CreateAccessToken(userLoginStat.Data);
            return token.Result.Success
                ? Ok(token.Result.Data)
                : BadRequest(token.Result.Message);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            var registerStat = await _authService.Register(userForRegisterDto, password);
            if (!registerStat.Success)
            {
                return BadRequest(registerStat.Success);
            }
            var token = await _authService.CreateAccessToken(registerStat.Data);
            return token.Success
                ? Ok(token.Data)
                : BadRequest(token.Message);
        }
    }
}
