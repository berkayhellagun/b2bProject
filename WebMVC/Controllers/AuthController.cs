using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebMVC.API;
using WebMVC.Models;
using WebMVC.Models.Cons;

namespace WebMVC.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IRequest _request;
        public AuthController(IRequest request)
        {
            _request = request;
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDtoModel userForLoginDto)
        {
            var result = _request.Post("api/Auth/login", userForLoginDto);
            if (result == Constants.Exception)
            {
                // exception
                return View();
            }
            try
            {
                var apiResultJson = JsonConvert.DeserializeObject<TokenModel>(result);
                Response.Cookies.Append(Constants.XAccessToken, apiResultJson.Token, new CookieOptions
                {
                    //object initialization
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,

                });
                if (apiResultJson == null && apiResultJson.ExpirationTime <= DateTime.Now)
                {
                    return View();
                }
                JwtSecurityToken token = new JwtSecurityToken(jwtEncodedString: apiResultJson.Token);
                var role = token.Claims.First(claims => claims.Type.Contains("role")).Value;
                HttpContext.Session.SetString(Constants.Email, userForLoginDto.Email);
                HttpContext.Session.SetString(Constants.Role, role);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserForRegisterDtoModel userForRegisterDtoModel)
        {
            var result = _request.Post("api/Auth/register", userForRegisterDtoModel);
            if (result == Constants.Exception || result == null)
            {
                //exception
                return View();
            }
            if (result != null)
            {
                //success
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(Constants.Email);
            HttpContext.Session.Remove(Constants.Role);
            HttpContext.Response.Cookies.Delete(Constants.XAccessToken);
            return RedirectToAction("Index", "Home");
        }
    }
}
