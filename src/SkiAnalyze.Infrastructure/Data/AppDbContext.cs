
using Ardalis.EFCore.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkiAnalyze.Core.PisteAggregate;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.SharedKernel;
using SkiAnalyze.Core.SessionAggregate;

namespace SkiAnalyze.Data;

public class AppDbContext : DbContext
{
    private readonly IMediator? _mediator;
    public DbSet<Gondola> Gondolas { get; set; } = default!;
    public DbSet<Piste> Pistes { get; set; } = default!;
    public DbSet<UserSession> UserSessions { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator? mediator) : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_mediator == null) return result;

        // dispatch events only if save was successful
       /* var entitiesWithEvents = ChangeTracker.Entries(typeof(BaseEntity<>))
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.Events.ToArray();
            entity.Events.Clear();
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }
       */
        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}
