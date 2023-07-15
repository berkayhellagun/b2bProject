using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models.DetailModel;

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
            //var Url = string.Format("api/Suppliers/supplierdetailbyid?supplierId=" + supplierId);
            //var apiObject = _request.GetAsync(Url).Result;
            //var jsonObject = JsonConvert.DeserializeObject<SupplierDetailModel>(apiObject);
            //return View(jsonObject);
            return View();
        }
    }
}
