using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class AdminController : Controller
    {
        IRequest _request;

        public AdminController(IRequest request)
        {
            _request = request;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Users()
        {
            var apiObj = _request.GetAsync("api/Users/getall").Result;
            var jsonObj = JsonConvert.DeserializeObject<List<UserModel>>(apiObj);
            return View(jsonObj);
        }

        [HttpGet]
        public IActionResult Products()
        {
            var apiObj = _request.GetAsync("api/Products/getall").Result;
            var jsonObj = JsonConvert.DeserializeObject<List<ProductModel>>(apiObj);
            return View(jsonObj);
        }

        [HttpGet]
        public IActionResult Suppliers()
        {
            var apiObj = _request.GetAsync("api/Supplier/getall").Result;
            var jsonObj = JsonConvert.DeserializeObject<List<SupplierModel>>(apiObj);
            return View(jsonObj);
        }

        [HttpGet]
        public IActionResult Categories()
        {
            var apiObj = _request.GetAsync("api/Categories/getall").Result;
            var jsonObj = JsonConvert.DeserializeObject<List<CategoryModel>>(apiObj);
            return View(jsonObj);
        }
    }
}
