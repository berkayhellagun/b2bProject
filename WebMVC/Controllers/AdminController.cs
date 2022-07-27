using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Personel()
        {
            return View();
        }
    }
}
