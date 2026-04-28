using Microsoft.EntityFrameworkCore;
using Workspace.Domain.Entities;

namespace Workspace.Infrastructure.Persistence.Configurations;

public class WorkspaceDbContext : DbContext
{
    public WorkspaceDbContext(DbContextOptions<WorkspaceDbContext> options) : base(options) { }

    public DbSet<Documento> Documentos => Set<Documento>();
    public DbSet<Coautor> Coautores => Set<Coautor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkspaceDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}