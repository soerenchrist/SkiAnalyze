using AutoMapper;
using SkiAnalyze.ApiModels;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Common.Analysis;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Core.PisteAggregate;
using SkiAnalyze.Core.SessionAggregate;

namespace SkiAnalyze.Util;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Gondola, GondolaDto>();
        CreateMap<GondolaNode, GondolaNodeDto>();
        CreateMap<Piste, PisteDto>();
        CreateMap<PisteNode, PisteNodeDto>();
        CreateMap<Track, TrackDto>();
        CreateMap<Run, RunDto>();
        CreateMap<AnalysisStatus, AnalysisStatusDto>();
    }

}
