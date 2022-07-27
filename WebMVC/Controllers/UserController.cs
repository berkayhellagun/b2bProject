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
        public async Task<IActionResult> Index()
        {
            var ApiObject = await _request.GetAsync("api/Users/getall");
            if (ApiObject == Constants.Exception )
            {
                //exception
                return View();
            }
            var ApiResult = JsonConvert.DeserializeObject<List<UserModel>>(ApiObject);
            return View(ApiResult);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var apiObject = await _request.DeleteAsync("api/Users/remove");
            if (apiObject == Constants.Exception)
            {
                return View();
            }
            var ApiResult = JsonConvert.DeserializeObject<List<UserModel>>(apiObject);
            return View(ApiResult);
        }
    }
}
