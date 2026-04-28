using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workspace.Infrastructure.Persistence.Configurations;

namespace Workspace.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkspaceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configurar Base de Datos
        services.AddDbContext<WorkspaceDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
        });

        // Aquí más adelante registrar los Repositorios de Workspace
        // Ejemplo: services.AddScoped<IDocumentoRepository, DocumentoRepository>();

        return services;
    }
}