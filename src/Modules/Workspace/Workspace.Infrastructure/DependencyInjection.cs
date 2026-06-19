using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workspace.Application.Common.Interfaces;
using Workspace.Infrastructure.Persistence.Configurations;
using Workspace.Infrastructure.Persistence.Repositories;
using Workspace.Infrastructure.Storage;

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

        // Registrar Repositorios y Unit of Work
        services.AddScoped<IDocumentoRepository, DocumentoRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Configuración dinámica de almacenamiento (S3 vs Local)
        var storageProvider = configuration["StorageSettings:Provider"] ?? "Local";
        
        if (storageProvider.Equals("S3", System.StringComparison.OrdinalIgnoreCase))
        {
            services.AddScoped<IFileStorageService, S3FileStorageService>();
        }
        else
        {
            services.AddScoped<IFileStorageService, LocalFileStorageService>();
        }

        return services;
    }
}