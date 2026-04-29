using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<ArticuloPublicado> ArticulosPublicados => Set<ArticuloPublicado>();
    public DbSet<Etiqueta> Etiquetas => Set<Etiqueta>();
    public DbSet<ArticuloEtiqueta> ArticuloEtiquetas => Set<ArticuloEtiqueta>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}