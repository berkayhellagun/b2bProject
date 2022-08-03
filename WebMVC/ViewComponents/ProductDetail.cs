using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.ViewComponents
{
    public class ProductDetail : ViewComponent
    {
        IRequest _request;

        public ProductDetail(IRequest request)
        {
            _request = request;
        }

        public IViewComponentResult Invoke(string productId)
        {
            var requestUrl = string.Format("api/Products/getproductdetailbyid?id=" + productId);
            var apiObject = _request.GetAsync(requestUrl).Result;
            var jsonObject = JsonConvert.DeserializeObject<List<ProductDetailModel>>(apiObject);
            return View(jsonObject);
        }
    }
}
