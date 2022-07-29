﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

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
