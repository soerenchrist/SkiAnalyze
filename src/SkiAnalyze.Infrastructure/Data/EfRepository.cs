using Ardalis.Specification.EntityFrameworkCore;
using EFCore.BulkExtensions;
using SkiAnalyze.Data;
using SkiAnalyze.SharedKernel.Interfaces;

namespace SkiAnalyze.Infrastructure.Data;
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    private readonly AppDbContext _appDbContext;
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public Task BulkInsertAsync(List<T> records)
    {
        return _appDbContext.BulkInsertAsync(records, new BulkConfig
        {
            SetOutputIdentity = true,
            PreserveInsertOrder = true,
        });
    }
    public Task BulkInsertOrUpdateAsync(List<T> records)
    {
        return _appDbContext.BulkInsertOrUpdateAsync(records);
    }
}