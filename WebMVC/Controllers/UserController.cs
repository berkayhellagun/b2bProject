using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;
using WebMVC.Models.Cons;

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
            return RedirectToAction("Users", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserModel user)
        {
            var newUser = UpdateExtension(user);
            var result = await _request.PutAsync("api/Users/update", newUser);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("Users", "Admin");
        }

        [HttpGet]
        public IActionResult Update()
        {
            var user = GetUser();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(UserModel user)
        {
            var newUser = UpdateExtension(user);
            var result = await _request.PutAsync("api/Users/update", newUser);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("Profile", "User");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var email = RouteData.Values["id"];
            var url = string.Format("api/Users/getbyemail?email=" + email);
            var apiObject = _request.GetAsync(url).Result;
            var jsonObject = JsonConvert.DeserializeObject<UserModel>(apiObject);
            return View(jsonObject);
        }

        public UserModel? GetUser()
        {
            var userId = RouteData.Values["id"];
            var url = string.Format("api/Users/getbyid?id=" + userId);
            var apiObject = _request.GetAsync(url).Result;
            var jsonObject = JsonConvert.DeserializeObject<UserModel>(apiObject);
            return jsonObject;
        }

        private UserModel UpdateExtension(UserModel user)
        {
            var url = string.Format("api/Users/getbyemail?email=" + user.Email);
            var apiObject = _request.GetAsync(url).Result;
            var jsonObject = JsonConvert.DeserializeObject<UserModel>(apiObject);
            user.PasswordHash = jsonObject.PasswordHash;
            user.PasswordSalt = jsonObject.PasswordSalt;
            user.Status = jsonObject.Status;
            user.UserImage = jsonObject.UserImage;
            return user;
        }
    }
}
