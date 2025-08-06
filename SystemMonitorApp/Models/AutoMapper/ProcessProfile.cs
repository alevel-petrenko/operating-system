using AutoMapper;
using System.Diagnostics;
using System.Management;

namespace SystemMonitor.Api.Models.AutoMapper;

public class ProcessProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessProfile"/> class, configuring mappings  between <see
    /// cref="Process"/> and <see cref="ProcessInfo"/> objects.
    /// </summary>
    /// <remarks>This constructor sets up mappings to transform properties of a <see
    /// cref="System.Diagnostics.Process"/>  into corresponding properties of a <see cref="ProcessInfo"/> object.
    /// </remarks>
    public ProcessProfile()
    {
        CreateMap<Process, ProcessInfo>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProcessName))
            //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => GetProcessOwner(src.Id)))
            .ForMember(dest => dest.MainWindowTitle, opt => opt.MapFrom(src => src.MainWindowTitle))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => GetPriority(src)))
            .ForMember(dest => dest.ThreadCount, opt => opt.MapFrom(src => src.Threads.Count))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => GetStartTime(src)))
            .ForMember(dest => dest.MemoryUsageMb, opt => opt.MapFrom(src => src.WorkingSet64 / 1048576));
    }

    private static DateTime GetStartTime(Process process)
    {
        try
        {
            return process.StartTime;
        }
        catch { }

        return DateTime.Now;
    }

    private static ProcessPriorityClass GetPriority(Process process)
    {
        try
        {
            return process.PriorityClass;
        }
        catch { }

        return ProcessPriorityClass.Normal;
    }

    private static string GetProcessOwner(int processId)
    {
        try
        {
            string query = $"SELECT * FROM Win32_Process WHERE ProcessId = {processId}";
            using ManagementObjectSearcher searcher = new(query);
            foreach (ManagementObject obj in searcher.Get())
            {
                object[] args = new object[2];
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", args));
                if (returnVal == 0)
                {
                    return $"{args[1]}\\{args[0]}";
                }
            }
        }
        catch { }

        return "Unknown";
    }
}
