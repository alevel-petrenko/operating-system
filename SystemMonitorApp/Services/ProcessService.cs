using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using SystemMonitor.Api.Models;
using SystemMonitor.Api.SignalR;

namespace SystemMonitor.Api.Services;

public class ProcessService(IMapper mapper, IHubContext<ProcessHub> hub) : IProcessService
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

    public async Task NotifyProcessesUpdated(List<ProcessInfo> processes)
    {
        await hub.Clients.All.SendAsync("ProcessesUpdated", processes);
    }
}
