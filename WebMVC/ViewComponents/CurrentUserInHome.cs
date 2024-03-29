﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models.Cons;

namespace WebMVC.ViewComponents
{
    public class CurrentUserInHome : ViewComponent
    {
        public CurrentUserInHome()
        {

        }

        public IViewComponentResult Invoke()
        {
            var userMail = HttpContext.Session.GetString(Constants.Email);
            var role = HttpContext.Request.Cookies[Constants.Role];
            if (userMail != null)
            {
                ViewBag.Email = userMail;
                ViewBag.Role = role;
                return View();
            }
            return View();
        }
    }
}
