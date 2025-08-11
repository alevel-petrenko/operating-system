
namespace SystemMonitor.Api.Services;

public class FileLoggingService : ILoggingService
{
    private static readonly object _logLock = new();
    private const string LogFileName = "logs.txt";

    public void LogError(string message)
    {
        lock (_logLock)
        {
            using var stream = new FileStream(LogFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
            using var writer = new StreamWriter(stream);
            writer.WriteLine($"{DateTime.Now}: ERROR - {message}");
        }
    }

    public void LogInfo(string message)
    {
        lock (_logLock)
        {
            using var stream = new FileStream(LogFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
            using var writer = new StreamWriter(stream);
            writer.WriteLine($"{DateTime.Now}: INFO - {message}");
        }
    }
}
