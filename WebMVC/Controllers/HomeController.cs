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
        public HomeController(IRequest request)
        {
            _request = request;
        }

        //[HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var apiObject = _request.Get("api/Products/getall");
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