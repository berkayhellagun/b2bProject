using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models.AddModel;
using WebMVC.Models.Cons;
using WebMVC.Models.DetailModel;

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
            var userId = HttpContext.Session.GetString(Constants.UserId);
            if (!string.IsNullOrEmpty(userId))
            {
                var orderData = new OrderAddModel { BuyerId = Convert.ToInt32(userId), OrderDate = DateTime.Now, ProductId = Convert.ToInt32(productId), ProductQuantity = 0, SellerId = 0 };
                _request.PostAsync("api/order/add", orderData);
            }

            var requestUrl = string.Format($"api/Products/getproductdetailsbyid?productId={productId}");
            var apiObject = _request.GetAsync(requestUrl).Result;
            var jsonObject = JsonConvert.DeserializeObject<ProductDetailModel>(apiObject);
            return View(jsonObject);
        }
    }
}
