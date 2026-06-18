using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogPresentation(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        services.AddControllers()
            .AddApplicationPart(assembly);

        return services;
    }
}