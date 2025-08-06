using Microsoft.AspNetCore.Mvc;
using SystemMonitor.Api.Models;
using SystemMonitor.Api.Services;

namespace SystemMonitor.Api.Controllers;

[Route("api/[controller]")]
public class ProcessController(IProcessService processService) : ControllerBase
{
    /// <summary>
    /// Retrieves a collection of information about all currently active processes.
    /// </summary>
    /// <remarks>This method returns details for each active process, including its identifier, name, and
    /// other relevant metadata. The collection will be empty if no processes are active at the time of the
    /// call.</remarks>
    /// <returns>An enumerable collection of <see cref="ProcessInfo"/> objects representing the active processes.</returns>
    [HttpGet("all")]
    public IEnumerable<ProcessInfo> GetActiveProcesses()
    {
        return processService.GetActiveProcesses();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="processId"></param>
    /// <returns></returns>
    [HttpPost("decreasePriority/{processId}")]
    public IActionResult DecreasePriority(int processId)
    {
        try
        {
            processService.SetPriority(processId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to set priority for process {processId}: {ex.Message}");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="processId"></param>
    /// <returns></returns>
    [HttpPost("increasePriority/{processId}")]
    public IActionResult IncreasePriority(int processId)
    {
        try
        {
            processService.SetPriority(processId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to set priority for process {processId}: {ex.Message}");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="processId"></param>
    /// <returns></returns>
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
            return BadRequest($"Failed to kill process {processId}: {ex.Message}");
        }
    }
}
