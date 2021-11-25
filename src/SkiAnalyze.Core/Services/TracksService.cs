using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.SessionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiAnalyze.Core.Services;

public class TracksService : ITracksService
{
    private readonly IUserSessionManager _userSessionManager;

    public TracksService(IUserSessionManager userSessionManager)
    {
        _userSessionManager = userSessionManager;
    }

    public async Task<Track?> GetTrack(Guid userSessionId, int trackId)
    {
        var session = await _userSessionManager.GetUserSession(userSessionId);
        if (session == null)
            return null;

        return session.Tracks.FirstOrDefault(x => x.Id == trackId);
    }

    public async Task<Track> AddTrack(Track track)
    {
        var session = await _userSessionManager.GetUserSession(track.UserSessionId);
        if (session == null)
        {
            session = await _userSessionManager.CreateUserSession();            
        }

        session.AddTrack(track);
        await _userSessionManager.UpdateUserSession(session);

        return track;
    }

    public async Task ClearTracks(Guid sessionId)
    {
        var session = await _userSessionManager.GetUserSession(sessionId);
        if (session != null)
        {
            session.ClearTracks();
            await _userSessionManager.UpdateUserSession(session);
        }
    }

    public async Task<List<Track>> GetTracks(Guid sessionId)
    {
        var session = await _userSessionManager.GetUserSession(sessionId); 
        if (session != null)
        {
            return session.Tracks.ToList();
        }
        return new List<Track>();
    }

    public async Task RemoveTrack(Track track)
    {
        var session = await _userSessionManager.GetUserSession(track.UserSessionId);
        if (session != null)
        {
            session.RemoveTrack(track);
            await _userSessionManager.UpdateUserSession(session);
        }
    }

}
