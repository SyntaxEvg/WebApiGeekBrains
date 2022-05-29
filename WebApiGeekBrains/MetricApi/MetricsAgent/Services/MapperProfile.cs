using AutoMapper;
using MetricsAgent.Metrics;
using MetricsAgent.Responses;
using System;

namespace MetricsAgent.Services
{


    public class MapperProfile : Profile
    {//можно еще ускорить  нависив поверх linq.expression  ну для дз  сойдет и так  
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>()
                .ForMember(dto => dto.Time,
                    opt => opt.MapFrom(
                        src => DateTimeOffset.FromUnixTimeSeconds(src.Time.ToUnixTimeSeconds())));

            CreateMap<DotNetMetric, DotNetMetricDto>()
                .ForMember(dto => dto.Time,
                    opt => opt.MapFrom(
                        src => DateTimeOffset.FromUnixTimeSeconds(src.Time.ToUnixTimeSeconds())));

            CreateMap<HddMetric, HddMetricDto>()
                .ForMember(dto => dto.Time,
                    opt => opt.MapFrom(
                        src => DateTimeOffset.FromUnixTimeSeconds(src.Time.ToUnixTimeSeconds())));

            CreateMap<NetworkMetric, NetworkMetricDto>()
                .ForMember(dto => dto.Time,
                    opt => opt.MapFrom(
                        src => DateTimeOffset.FromUnixTimeSeconds(src.Time.ToUnixTimeSeconds())));

            CreateMap<RamMetric, RamMetricDto>()
                .ForMember(dto => dto.Time,
                    opt => opt.MapFrom(
                        src => DateTimeOffset.FromUnixTimeSeconds(src.Time.ToUnixTimeSeconds())));
        }
    }
}
