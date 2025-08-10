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

    /// <summary>
    /// Notifies about an updated list of processes.
    /// </summary>
    /// <param name="processes">A collection of <see cref="ProcessInfo"/> objects containing information about the updated processes.</param>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    Task NotifyProcessesUpdated(IEnumerable<ProcessInfo> processes);

    /// <summary>
    /// Sets the priority for the specified process up.
    /// </summary>
    /// <param name="processId">The identifier of the process for which to set the priority.</param>
    void SetPriorityUp(int processId);

    /// <summary>
    /// Sets the priority for the specified process down.
    /// </summary>
    /// <param name="processId">The identifier of the process for which to set the priority.</param>
    void SetPriorityDown(int processId);

    /// <summary>
    /// Terminates (kills) the specified process.
    /// </summary>
    /// <param name="processId">The identifier of the process to be terminated.</param>
    void KillProcess(int processId);
}
