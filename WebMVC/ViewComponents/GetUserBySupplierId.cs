using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.ViewComponents
{
    public class GetUserBySupplierId : ViewComponent
    {
        IRequest _request;

        public GetUserBySupplierId(IRequest request)
        {
            _request = request;
        }

        public IViewComponentResult Invoke(string supplierId)
        {
            var url = string.Format("api/Users/getuserbysupplierid?supplierId=" + supplierId);
            var apiObject = _request.GetAsync(url).Result;
            var jsonObject = JsonConvert.DeserializeObject <List<UserModel>>(apiObject);
            return View(jsonObject);
        }
    }
}
