using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using SystemMonitor.Api.Extensions;
using SystemMonitor.Api.Models;
using SystemMonitor.Api.SignalR;

namespace SystemMonitor.Api.Services;

public class ProcessService(IMapper mapper, IHubContext<ProcessHub> hub, ILoggingService logger) : IProcessService
{
    /// <inheritdoc/>
    public IEnumerable<ProcessInfo> GetActiveProcesses()
    {
        return mapper.Map<IEnumerable<ProcessInfo>>(Process.GetProcesses());
    }

    /// <inheritdoc/>
    public void SetPriorityUp(int processId)
    {
        var process = Process.GetProcessById(processId);

        if (process != null)
        {
            logger.LogInfo($"Setting priority for process {process.ProcessName} (ID: {processId}) to higher priority.");
            var nextPrio = process.PriorityClass.ToMyPriority().GetNext();
            process.PriorityClass = nextPrio.ToMicrosoftPriority();
        }
    }

    /// <inheritdoc/>
    public void SetPriorityDown(int processId)
    {
        var process = Process.GetProcessById(processId);

        if (process != null)
        {
            logger.LogInfo($"Setting priority for process {process.ProcessName} (ID: {processId}) to lower priority.");
            var prevPrio = process.PriorityClass.ToMyPriority().GetPrevious();
            process.PriorityClass = prevPrio.ToMicrosoftPriority();
        }
    }

    /// <inheritdoc/>
    public void KillProcess(int processId)
    {
        var process = Process.GetProcessById(processId);
        logger.LogInfo($"Killing process {process.ProcessName} (ID: {processId}).");
        process?.Kill();
    }

    /// <inheritdoc/>
    public async Task NotifyProcessesUpdated(IEnumerable<ProcessInfo> processes)
    {
        await hub.Clients.All.SendAsync("ProcessesUpdated", processes);
    }
}
