using AutoMapper;
using SkiAnalyze.ApiModels;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.GondolaAggregate;
using System.Reflection;

namespace SkiAnalyze.Util;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Gondola, GondolaDto>();
        CreateMap<GondolaNode, GondolaCoordinateDto>();
    }

}
