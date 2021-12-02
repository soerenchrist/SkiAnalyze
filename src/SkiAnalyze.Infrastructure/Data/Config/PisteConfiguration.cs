using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.Entities.PisteAggregate;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class PisteConfiguration : IEntityTypeConfiguration<Piste>
{
    public void Configure(EntityTypeBuilder<Piste> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.HasMany(x => x.Coordinates)
            .WithOne(x => x.Piste)
            .HasForeignKey(x => x.PisteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
