using SystemMonitor.Api.Services;

namespace SystemMonitor.Api.Background;

public class ProcessBackgroundService(IProcessService processService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var activeProcesses = processService.GetActiveProcesses();

            await processService.NotifyProcessesUpdated(activeProcesses);

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
