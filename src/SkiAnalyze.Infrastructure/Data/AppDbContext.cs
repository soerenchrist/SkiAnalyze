
using Ardalis.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;
using SkiAnalyze.Core.PisteAggregate;
using SkiAnalyze.Core.GondolaAggregate;

namespace SkiAnalyze.Data;

public class AppDbContext : DbContext
{
    public DbSet<Gondola> Gondolas { get; set; } = default!;
    public DbSet<Piste> Pistes { get; set; } = default!;

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
