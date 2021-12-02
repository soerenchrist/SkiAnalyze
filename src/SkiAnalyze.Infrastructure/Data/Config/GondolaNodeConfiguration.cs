using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiAnalyze.Core.Entities.GondolaAggregate;

namespace SkiAnalyze.Infrastructure.Data.Config;

public class GondolaNodeConfiguration : IEntityTypeConfiguration<GondolaNode>
{
    public void Configure(EntityTypeBuilder<GondolaNode> builder)
    {
    }
}