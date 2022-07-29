using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class CategoryController : Controller
    {
        IRequest _request;

        public CategoryController(IRequest request)
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
            var result = await _request.PutAsync("api/Categories/update", category);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("/Admin/Categories");
        }
        [HttpGet]
        public IActionResult Update()
        {
            var categoryId = RouteData.Values["id"];
            var url = string.Format("api/Categories/getbyid?id=" + categoryId);
            var apiObject = _request.GetAsync(url).Result;
            var jsonObject = JsonConvert.DeserializeObject<CategoryModel>(apiObject);
            return View(jsonObject);
        }

        public async Task<IActionResult> Remove()
        {
            var categoryId = RouteData.Values["id"];
            var url = string.Format("api/Users/removebyid?id=" + categoryId);
            var result = await _request.DeleteAsync(url);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("/Admin/Users");
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryModel category)
        {
            var result = await _request.PostAsync("api/Categories/add", category);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("/Admin/Categories");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        
    }
}
