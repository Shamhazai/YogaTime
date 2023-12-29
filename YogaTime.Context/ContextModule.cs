using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using YogaTime.Common;
using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Context.Contracts;

namespace YogaTime.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.TryAddScoped<ITimeTableContext>(provider => provider.GetRequiredService<TimeTableContext>());
            service.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<TimeTableContext>());
            service.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<TimeTableContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TimeTableContext>());
        }
    }
}
