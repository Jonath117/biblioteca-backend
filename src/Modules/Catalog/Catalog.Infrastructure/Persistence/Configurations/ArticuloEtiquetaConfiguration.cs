using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class ArticuloEtiquetaConfiguration : IEntityTypeConfiguration<ArticuloEtiqueta>
{
    public void Configure(EntityTypeBuilder<ArticuloEtiqueta> builder)
    {
        builder.ToTable("ArticuloEtiqueta", "catalog");
        builder.HasKey(ae => new { ae.ArticuloId, ae.EtiquetaId }); // Llave compuesta

        builder.HasOne(ae => ae.Articulo)
            .WithMany(a => a.ArticuloEtiquetas)
            .HasForeignKey(ae => ae.ArticuloId);

        builder.HasOne(ae => ae.Etiqueta)
            .WithMany(e => e.ArticuloEtiquetas)
            .HasForeignKey(ae => ae.EtiquetaId);
    }
}