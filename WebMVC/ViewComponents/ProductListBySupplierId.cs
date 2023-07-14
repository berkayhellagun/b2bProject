using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.ViewComponents
{
    public class ProductListBySupplierId : ViewComponent
    {
        IRequest _request;

        public ProductListBySupplierId(IRequest request)
        {
            _request = request;
        }

        public IViewComponentResult Invoke(string supplierId)
        {
            var url = string.Format("api/Person/getproductsbysellerid?sellerId=" + supplierId);
            var apiObject = _request.GetAsync(url).Result;
            var jsonObject = JsonConvert.DeserializeObject<List<ProductModel>>(apiObject);
            return View(jsonObject);
        }
    }
}
