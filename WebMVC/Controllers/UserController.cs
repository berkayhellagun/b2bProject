using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

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
            var ApiResult = JsonConvert.DeserializeObject<List<User>>(ApiObject);
            return View(ApiResult);
        }
    }
}
