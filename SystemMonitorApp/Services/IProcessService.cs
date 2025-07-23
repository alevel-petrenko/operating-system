using SystemMonitor.Api.Models;

namespace SystemMonitor.Api.Services;

public interface IProcessService
{
    /// <summary>
    /// Retrieves a collection of information about all currently active processes on the system.
    /// </summary>
    /// <remarks>This method provides information about processes that are currently running on the system. 
    /// The returned collection may include system processes, user processes, and background processes.</remarks>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ProcessInfo"/> objects, where each object contains details about
    /// an active process. The collection will be empty if no processes are active.</returns>
    IEnumerable<ProcessInfo> GetActiveProcesses();
}
