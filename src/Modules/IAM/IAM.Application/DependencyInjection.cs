using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddIamApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}