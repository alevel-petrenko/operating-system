using AutoMapper;
using System.Diagnostics;
using SystemMonitor.Api.Models;

namespace SystemMonitor.Api.Services;

public class ProcessService(IMapper mapper) : IProcessService
{
    public IEnumerable<ProcessInfo> GetActiveProcesses()
    {
        return mapper.Map<IEnumerable<ProcessInfo>>(Process.GetProcesses());
    }
}
