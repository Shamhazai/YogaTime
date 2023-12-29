using Microsoft.Extensions.DependencyInjection;
using YogaTime.Common;
using YogaTime.Shared;

namespace YogaTime.Repositories
{
    public class RepositoryModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
