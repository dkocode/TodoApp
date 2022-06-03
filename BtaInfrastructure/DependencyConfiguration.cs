using BtaDomainInterfaces.Todo;
using BtaInfrastructure.Todo;
using Microsoft.Extensions.DependencyInjection;

namespace BtaInfrastructure;

public static class DependencyConfiguration
{
    public static void RegisterDependencies(this IServiceCollection services)
    {
        services.AddSingleton<ITodoRepository, TodoRepository>();
    }
}