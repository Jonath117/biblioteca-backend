using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Repositories;
using Workflow.Infrastructure.Configurations;
using Workflow.Infrastructure.Repositories;

namespace Workflow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkflowInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRevisionRepository, RevisionRepository>();
        services.AddDbContext<WorkflowDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
        });
        return services;
    }
}