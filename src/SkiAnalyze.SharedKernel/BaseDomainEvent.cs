using MediatR;

namespace SkiAnalyze.SharedKernel;

public abstract class BaseDomainEvent : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
