using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.ViewComponents
{
    public class SupplierListByCategoryId : ViewComponent
    {
        IRequest _request;

        public SupplierListByCategoryId(IRequest request)
        {
            _request = request;
        }

        public IViewComponentResult Invoke(string categoryId)
        {
            var apiObject = _request.GetAsync("api/Person/GetSellerByCategoryId?categoryId=" + categoryId).Result;
            var jsonObj = JsonConvert.DeserializeObject<List<SupplierModel>>(apiObject);
            return View(jsonObj);
        }
    }
}
