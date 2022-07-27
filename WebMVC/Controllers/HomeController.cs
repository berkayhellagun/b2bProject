using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
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
        public async Task<IActionResult> Index()
        {
            HomeViewModel homeView = new HomeViewModel();
            var apiProduct = await _request.GetAsync("api/Products/getall");
            var apiCategory = await _request.GetAsync("api/Categories/getall");
            var apiSupplier = await _request.GetAsync("api/Suppliers/getall");

            if (apiProduct == Constants.Exception && apiCategory == Constants.Exception)
            {
                //exception
                return View();
            }

            homeView.Product = JsonConvert.DeserializeObject<List<ProductModel>>(apiProduct);
            homeView.Category = JsonConvert.DeserializeObject<List<CategoryModel>>(apiCategory);
            homeView.Supplier = JsonConvert.DeserializeObject<List<SupplierModel>>(apiSupplier);

            return View(homeView);
        }
    }
}