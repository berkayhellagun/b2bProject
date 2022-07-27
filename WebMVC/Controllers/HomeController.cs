﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            var apiObject = _request.Get("api/Products/getall");
            var jsonObject = JsonConvert.DeserializeObject<List<ProductModel>>(apiObject);
            return View(jsonObject);
        }
    }
}