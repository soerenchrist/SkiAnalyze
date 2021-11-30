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
