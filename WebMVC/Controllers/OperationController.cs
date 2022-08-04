using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;
using WebMVC.Models.AddModel;
using WebMVC.Models.Cons;

namespace WebMVC.Controllers
{
    public class OperationController : Controller
    {
        public readonly IRequest _request;

        public OperationController(IRequest request)
        {
            _request = request;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(OperationClaim operationClaim)
        {
            var url = "api/OperationClaims/add";
            var apiObject = _request.PostAsync(url, operationClaim);
            return RedirectToAction("OperationClaims","Admin");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(OperationClaimModel operationClaimModel)
        {
            var result = await _request.PutAsync("api/OperationClaims/update", operationClaimModel);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("OperationClaims", "Admin");
        }
        [HttpGet]
        public IActionResult Update()
        {
            var id = RouteData.Values["id"];
            var url = string.Format("api/OperationClaims/getbyid?id=" + id);
            var apiObject = _request.GetAsync(url).Result;
            var jsonObj = JsonConvert.DeserializeObject<OperationClaimModel>(apiObject);
            return View(jsonObj);
        }

        public async Task<IActionResult> Remove()
        {
            var id = RouteData.Values["id"];
            var url = string.Format("api/OperationClaims/removebyid?id=" + id);
            var result = await _request.DeleteAsync(url);
            if (result == Constants.Exception)
            {
                return RedirectToAction("Exception", "Error");
            }
            return RedirectToAction("OperationClaims", "Admin");
        }
    }
}
