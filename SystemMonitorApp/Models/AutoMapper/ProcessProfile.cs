using AutoMapper;
using System.Diagnostics;

namespace SystemMonitor.Api.Models.AutoMapper;

public class ProcessProfile : Profile
{
    public ProcessProfile()
    {
        CreateMap<Process, ProcessInfo>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProcessName))
            .ForMember(dest => dest.ThreadCount, opt => opt.MapFrom(src => src.Threads.Count))
            .ForMember(dest => dest.MemoryUsageKb, opt => opt.MapFrom(src => src.WorkingSet64 / 1024));
    }
}
