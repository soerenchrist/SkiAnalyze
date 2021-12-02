
using Ardalis.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;
using SkiAnalyze.Core.Entities.PisteAggregate;
using SkiAnalyze.Core.Entities.GondolaAggregate;
using SkiAnalyze.Core.Entities.TrackAggregate;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.SkiAreaAggregate;

namespace SkiAnalyze.Data;

public class AppDbContext : DbContext
{
    public DbSet<Gondola> Gondolas { get; set; } = default!;
    public DbSet<Piste> Pistes { get; set; } = default!;
    public DbSet<Track> Tracks { get; set; } = default!;
    public DbSet<AnalysisStatus> AnalysisStatus { get; set; } = default!;
    public DbSet<Run> Runs { get; set; } = default!;
    public DbSet<TrackPoint> TrackPoints { get; set; } = default!;
    public DbSet<SkiArea> SkiAreas { get; set; } = default!;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}
