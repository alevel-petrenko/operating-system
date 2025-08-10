using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using SystemMonitor.Api.Extensions;
using SystemMonitor.Api.Models;
using SystemMonitor.Api.SignalR;

namespace SystemMonitor.Api.Services;

public class ProcessService(IMapper mapper, IHubContext<ProcessHub> hub) : IProcessService
{
    public IEnumerable<ProcessInfo> GetActiveProcesses()
    {
        return mapper.Map<IEnumerable<ProcessInfo>>(Process.GetProcesses());
    }

    public void SetPriorityUp(int processId)
    {
        var process = Process.GetProcessById(processId);

        if (process != null)
        {
            var nextPrio = process.PriorityClass.ToMyPriority().GetNext();
            process.PriorityClass = nextPrio.ToMicrosoftPriority();
        }
    }

    public void SetPriorityDown(int processId)
    {
        var process = Process.GetProcessById(processId);

        if (process != null)
        {
            var prevPrio = process.PriorityClass.ToMyPriority().GetPrevious();
            process.PriorityClass = prevPrio.ToMicrosoftPriority();
        }
    }

    public void KillProcess(int processId)
    {
        var process = Process.GetProcessById(processId);
        process?.Kill();
    }

    public async Task NotifyProcessesUpdated(IEnumerable<ProcessInfo> processes)
    {
        await hub.Clients.All.SendAsync("ProcessesUpdated", processes);
    }
}
