using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workflow.Domain.Entities;

namespace Workflow.Infrastructure.Configurations;

public class RevisionConfiguration :  IEntityTypeConfiguration<Revision>
{
    public void Configure(EntityTypeBuilder<Revision> builder)
    {
        builder.ToTable("Revisiones", "workflow");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Estado).IsRequired().HasMaxLength(50);
    }
}