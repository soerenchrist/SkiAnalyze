using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.TrackAggregate;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class RunConfiguration : IEntityTypeConfiguration<Run>
{
    public void Configure(EntityTypeBuilder<Run> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasMany(x => x.Coordinates)
            .WithOne(x => x.Run)
            .HasForeignKey(x => x.RunId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Gondola)
            .WithMany(x => x.Runs)
            .HasForeignKey(x => x.GondolaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
