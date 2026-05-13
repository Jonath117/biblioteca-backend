using IAM.Application.Interfaces.Authentication;
using IAM.Application.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddIamApplication(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioRepository>();
        services.AddScoped<IJwtTokenGenerator>();
        
        return services;
    }
}