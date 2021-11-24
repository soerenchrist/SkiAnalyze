using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiAnalyze.Core.SessionAggregate.Specifications;

public class GetSessionByIdWithTracks : Specification<UserSession>, ISingleResultSpecification
{
    public GetSessionByIdWithTracks(Guid id)
    {
        Query
            .Where(x => x.Id == id)
            .Include(x => x.Tracks);
    }
}
