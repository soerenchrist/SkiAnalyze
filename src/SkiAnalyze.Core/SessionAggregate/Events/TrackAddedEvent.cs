using SkiAnalyze.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiAnalyze.Core.SessionAggregate.Events;

public class TrackAddedEvent : BaseDomainEvent
{
    public Track Track { get; set; }
    public UserSession UserSession { get; set; }

    public TrackAddedEvent(UserSession userSession, Track track)
    {
        Track = track;
        UserSession = userSession;
    }
}
