﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<IActionResult> Add(ProductModel product)
        {
            var result = await _request.PostAsync("api/Products/add", product);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("/Admin/Products");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductModel product)
        {
            var result = await _request.PutAsync("api/Products/update", product);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("/Admin/Products");
        }
        [HttpGet]
        public IActionResult Update()
        {
            var productId = RouteData.Values["id"];
            var url = string.Format("api/Products/getbyid?id=" + productId);
            var apiObject = _request.GetAsync(url).Result;
            var jsonObject = JsonConvert.DeserializeObject<ProductModel>(apiObject);
            return View(jsonObject);
        }

        public async Task<IActionResult> Remove()
        {
            var productId = RouteData.Values["id"];
            var url = string.Format("api/Users/removebyid?id=" + productId);
            var result = await _request.DeleteAsync(url);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("/Admin/Products");
        }
    }
}
