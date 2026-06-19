using Microsoft.EntityFrameworkCore;
using Workflow.Domain.Entities;

namespace Workflow.Infrastructure.Configurations;

public class WorkflowDbContext : DbContext
{
    public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options) : base(options) { }

    public DbSet<Revision> Revisiones => Set<Revision>();
    public DbSet<ComentarioRevision> Comentarios => Set<ComentarioRevision>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("workflow");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkflowDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}