using Microsoft.Extensions.DependencyInjection;
using YogaTime.Common;
using YogaTime.Services.Automappers;
using YogaTime.Shared;

namespace YogaTime.Services
{
    public class ServiceModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);
            service.RegisterAutoMapperProfile<ServiceProfile>();
        }
    }
}
