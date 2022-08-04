using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.CustomAttribute;
using WebMVC.Models;
using WebMVC.Models.Cons;
using WebMVC.Models.DetailModel;

namespace WebMVC.Controllers
{
    [MyAuthorizeAttribute(Roles ="admin")]
    public class AdminController : Controller
    {
        IRequest _request;

        public AdminController(IRequest request)
        {
            _request = request;
        }

        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString(Constants.Email);
            ViewBag.Email = email;
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
            var apiObj = _request.GetAsync("api/Suppliers/getall").Result;
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
        [HttpGet]
        public IActionResult OperationClaims()
        {
            var apiObj = _request.GetAsync("api/OperationClaims/getall").Result;
            var jsonObj = JsonConvert.DeserializeObject<List<OperationClaimModel>>(apiObj);
            return View(jsonObj);
        }

        [HttpGet]
        public IActionResult UserOperationClaims()
        {
            var apiObj = _request.GetAsync("api/UserOperationClaims/getdetailuseroperation").Result;
            var jsonObj = JsonConvert.DeserializeObject<List<UserOperationClaimModel>>(apiObj);
            return View(jsonObj);
        }
    }
}
