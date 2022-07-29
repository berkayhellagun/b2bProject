using Microsoft.AspNetCore.Mvc;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class ProductController : Controller
    {
        IRequest _request;

        public ProductController(IRequest request)
        {
            _request = request;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserModel user)
        {
            var result = await _request.PostAsync("api/Users/add", user);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("/Admin/Users");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
