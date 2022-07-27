using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.ViewComponents
{
    public class SupplierGetList : ViewComponent
    {
        IRequest _request;

        public SupplierGetList(IRequest request)
        {
            _request = request;
        }

        public IViewComponentResult Invoke()
        {
            var apiObject = _request.GetAsync("api/Suppliers/getall").Result;
            var jsonObject = JsonConvert.DeserializeObject<List<SupplierModel>>(apiObject);
            return View(jsonObject);
        }
    }
}
