namespace SystemMonitor.Api.Services;

public interface IEnvironmentService
{
    Dictionary<string, string> GetEnvironmentVariablesForProcess(int processId);
}
