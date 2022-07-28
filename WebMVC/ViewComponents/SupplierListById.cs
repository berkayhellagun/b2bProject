using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.ViewComponents
{
    public class SupplierListById : ViewComponent
    {
        IRequest _request;

        public SupplierListById(IRequest request)
        {
            _request = request;
        }

        public IViewComponentResult Invoke(string supplierId)
        {
            var Url = string.Format("api/Suppliers/getbyid?id=" + supplierId);
            var apiObject = _request.GetAsync(Url).Result;
            var jsonObject = JsonConvert.DeserializeObject<SupplierModel>(apiObject);
            return View(jsonObject);
        }
    }
}
