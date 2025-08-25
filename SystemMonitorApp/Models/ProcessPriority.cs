namespace SystemMonitor.Api.Models;

public enum ProcessPriority
{
    Idle = 0,
    BelowNormal = 1,
    Normal = 2,
    AboveNormal = 3,
    High = 4,

    // Not used in the UI to avoid system freeze, but available for completeness
    RealTime = 5
}
