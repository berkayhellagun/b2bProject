using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Security.Principal;
using WebMVC.API;
using WebMVC.Middleware;
using WebMVC.Models;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection service = builder.Services;

// Add services to the container.
service.AddControllersWithViews();

//service.AddMvc(config =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//                     .RequireAuthenticatedUser()
//                     .Build();
//    config.Filters.Add(new AuthorizeFilter(policy));
//});

service.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => x.LoginPath = "/Auth/Login");

service.AddSingleton<IRequest, Request>();
service.AddHttpContextAccessor();
service.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();
service.AddSession();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
};
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseMiddleware<WebMVC.Middleware.AuthorizationMiddleware>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}"); // ilk çalýþacak

app.Run();
