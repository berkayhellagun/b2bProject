using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

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
            if (userMail != null)
            {
                ViewBag.Email = userMail;
                return View();
            }
            return View();
        }
    }
}
