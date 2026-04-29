using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class ArticuloPublicadoConfiguration : IEntityTypeConfiguration<ArticuloPublicado>
{
    public void Configure(EntityTypeBuilder<ArticuloPublicado> builder)
    {
        builder.ToTable("ArticulosPublicados", "catalog");
        builder.HasKey(a => a.Id);
    }
}