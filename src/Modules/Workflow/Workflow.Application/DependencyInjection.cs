using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Workflow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkflowApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}