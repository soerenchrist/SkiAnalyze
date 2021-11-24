using SkiAnalyze.Core.SessionAggregate;

namespace SkiAnalyze.Core.Interfaces;

public interface IUserSessionManager
{
    Task<UserSession> CreateUserSession();
    Task UpdateUserSession(UserSession session);
    Task<UserSession?> GetUserSession(Guid guid);
}
