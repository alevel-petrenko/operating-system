namespace SystemMonitor.Api.Models;

public enum ProcessPriority
{
    Idle = 1,
    BelowNormal = 2,
    Normal = 3,
    AboveNormal = 4,
    High = 5,

    // Not used in the UI to avoid system freeze, but available for completeness
    RealTime = 6
}
