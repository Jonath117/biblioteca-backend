using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Workflow.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkflowPresentation(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddControllers()
            .AddApplicationPart(assembly);

        return services;
    }
}
