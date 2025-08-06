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

    public void SetPriority(int processId)
    {
        var process = Process.GetProcessById(processId);

        if (process != null)
        {
            // TODO Set the real process priority
            process.PriorityClass = ProcessPriorityClass.High;
        }
    }

    public void KillProcess(int processId)
    {
        var process = Process.GetProcessById(processId);
        process?.Kill();
    }
}
