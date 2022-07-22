using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IRequest _request;

        public AuthController(IRequest request)
        {
            _request = request;
        }

        [HttpPost]
        public IActionResult Login(UserForLoginDtoModel userForLoginDto)
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
                if (apiResultJson != null && apiResultJson.ExpirationTime >= DateTime.Now)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                return View();
            }
            return View();
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
    }
}
