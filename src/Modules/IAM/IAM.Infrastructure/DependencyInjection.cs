using IAM.Application.Interfaces.Authentication;
using IAM.Application.Interfaces.Repository;
using IAM.Infrastructure.Authentication;
using IAM.Infrastructure.Persistence.Configurations;
using IAM.Infrastructure.Persistence.Repositories;
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
        

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}