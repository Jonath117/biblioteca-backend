using IAM.Infrastructure.Authentication;
using IAM.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddIamInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IamDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
        });

        // Aquí más adelante registrar los Repositorios de IAM
        // Ejemplo: services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        services.AddScoped<JwtTokenGenerator>();

        return services;
    }
}