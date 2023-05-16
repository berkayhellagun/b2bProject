using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using Core.Utilities.Security.JWT.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.Neo4J;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        // this method override from module(autofac)
        protected override void Load(ContainerBuilder builder)
        {
            //auth solve
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            //category solve
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<NeoCategoryDal>().As<ICategoryDal>().SingleInstance();

            //product solve
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<NeoProductDal>().As<IProductDal>().SingleInstance();

            //supplier solve
            builder.RegisterType<SupplierManager>().As<ISupplierService>().SingleInstance();
            builder.RegisterType<EfSupplierDal>().As<ISupplierDal>().SingleInstance();

            //user solve
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            //operation claim solve
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>().SingleInstance();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();

            //user operation claim
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();

            //for aop --- this section provide interception 
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                            {
                                Selector = new AspectInterceptorSelector()
                            }).SingleInstance();
        }
    }
}
