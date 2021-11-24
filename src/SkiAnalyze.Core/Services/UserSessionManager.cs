using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.SessionAggregate;
using SkiAnalyze.Core.SessionAggregate.Specifications;
using SkiAnalyze.SharedKernel.Interfaces;

namespace SkiAnalyze.Core.Services;

public class UserSessionManager : IUserSessionManager
{
    private readonly IRepository<UserSession> _repository;

    public UserSessionManager(IRepository<UserSession> repository)
    {
        _repository = repository;
    }

    public async Task<UserSession> CreateUserSession()
    {
        var guid = Guid.NewGuid();
        var userSession = new UserSession
        {
            Id = guid,
            LastUsage = GetTimeStamp()
        };
        await _repository.AddAsync(userSession);

        return userSession;
    }

    public async Task<UserSession?> GetUserSession(Guid guid)
    {

        var session = await _repository.GetBySpecAsync(new GetSessionByIdWithTracks(guid));
        if (session ==null)
            return null;

        session.LastUsage = GetTimeStamp();
        await _repository.UpdateAsync(session);
        return session;
    }

    public async Task UpdateUserSession(UserSession session)
    {
        await _repository.UpdateAsync(session);
    }

    private long GetTimeStamp()
    {
        return DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
}
