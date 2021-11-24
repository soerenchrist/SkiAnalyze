using Ardalis.Specification;

namespace SkiAnalyze.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}
