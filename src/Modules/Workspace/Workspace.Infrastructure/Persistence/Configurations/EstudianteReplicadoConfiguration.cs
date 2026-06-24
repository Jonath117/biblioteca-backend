using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workspace.Domain.Entities;

namespace Workspace.Infrastructure.Persistence.Configurations;

public class EstudianteReplicadoConfiguration : IEntityTypeConfiguration<EstudianteReplicado>
{
    public void Configure(EntityTypeBuilder<EstudianteReplicado> builder)
    {
        builder.ToTable("EstudiantesReplicados", "workspace");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Email).IsRequired().HasMaxLength(150);
        builder.Property(e => e.Nombre).HasMaxLength(100);
        builder.Property(e => e.Apellido).HasMaxLength(100);
    }
}
