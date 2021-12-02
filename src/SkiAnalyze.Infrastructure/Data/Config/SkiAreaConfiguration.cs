using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.Entities.SkiAreaAggregate;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class SkiAreaConfiguration : IEntityTypeConfiguration<SkiArea>
{
    public void Configure(EntityTypeBuilder<SkiArea> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasMany(x => x.Nodes)
            .WithOne(x => x.SkiArea)
            .HasForeignKey(x => x.SkiAreaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
