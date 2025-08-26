using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SystemMonitor.Api.Models;
using SystemMonitor.Api.Services;

namespace SystemMonitor.Api.Controllers;

[Route("api/[controller]")]
public class ProcessController(IProcessService processService, ILoggingService logger) : ControllerBase
{
    /// <summary>
    /// Retrieves a collection of information about all currently active processes.
    /// </summary>
    /// <remarks>This method returns details for each active process, including its identifier, name, and
    /// other relevant metadata. The collection will be empty if no processes are active at the time of the
    /// call.</remarks>
    /// <returns>An enumerable collection of <see cref="ProcessInfo"/> objects representing the active processes.</returns>
    [HttpGet("all")]
    public ActionResult<IEnumerable<ProcessInfo>> GetActiveProcesses()
    {
        try
        {
            return Ok(processService.GetActiveProcesses());
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to retrieve active processes: " + ex.Message);
            return BadRequest("Failed to retrieve active processes.");
        }
    }

    /// <summary>
    /// Decreases the priority of the specified process.
    /// </summary>
    /// <param name="processId">The identifier of the process whose priority should be decreased.</param>
    /// <returns>
    /// Returns **200 OK** if the operation is successful.
    /// Returns **400 Bad Request** with an error message if the priority could not be set.
    /// </returns>
    [HttpPost("decreasePriority/{processId}")]
    public IActionResult DecreasePriority(int processId)
    {
        try
        {
            processService.SetPriorityDown(processId);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to decrease priority for process {processId}: {ex.Message}");
            return BadRequest($"Failed to decrease priority for process {processId}.");
        }
    }

    /// <summary>
    /// Increases the priority of the specified process.
    /// </summary>
    /// <param name="processId">The identifier of the process whose priority should be increased.</param>
    /// <returns>
    /// Returns **200 OK** if the operation is successful.
    /// Returns **400 Bad Request** with an error message if the priority could not be set.
    /// </returns>
    [HttpPost("increasePriority/{processId}")]
    public IActionResult IncreasePriority(int processId)
    {
        try
        {
            processService.SetPriorityUp(processId);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to increase priority for process {processId}: {ex.Message}");
            return BadRequest($"Failed to increase priority for process {processId}.");
        }
    }

    /// <summary>
    /// Terminates (kills) the specified process.
    /// </summary>
    /// <param name="processId">The identifier of the process to be terminated.</param>
    /// <returns>
    /// Returns **200 OK** if the operation is successful.
    /// Returns **400 Bad Request** with an error message if the process could not be killed.
    /// </returns>
    [HttpPost("kill/{processId}")]
    public IActionResult KillProcess(int processId)
    {
        try
        {
            processService.KillProcess(processId);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to kill process {processId}: {ex.Message}");
            return BadRequest($"Failed to kill process {processId}.");
        }
    }
}
