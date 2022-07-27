using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.ViewComponents
{
    public class ProductGetList : ViewComponent
    {
        IRequest _request;
        public ProductGetList(IRequest request)
        {
            _request = request;
        }

        public IViewComponentResult Invoke()
        {
            var apiObject = _request.GetAsync("api/Products/getall").Result;
            var jsonObject = JsonConvert.DeserializeObject<List<ProductModel>>(apiObject);
            return View(jsonObject);
        } 
    }
}
