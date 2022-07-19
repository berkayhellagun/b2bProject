using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Business.BusinessAspect
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var isAuth = _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated;
            var roleClaims = _httpContextAccessor.HttpContext?.User?.ClaimRoles();// we get user claims 

            
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role)) //does exist role claims in role array
                {
                    return;
                }
            }
            throw new Exception("Authorization Denied");
        }
    }
}
