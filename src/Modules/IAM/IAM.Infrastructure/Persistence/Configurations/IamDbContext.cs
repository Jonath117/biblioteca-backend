using IAM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.Persistence.Configurations;

public class IamDbContext : DbContext 
{
    public IamDbContext(DbContextOptions<IamDbContext> options) : base(options){ }
    
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Rol> Roles => Set<Rol>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IamDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
}