using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using TimeTable203.Common.Entity.InterfaceDB;
using YogaTime.Context.Contracts;

namespace YogaTime.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.TryAddScoped<IYogaTimeContext>(provider => provider.GetRequiredService<YogaTimeContext>());
            service.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<YogaTimeContext>());
            service.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<YogaTimeContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<YogaTimeContext>());
        }
    }
}
