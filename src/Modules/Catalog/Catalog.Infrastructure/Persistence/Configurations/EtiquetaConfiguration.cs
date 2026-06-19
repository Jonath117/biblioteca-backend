using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class EtiquetaConfiguration : IEntityTypeConfiguration<Etiqueta>
{
    public void Configure(EntityTypeBuilder<Etiqueta> builder)
    {
        builder.ToTable("Etiquetas", "catalog");
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Nombre).IsUnique();
    }
}