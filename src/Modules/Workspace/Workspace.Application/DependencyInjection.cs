using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Workspace.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkspaceApplication(this IServiceCollection services)
    {
        // Registrar MediatR para este ensamblado
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        // Registrar FluentValidation para este ensamblado
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
