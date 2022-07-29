using Microsoft.AspNetCore.Mvc;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class CategoryContoller : Controller
    {
        IRequest _request;

        public CategoryContoller(IRequest request)
        {
            _request = request;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryModel category)
        {
            return RedirectToAction("/Admin/Categories");
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        public async Task<IActionResult> Delete()
        {
            return View();
        }
    }
}
