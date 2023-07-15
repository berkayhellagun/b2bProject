using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.API;
using WebMVC.Models;

namespace WebMVC.ViewComponents
{
    public class CurrentUser : ViewComponent
    {
        IRequest _request;

        public CurrentUser(IRequest request)
        {
            _request = request;
        }

        public IViewComponentResult Invoke(string email)
        {
            var requestUrl = string.Format("api/Person/getbyemail?email=" + email);
            var apiObject = _request.GetAsync(requestUrl).Result;
            var jsonObject = JsonConvert.DeserializeObject<UserModel>(apiObject);
            return View(jsonObject);
        }
    }
}
