using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;
using WebMVC.Models.AddModel;
using WebMVC.Models.Cons;

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
            return RedirectToAction("Categories","Admin");
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
            var url = string.Format("api/Categories/removebyid?id=" + categoryId);
            var result = await _request.DeleteAsync(url);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("Categories","Admin");
        }
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            var result = await _request.PostAsync("api/Categories/add", category);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("Categories", "Admin");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        
    }
}
