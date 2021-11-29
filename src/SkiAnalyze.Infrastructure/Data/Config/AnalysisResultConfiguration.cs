using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Common.Analysis;
using System.Text.Json;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class AnalysisResultConfiguration : IEntityTypeConfiguration<AnalysisResult>
{
    public void Configure(EntityTypeBuilder<AnalysisResult> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Bounds)
            .HasConversion(x => JsonSerializer.Serialize(x, new JsonSerializerOptions()), 
            x => JsonSerializer.Deserialize<Bounds>(x, new JsonSerializerOptions())!);
        builder.HasMany(x => x.Runs)
            .WithOne(x => x.AnalysisResult)
            .HasForeignKey(x => x.AnalysisId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
