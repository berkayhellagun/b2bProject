using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;
using WebMVC.Models.AddModel;

namespace WebMVC.Controllers
{
    public class SupplierController : Controller
    {
        IRequest _request;

        public SupplierController(IRequest request)
        {
            _request = request;
        }

        public IActionResult Index()
        {
            var apiObject = _request.GetAsync("api/Suppliers/getall").Result;
            var jsonObject = JsonConvert.DeserializeObject<List<SupplierModel>>(apiObject);
            return View(jsonObject);
        }

        public IActionResult SupplierDetail()
        {
            var supplierId = RouteData.Values["id"];
            ViewBag.id = supplierId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Supplier supplier)
        {
            var result = await _request.PostAsync("api/Suppliers/add", supplier);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("/Admin/Suppliers");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(SupplierModel supplier)
        {
            var result = await _request.PutAsync("api/Suppliers/update", supplier);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("/Admin/Suppliers");
        }
        [HttpGet]
        public IActionResult Update()
        {
            var SupplierId = RouteData.Values["id"];
            ViewBag.id = SupplierId;
            var url = string.Format("api/Suppliers/getbyid?id=" + SupplierId);
            var apiObject = _request.GetAsync(url).Result;
            var jsonObject = JsonConvert.DeserializeObject<SupplierModel>(apiObject);
            return View(jsonObject);
        }

        public async Task<IActionResult> Remove()
        {
            var SupplierId = RouteData.Values["id"];
            var url = string.Format("api/Suppliers/removebyid?id=" + SupplierId);
            var result = await _request.DeleteAsync(url);
            if (result == Constants.Exception)
            {
                return RedirectToAction("/Admin/Suppliers");
            }
            return RedirectToAction("/Admin/Suppliers");
        }
    }
}
