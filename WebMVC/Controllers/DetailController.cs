using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class DetailController : Controller
    {
        public IActionResult Index()
        {
            var productId = RouteData.Values["id"];
            ViewBag.id = productId;
            return View();
        }
    }
}
