using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalayze.Core.PisteAggregate;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class PisteConfiguration : IEntityTypeConfiguration<Piste>
{
    public void Configure(EntityTypeBuilder<Piste> builder)
    {
        builder.OwnsMany(x => x.Coordinates);
    }
}
