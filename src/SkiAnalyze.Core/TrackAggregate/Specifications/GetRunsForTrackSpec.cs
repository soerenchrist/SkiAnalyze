﻿using Ardalis.Specification;

namespace SkiAnalyze.Core.TrackAggregate.Specifications;

public class GetRunsForTrackSpec : Specification<Run>
{
    public GetRunsForTrackSpec(int trackId)
    {
        Query
            .Where(x => x.TrackId == trackId)
            .Include(x => x.Gondola);
    }
}