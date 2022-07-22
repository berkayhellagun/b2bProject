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
            // burada data da kayıp olabilir result her türlü gelebilir
                var result = _request.Post("api/Auth/login", userForLoginDto);
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
                return View(result);
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
            //token tutulacak ve işleme sokulacak
            if (result != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(result);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}
