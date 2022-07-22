using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Authentication;
using WebMVC.API;
using WebMVC.Models;
using WebMVC.Models.Response;

namespace WebMVC.Controllers
{
    public class UserController : Controller
    {

        private readonly IRequest _request;

        public UserController(IRequest request)
        {
            _request = request;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var ApiObject = _request.Get("api/Users/getall");
            var ApiResult = JsonConvert.DeserializeObject<List<UserModel>>(ApiObject);
            return View(ApiResult);
        }
    }
}
