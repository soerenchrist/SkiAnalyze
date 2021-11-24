using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.PisteAggregate;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class PisteNodeConfiguration : IEntityTypeConfiguration<PisteNode>
{
    public void Configure(EntityTypeBuilder<PisteNode> builder)
    {
    }
}
