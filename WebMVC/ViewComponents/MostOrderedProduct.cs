using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.ViewComponents
{
    public class MostOrderedProduct : ViewComponent
    {
        IRequest _request;
        public MostOrderedProduct(IRequest request)
        {
            _request = request;
        }

        public IViewComponentResult Invoke()
        {
            var apiObject = _request.GetAsync("api/Products/getproductsmostorderedlast24hours").Result;
            var jsonApi = JsonConvert.DeserializeObject<List<MostOrderedProductModel>>(apiObject);
            return View(jsonApi);
        }
    }
}
