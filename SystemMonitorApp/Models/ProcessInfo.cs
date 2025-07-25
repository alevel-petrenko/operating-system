namespace SystemMonitor.Api.Models;

public class ProcessInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ThreadCount { get; set; }
    public long MemoryUsageKb { get; set; }
    // good to have
    public string UserName { get; set; }
    public bool IsSystemProcess { get; set; }
}
