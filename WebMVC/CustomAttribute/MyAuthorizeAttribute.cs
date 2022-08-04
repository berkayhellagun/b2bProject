using Microsoft.AspNetCore.Mvc.Filters;
using WebMVC.Models.Cons;

namespace WebMVC.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        private string[] _roles;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
             _roles = Roles.Split(',');
            var role = context.HttpContext.Session.GetString(Constants.Role);
            if (!_roles.Contains(role))
            {
                context.HttpContext.Response.Redirect("/Auth/Login");
            }
        }
    }
}
