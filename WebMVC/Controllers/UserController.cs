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
            if (ApiObject == Constants.Exception)
            {
                //exception
                return View();
            }
            var ApiResult = JsonConvert.DeserializeObject<List<UserModel>>(ApiObject);
            return View(ApiResult);
        }

        public async Task<IActionResult> Remove()
        {
            var userId = RouteData.Values["id"];
            var url = string.Format("api/Users/removebyid?id=" + userId);
            var result = await _request.DeleteAsync(url);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("/Admin/Users");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserModel user)
        {
            user.Status = true;
            var result = await _request.PutAsync("api/Users/update", user);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("/Admin/Users");
        }

        [HttpGet]
        public IActionResult Update()
        {
            var userId = RouteData.Values["id"];
            var url = string.Format("api/Users/getbyid?id=" + userId);
            var apiObject = _request.GetAsync(url).Result;
            var jsonObject = JsonConvert.DeserializeObject<UserModel>(apiObject);
            return View(jsonObject);
        }
    }
}
