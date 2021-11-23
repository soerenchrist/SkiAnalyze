using Ardalis.Specification;

namespace SkiAnalyze.SharedKernel.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
    Task BulkInsertAsync(List<T> records);
    Task BulkInsertOrUpdateAsync(List<T> records);
}
