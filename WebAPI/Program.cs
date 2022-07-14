using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using NLog;
using NLog.Web;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Utilities.Security.Encryption;
using Core.Utilities.IoC;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.MemoryCaching;

var builder = WebApplication.CreateBuilder(args);

//services
IServiceCollection service = builder.Services;

// Add services to the container.

//use service for extending. this addDependecyResolver is extension.
service.AddDependencyResolvers( new ICoreModule[]
{
    new CoreModule(),
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Autofac integretion for resolve of dependency
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>
        (builder => builder.RegisterModule(new AutofacBusinessModule()));

//NLog integretion for resolve of dependency
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
builder.Logging.ClearProviders();
builder.Host.UseNLog();


//JWT configuration
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

//app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
