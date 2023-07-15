using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using WebMVC.API;
using WebMVC.Models;
using WebMVC.Models.Cons;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        IRequest _request;
        IHttpContextAccessor _httpContextAccessor;

        public HomeController(IRequest request)
        {
            _request = request;
        }

        //[HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString(Constants.UserId);
            var apiObject = "";

            //if (string.IsNullOrEmpty(userId))
                apiObject = _request.Get("api/Products/getall");

            //else
            //    apiObject = _request.Get($"api/Products/getrecommendedproductsfororders?personId={userId}");
            
            var jsonObject = JsonConvert.DeserializeObject<List<ProductModel>>(apiObject);
            return View(jsonObject);
        }
        [AllowAnonymous]
        public IActionResult CategoryDetails()
        {
            //i need string convert to int
            var categoryId = RouteData.Values["id"];
            ViewBag.id = categoryId;
            return View();
        }
    }
}