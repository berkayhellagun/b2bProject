using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.ViewComponents
{
    public class CategoryGetList : ViewComponent
    {
        IRequest _request;
        public CategoryGetList(IRequest request)
        {
            _request = request;
        }
        public IViewComponentResult Invoke()
        {
            var apiObject = _request.GetAsync("api/Categories/getall").Result;
            var jsonApi = JsonConvert.DeserializeObject<List<CategoryModel>>(apiObject);
            return View(jsonApi);
        }
    }
}
