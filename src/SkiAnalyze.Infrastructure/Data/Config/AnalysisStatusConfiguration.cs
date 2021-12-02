using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.Entities.TrackAggregate;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class AnalysisStatusConfiguration : IEntityTypeConfiguration<AnalysisStatus>
{
    public void Configure(EntityTypeBuilder<AnalysisStatus> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Track)
            .WithOne(x => x.AnalysisStatus)
            .HasForeignKey<AnalysisStatus>(x => x.TrackId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
