using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.Common;

namespace SkiAnalyze.Infrastructure.Data.Config;

internal class TrackPointConfiguration : IEntityTypeConfiguration<TrackPoint>
{
    public void Configure(EntityTypeBuilder<TrackPoint> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasOne(x => x.Piste)
            .WithMany(x => x.TrackPoints)
            .HasForeignKey(x => x.PisteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
