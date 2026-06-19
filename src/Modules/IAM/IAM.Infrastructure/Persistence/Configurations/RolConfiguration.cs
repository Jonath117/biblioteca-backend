using IAM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAM.Infrastructure.Persistence.Configurations;

public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("Roles", "iam"); 
        
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Nombre).IsRequired().HasMaxLength(50);

        builder.HasData(
            new Rol { Id = 1, Nombre = "Estudiante" },
            new Rol { Id = 2, Nombre = "Asesor" },
            new Rol { Id = 3, Nombre = "Administrador" }
        );
    }
}