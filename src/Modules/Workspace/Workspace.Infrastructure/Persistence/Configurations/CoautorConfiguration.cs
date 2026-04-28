using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workspace.Domain.Entities;

namespace Workspace.Infrastructure.Persistence.Configurations;

public class CoautorConfiguration: IEntityTypeConfiguration<Coautor>
{
    public void Configure(EntityTypeBuilder<Coautor> builder)
    {
        builder.ToTable("Coautores", "workspace"); // Esquema workspace
        
        builder.HasKey(c => new { c.DocumentId, c.UsuarioId });

        builder.HasOne(c => c.Documento)
            .WithMany(d => d.Coautores)
            .HasForeignKey(c => c.DocumentId);
    }
}