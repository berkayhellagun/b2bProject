using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.MemoryCaching;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    // this class dependency resolver for core module.
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            //var client = new BoltGraphClient(new Uri("bolt+s://39ddb34f.databases.neo4j.io:7687"), "neo4j", "testConnection");
            //client.ConnectAsync();
            services.AddMemoryCache();
            //services.AddSingleton<IGraphClient>(client);
            services.AddSingleton<ICacheService, MemoryCacheManager>();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
        }
    }
}
