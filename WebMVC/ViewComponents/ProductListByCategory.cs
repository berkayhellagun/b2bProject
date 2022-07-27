using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.ViewComponents
{
    public class ProductListByCategory : ViewComponent
    {
        IRequest _request;

        public ProductListByCategory(IRequest request)
        {
            _request = request;
        }

        public IViewComponentResult Invoke(string categoryId)
        {
            var url = string.Format("api/Products/getproductbycategoryid?categoryId="+categoryId);
            var apiObject = _request.GetAsync(url).Result;
            var jsonObject = JsonConvert.DeserializeObject<List<ProductModel>>(apiObject);
            return View(jsonObject);
        }
    }
}
