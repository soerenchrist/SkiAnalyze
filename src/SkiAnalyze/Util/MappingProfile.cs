using SkiAnalyze.Core.Common;

namespace SkiAnalyze.Util;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Gondola, GondolaDto>()
            .ForMember(x => x.Used,
                x => x.MapFrom(g => g.Runs.Any()));;
        CreateMap<GondolaNode, GondolaNodeDto>();
        CreateMap<Piste, PisteDto>();
        CreateMap<PisteNode, PisteNodeDto>();
        CreateMap<Track, TrackDto>();
        CreateMap<Run, RunDto>();
        CreateMap<AnalysisStatus, AnalysisStatusDto>();
        CreateMap<TrackPoint, TrackPointDto>();
        CreateMap<SkiArea, SkiAreaDto>();
        CreateMap<SkiArea, SkiAreaDetailDto>();
        CreateMap<SkiAreaNode, SkiAreaNodeDto>();
        CreateMap(typeof(BaseStatValue<,>), typeof(BaseStatValueDto<,>));
    }

}
