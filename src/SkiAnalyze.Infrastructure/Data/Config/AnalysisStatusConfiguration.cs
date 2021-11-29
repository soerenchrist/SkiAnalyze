using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.Common.Analysis;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class AnalysisStatusConfiguration : IEntityTypeConfiguration<AnalysisStatus>
{
    public void Configure(EntityTypeBuilder<AnalysisStatus> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
    }
}
