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
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var result = _request.Post("api/Auth/login", userForLoginDto);
            return View(result);
        }
    }
}
