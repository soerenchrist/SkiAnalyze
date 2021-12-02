using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.Entities.SkiAreaAggregate;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class SkiAreaNodeConfiguration : IEntityTypeConfiguration<SkiAreaNode>
{
    public void Configure(EntityTypeBuilder<SkiAreaNode> builder)
    {
    }
}
