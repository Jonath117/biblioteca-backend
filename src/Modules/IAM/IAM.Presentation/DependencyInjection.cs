using Microsoft.Extensions.DependencyInjection;

namespace IAM.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddIamPresentation(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        services.AddControllers()
            .AddApplicationPart(assembly);

        return services;
    }
}