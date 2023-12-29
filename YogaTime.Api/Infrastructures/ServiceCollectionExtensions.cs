using YogaTime.Common;
using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Context;
using YogaTime.Repositories;
using YogaTime.Services;
using YogaTime.Shared;

namespace YogaTime.Api.Infrastructures
{
    static internal class ServiceCollectionExtensions
    {
        public static void AddDependencies(this IServiceCollection service)
        {
            service.AddTransient<IDateTimeProvider, DateTimeProvider>();
            service.AddTransient<IDbWriterContext, DbWriterContext>();
            service.RegisterAutoMapperProfile<ApiAutoMapperProfile>();

            service.RegisterModule<ServiceModule>();
            service.RegisterModule<RepositoryModule>();
            service.RegisterModule<ContextModule>();

            service.RegisterAutoMapper();
        }
    }
}
