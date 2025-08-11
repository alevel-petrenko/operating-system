namespace SystemMonitor.Api.Services;

public interface ILoggingService
{
    void Log(string message, LogLevel level = LogLevel.Information);
}
