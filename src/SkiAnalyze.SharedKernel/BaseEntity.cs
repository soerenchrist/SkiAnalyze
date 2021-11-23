namespace SkiAnalyze.SharedKernel;

public abstract class BaseEntity<TId> where TId : struct
{
    public TId Id { get; set; }

    public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
}