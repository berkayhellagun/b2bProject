using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

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
        public IActionResult Index()
        {   
            /*var apiObject = _request.GetAsync("api/Products/getall");
            var jsonObject = JsonConvert.DeserializeObject<List<ProductModel>>(apiObject.Result);
            return View(jsonObject);*/

            return View();
        }
    }
}