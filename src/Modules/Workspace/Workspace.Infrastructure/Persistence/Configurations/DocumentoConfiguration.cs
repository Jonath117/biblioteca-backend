using Microsoft.EntityFrameworkCore;
using Workspace.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Workspace.Infrastructure.Persistence.Configurations;

public class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
{
    public void Configure(EntityTypeBuilder<Documento> builder)
    {
        builder.ToTable("Documentos", "workspace");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Titulo).IsRequired().HasMaxLength(250);
    }
    
}