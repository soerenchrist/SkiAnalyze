using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.SessionAggregate;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasMany(x => x.Tracks)
            .WithOne(x => x.UserSession)
            .HasForeignKey(x => x.UserSessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
