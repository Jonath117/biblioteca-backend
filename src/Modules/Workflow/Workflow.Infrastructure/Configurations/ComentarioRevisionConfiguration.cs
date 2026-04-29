using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workflow.Domain.Entities;

namespace Workflow.Infrastructure.Configurations;

public class ComentarioRevisionConfiguration : IEntityTypeConfiguration<ComentarioRevision>
{
    public void Configure(EntityTypeBuilder<ComentarioRevision> builder)
    {
        builder.ToTable("ComentariosRevision", "workflow");
        builder.HasKey(c => c.Id);
        
        builder.HasOne(c => c.Revision)
            .WithMany(r => r.Comentarios)
            .HasForeignKey(c => c.RevisionId);
    }
}