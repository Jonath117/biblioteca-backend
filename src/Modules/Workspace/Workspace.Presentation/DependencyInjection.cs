using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Workspace.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkspacePresentation(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddControllers()
            .AddApplicationPart(assembly);

        return services;
    }
}
