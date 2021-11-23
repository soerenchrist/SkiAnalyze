using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.GondolaAggregate;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class GondolaConfiguration : IEntityTypeConfiguration<Gondola>
{
    public void Configure(EntityTypeBuilder<Gondola> builder)
    {
        builder.OwnsMany(x => x.Coordinates);
    }
}
