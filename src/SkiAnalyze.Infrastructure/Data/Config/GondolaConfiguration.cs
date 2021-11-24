using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.GondolaAggregate;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class GondolaConfiguration : IEntityTypeConfiguration<Gondola>
{
    public void Configure(EntityTypeBuilder<Gondola> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.HasMany(x => x.Coordinates)
            .WithOne(x => x.Gondola)
            .HasForeignKey(x => x.GondolaId);
    }
}
