using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;
using WebMVC.Models.AddModel;

namespace WebMVC.Controllers
{
    public class UserOperationController : Controller
    {
        IRequest _request;

        public UserOperationController(IRequest request)
        {
            _request = request;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserOperationClaim UserOperationClaim)
        {
            var url = "api/UserOperationClaims/add";
            var apiObject = _request.PostAsync(url, UserOperationClaim);
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserOperationClaimUpdateModel userOperationClaim)
        {
            var result = await _request.PutAsync("api/UserOperationClaims/update", userOperationClaim);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("UserOperationClaims", "Admin");
        }
        [HttpGet]
        public IActionResult Update()
        {
            var id = RouteData.Values["id"];
            var url = string.Format("api/UserOperationClaims/getdetailbyid?userOperationId=" + id);
            var apiObject = _request.GetAsync(url).Result;
            var jsonObj = JsonConvert.DeserializeObject<List<UserOperationClaimModel>>(apiObject);
            return View(jsonObj);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove()
        {
            var id = RouteData.Values["id"];
            var url = string.Format("api/UserOperationClaims/removebyid?id=" + id);
            var result = await _request.DeleteAsync(url);
            if (result == Constants.Exception)
            {
                return View();
            }
            return RedirectToAction("UserOperationClaims", "Admin");
        }

    }
}
