using System.Diagnostics;

namespace SystemMonitor.Api.Models;

public class ProcessInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string MainWindowTitle { get; set; } = string.Empty;
    public ProcessPriorityClass Priority { get; set; }
    public int ThreadCount { get; set; }
    public long MemoryUsageMb { get; set; }
    public DateTime StartTime { get; set; }
    public string? UserName { get; set; } = string.Empty;
}
