﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.Entities.TrackAggregate;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.HasMany(x => x.Runs)
            .WithOne(x => x.Track)
            .HasForeignKey(x => x.TrackId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.SkiArea)
            .WithMany(x => x.Tracks)
            .HasForeignKey(x => x.SkiAreaId);
    }
}
