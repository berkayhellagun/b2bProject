using Microsoft.AspNetCore.Mvc.Filters;
using WebMVC.Models;

namespace WebMVC.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var role = context.HttpContext.Session.GetString(Constants.Role);
            string[] roles = Roles.Split(new char[] { ',' });
            if (!roles.Contains(role))
            {
                context.HttpContext.Response.Redirect("/Auth/Login");
            }
        }
    }
}
